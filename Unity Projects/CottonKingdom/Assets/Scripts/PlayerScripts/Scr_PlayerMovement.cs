﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_PlayerMovement : MonoBehaviour
{

    public CharacterController characterController;
    private Vector3 input;

    public float runSpeed, walkSpeed, jumpForce, rotationSpeed;
    private float moveSpeed;
    public bool isRunning;

    public Transform cameraPivot;

    private float yVelocity;
    public float gravity;

    // Use this for initialization
    void Start ()
    {
        characterController = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        if (Input.GetButtonDown("Jump"))
        {
            if (characterController.isGrounded)
            {
                yVelocity = jumpForce;
            }
        }

        if (characterController.isGrounded)
        {
            //yVelocity = 0;
        }
        else
        {
            yVelocity -= gravity * Time.deltaTime;
        }

        characterController.Move(transform.TransformDirection(input * moveSpeed * Time.deltaTime + yVelocity * Vector3.up * Time.deltaTime));


        //Check if walk input is pressed
        isRunning = !Input.GetButton("Walk");

        //Set sprint and walk speed
        if (isRunning)
            moveSpeed = runSpeed;
        else
            moveSpeed = walkSpeed;

        //move relative to camera direction if there is a move input
        if (input.magnitude != 0)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, cameraPivot.rotation, rotationSpeed * Time.deltaTime);
        }
    }
}
