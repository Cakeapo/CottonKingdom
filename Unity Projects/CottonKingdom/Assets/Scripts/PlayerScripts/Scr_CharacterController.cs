using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_CharacterController : MonoBehaviour
{
    public static CharacterController characterController;

    public float moveSpeed = 5f;
    public float jumpSpeed = 10f;
    public float gravity = 21f;
    public float terminalVelocity = 20f;

    public Vector3 moveVector { get; set; }

    private float verticalVelocity;

	void Awake ()
    {
        characterController = GetComponent<CharacterController>();
    }
	
	void Update ()
    {
        GetMovementInput();
        HandleActionInput();

        UpdateMotor();
	}

    void UpdateMotor()
    {
        SnapAlignCharacterWithCamera();
        ProcessMotion();
    }

    void GetMovementInput()
    {
        var deadZone = 0.1f;

        verticalVelocity = moveVector.y;
        moveVector = Vector3.zero;

        if (Input.GetAxis("Vertical") > deadZone || Input.GetAxis("Vertical") < -deadZone)
            moveVector += new Vector3(0, 0, Input.GetAxis("Vertical"));

        if (Input.GetAxis("Horizontal") > deadZone || Input.GetAxis("Horizontal") < -deadZone)
            moveVector += new Vector3(Input.GetAxis("Horizontal"), 0, 0);
    }

    void HandleActionInput()
    {
        if (Input.GetButtonDown("Jump"))
            Jump();


    }

    void ProcessMotion()
    {
        moveVector = transform.TransformDirection(moveVector); // convert moveVector to world space

        if (moveVector.magnitude > 1) // Normalize
            moveVector = Vector3.Normalize(moveVector);

        moveVector *= moveSpeed; // multiply by movespeed;

        moveVector = new Vector3(moveVector.x, verticalVelocity, moveVector.z); // Reapply vertical velocity

        ApplyGravity();

        characterController.Move(moveVector * Time.deltaTime);
    }

    void ApplyGravity()
    {
        if (moveVector.y > -terminalVelocity)
            moveVector = new Vector3(moveVector.x, moveVector.y - gravity * Time.deltaTime, moveVector.z);

        if (characterController.isGrounded && moveVector.y < -1)
            moveVector = new Vector3(moveVector.x, -1, moveVector.z);
    }

    public void SnapAlignCharacterWithCamera()
    {
        if (moveVector.x != 0 || moveVector.z != 0)
        {
            transform.rotation = Quaternion.Euler(transform.eulerAngles.x,
                Camera.main.transform.eulerAngles.y,
                transform.eulerAngles.z);
        }
    }

    void Jump()
    {
        if (characterController.isGrounded)
            verticalVelocity = jumpSpeed;
    }

    
}
