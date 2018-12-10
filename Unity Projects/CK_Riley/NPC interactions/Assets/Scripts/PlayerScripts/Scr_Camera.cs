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
    public float X_mouseSensitivity = 5f;
    public float Y_mouseSensitivity = 5f;
    public float wheel_mouseSensitivity = 5f;
    public float X_smooth = 0.05f;
    public float Y_smooth = 0.01f;
    public float Y_minLimit = -40f;
    public float Y_maxLimit = 80f;

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
        CalculateDesiredPosition();
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
        }
    }

    void CalculateDesiredPosition()
    {
        //evaluate distance
        distance = Mathf.SmoothDamp(distance, desiredDistance, ref velDistance, distanceSmooth);

        //calculate desired position
        desiredPosition = CalculatePosition(mouseY, mouseX, distance);

    }

    Vector3 CalculatePosition(float rotationX, float rotationY, float distance)
    {
        Vector3 direction = new Vector3(0, 0, -distance);
        Quaternion rotation = Quaternion.Euler(rotationX, rotationY, 0);
        return TargetLookAt.position + rotation * direction;
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
