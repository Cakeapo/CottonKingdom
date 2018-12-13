using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Camera : MonoBehaviour
{
    public static Scr_Camera instance;

    public Transform TargetLookAt;
    public float distance = 5f;
    public float distanceMin = 3f;
    public float distanceMax = 10f;
    public float distanceSmooth = 0.05f;
    public float distanceResumeSmooth = 1f;
    public float X_mouseSensitivity = 5f;
    public float Y_mouseSensitivity = 5f;
    public float wheel_mouseSensitivity = 5f;
    public float X_smooth = 0.05f;
    public float Y_smooth = 0.01f;
    public float Y_minLimit = -40f;
    public float Y_maxLimit = 80f;
    public float occlusionDistanceStep = 0.1f;
    public int maxOcclusionChecks = 30;

    private float mouseX;
    private float mouseY;
    private float velX;
    private float velY;
    private float velZ;
    private float velDistance;
    private float startDistance;
    private Vector3 position;
    private Vector3 desiredPosition;
    private float desiredDistance;
    private float _distanceSmooth;
    private float preOcclusionDistance;

    void Awake()
    {
        instance = this;
    }

    void Start ()
    {
        distance = Mathf.Clamp(distance, distanceMin, distanceMax);
        startDistance = distance;
        Reset();
	}
	
	void LateUpdate ()
    {
        if (TargetLookAt == null)
            return;

        HandlePlayerInput();

        var count = 0;
        do
        {
            CalculateDesiredPosition();
            count++;
        } while (CheckIfOccluded(count));

        UpdatePosition();
    }

    void HandlePlayerInput()
    {
        var deadZone = 0.01f;

        mouseX += Input.GetAxis("Mouse X") * X_mouseSensitivity;
        mouseY -= Input.GetAxis("Mouse Y") * Y_mouseSensitivity;

        //limit mouse Y here
        mouseY = Helper.ClampAngle(mouseY, Y_minLimit, Y_maxLimit);

        if(Input.GetAxis("Mouse Wheel") < -deadZone || Input.GetAxis("Mouse Wheel") > deadZone)
        {
            desiredDistance = Mathf.Clamp(distance - Input.GetAxis("Mouse Wheel") * wheel_mouseSensitivity, distanceMin, distanceMax);

            preOcclusionDistance = desiredDistance;
            _distanceSmooth = distanceSmooth;
        }
    }

    void CalculateDesiredPosition()
    {
        //evaluate distance
        ResetDesiredDistance();
        distance = Mathf.SmoothDamp(distance, desiredDistance, ref velDistance, _distanceSmooth);

        //calculate desired position
        desiredPosition = CalculatePosition(mouseY, mouseX, distance);

    }

    Vector3 CalculatePosition(float rotationX, float rotationY, float distance)
    {
        Vector3 direction = new Vector3(0, 0, -distance);
        Quaternion rotation = Quaternion.Euler(rotationX, rotationY, 0);
        return TargetLookAt.position + rotation * direction;
    }

    bool CheckIfOccluded(int count)
    {
        var isOccluded = false;

        var nearestDistance = CheckCameraPoints(TargetLookAt.position, desiredPosition);

        if(nearestDistance != -1)
        {
            if(count < maxOcclusionChecks)
            {
                isOccluded = true;
                distance -= occlusionDistanceStep;

                if (distance < 0.1f) //TEST THIS NUMBER
                    distance = 0.1f;
            }
            else
                distance = nearestDistance - Camera.main.nearClipPlane;

            desiredDistance = distance;
            _distanceSmooth = distanceResumeSmooth;
        }

        return isOccluded;
    }

    float CheckCameraPoints(Vector3 from, Vector3 to)
    {
        var nearestDistance = -1f;

        RaycastHit hitInfo;

        Helper.ClipPlanePoints clipPlanePoints = Helper.ClipPlaneAtNear(to);

        // Draw lines  for visualisation
        Debug.DrawLine(from, to + transform.forward * -Camera.main.nearClipPlane , Color.red);
        Debug.DrawLine(from, clipPlanePoints.upperLeft);
        Debug.DrawLine(from, clipPlanePoints.lowerLeft);
        Debug.DrawLine(from, clipPlanePoints.upperRight);
        Debug.DrawLine(from, clipPlanePoints.lowerRight);

        Debug.DrawLine(clipPlanePoints.upperLeft, clipPlanePoints.upperRight);
        Debug.DrawLine(clipPlanePoints.upperRight, clipPlanePoints.lowerRight);
        Debug.DrawLine(clipPlanePoints.lowerRight, clipPlanePoints.lowerLeft);
        Debug.DrawLine(clipPlanePoints.lowerLeft, clipPlanePoints.upperLeft);

        if (Physics.Linecast(from, clipPlanePoints.upperLeft, out hitInfo) && hitInfo.collider.tag != "Player")
            nearestDistance = hitInfo.distance;

        if (Physics.Linecast(from, clipPlanePoints.lowerLeft, out hitInfo) && hitInfo.collider.tag != "Player")
            if (hitInfo.distance < nearestDistance || nearestDistance == -1)
                nearestDistance = hitInfo.distance;

        if (Physics.Linecast(from, clipPlanePoints.upperRight, out hitInfo) && hitInfo.collider.tag != "Player")
            if (hitInfo.distance < nearestDistance || nearestDistance == -1)
                nearestDistance = hitInfo.distance;

        if (Physics.Linecast(from, clipPlanePoints.lowerRight, out hitInfo) && hitInfo.collider.tag != "Player")
            if (hitInfo.distance < nearestDistance || nearestDistance == -1)
                nearestDistance = hitInfo.distance;

        if (Physics.Linecast(from, to + transform.forward * -Camera.main.nearClipPlane, out hitInfo) && hitInfo.collider.tag != "Player")
            if (hitInfo.distance < nearestDistance || nearestDistance == -1)
                nearestDistance = hitInfo.distance;

        return nearestDistance;
    }

    void ResetDesiredDistance()
    {
        if(desiredDistance < preOcclusionDistance)
        {
            var pos = CalculatePosition(mouseY, mouseX, preOcclusionDistance);

            var nearestDistance = CheckCameraPoints(TargetLookAt.position, pos);

            if(nearestDistance == -1 || nearestDistance > preOcclusionDistance)
            {
                desiredDistance = preOcclusionDistance;
            }
        }
    }

    void UpdatePosition()
    {
        var posX = Mathf.SmoothDamp(position.x, desiredPosition.x, ref velX, X_smooth);
        var posY = Mathf.SmoothDamp(position.y, desiredPosition.y, ref velY, Y_smooth);
        var posZ = Mathf.SmoothDamp(position.z, desiredPosition.z, ref velZ, X_smooth);
        position = new Vector3(posX, posY, posZ);

        transform.position = position;

        transform.LookAt(TargetLookAt);
    }

    public void Reset()
    {
        mouseX = 0;
        mouseY = 10;
        distance = startDistance;
        desiredDistance = distance;
        preOcclusionDistance = distance;
    }

    public static void UseExistingOrCreateNewMainCamera()
    {
        GameObject tempCamera;
        GameObject targetLookAt;
        Scr_Camera myCamera;

        if(Camera.main != null)
        {
            tempCamera = Camera.main.gameObject;
        }
        else
        {
            tempCamera = new GameObject("Main Camera");
            tempCamera.AddComponent<Camera>();
            tempCamera.tag = "MainCamera";
        }

        tempCamera.AddComponent<Scr_Camera>();
        myCamera = tempCamera.GetComponent<Scr_Camera>();

        targetLookAt = GameObject.Find("targetLookAt");

        if(targetLookAt == null)
        {
            targetLookAt = new GameObject("targetLookAt");
            targetLookAt.transform.position = Vector3.zero;
        }

        myCamera.TargetLookAt = targetLookAt.transform;
    }
}
