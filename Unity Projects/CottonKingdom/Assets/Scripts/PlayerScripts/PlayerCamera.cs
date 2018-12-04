using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour {

    public float rotationSpeedX, rotationSpeedY;
    private float rotX, rotY;
    public float lowerLimit, upperLimit;
    public Transform playerHead;

    public GameObject cameraObject;
    public GameObject cameraMount;
    public Camera playerCam;

    public GameObject player;

    public float lerpSpeed = 0.8f;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.position = Vector3.Lerp(transform.position, player.transform.position, lerpSpeed * Time.deltaTime);
        //cameraObject.transform.LookAt(playerHead);

        rotationSpeedY = Input.GetAxis("Mouse X");
        rotationSpeedX = -Input.GetAxis("Mouse Y");

        rotY = transform.eulerAngles.y + rotationSpeedY;
        rotX = Mathf.Clamp(transform.eulerAngles.x + rotationSpeedX, lowerLimit, upperLimit);

        transform.eulerAngles = new Vector3(transform.eulerAngles.x, rotY, transform.eulerAngles.z);
        cameraMount.transform.localEulerAngles = new Vector3(cameraMount.transform.eulerAngles.x + rotX, 0, 0);
	}
}
