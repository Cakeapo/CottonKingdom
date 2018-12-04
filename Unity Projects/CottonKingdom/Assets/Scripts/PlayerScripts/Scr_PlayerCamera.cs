using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_PlayerCamera : MonoBehaviour
{
    private Vector2 mouseInput;
    public float sensitivityX, sensitivityY, lerpSpeed, rotationSpeed;
    private float currentXRotation;
    public float lowerLimit, upperLimit;

    public Transform cameraMount, player;


    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.position = Vector3.Lerp(transform.position, player.transform.position, lerpSpeed * Time.deltaTime);

        mouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        transform.Rotate(Vector3.up, mouseInput.x * sensitivityX);

        currentXRotation = Mathf.Clamp(currentXRotation + mouseInput.y * rotationSpeed, lowerLimit, upperLimit);
        cameraMount.localRotation = Quaternion.identity;
        cameraMount.Rotate(Vector3.left, currentXRotation);
    }
}
