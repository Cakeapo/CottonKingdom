using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_PlayerMovement : MonoBehaviour
{

    public CharacterController characterController;
    public Vector3 input;

    public float runSpeed, walkSpeed, jumpForce, rotationSpeed;
    public float moveSpeed;
    public bool isRunning;

    public Transform cameraPivot;

    private float yVelocity;
    public float gravity;

    private Vector3 moveVelocity = Vector3.zero;
    private Vector3 velocity;

    public Scr_CharacterAnimation animScript;

    public float airMoveReduction;

    // Use this for initialization
    void Start ()
    {
        characterController = GetComponent<CharacterController>();
        animScript = GetComponent<Scr_CharacterAnimation>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        //input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        if (characterController.isGrounded)
        {
            moveVelocity = transform.TransformDirection(new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"))) * moveSpeed;
            yVelocity = 0;
        }
        else
        {
            //moveVelocity = transform.TransformDirection(new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"))) * (moveSpeed / airMoveReduction);
        }

        yVelocity -= gravity * Time.deltaTime; // reduce y vel at rate of gravity

        if (Input.GetButtonDown("Jump"))
        {
            if (characterController.isGrounded)
            {
                yVelocity = jumpForce;
                animScript.Jump();
            }
        }

        
        velocity = moveVelocity + yVelocity * Vector3.up;
        characterController.Move(velocity * Time.deltaTime);
        //characterController.Move(transform.TransformDirection(input * moveSpeed * Time.deltaTime + yVelocity * Vector3.up * Time.deltaTime));


        //Check if walk input is pressed
        isRunning = !Input.GetButton("Walk");

        //Set sprint and walk speed
        if (isRunning)
            moveSpeed = runSpeed;
        else
            moveSpeed = walkSpeed;

        //move relative to camera direction if there is a move input
        if (moveVelocity.magnitude != 0)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, cameraPivot.rotation, rotationSpeed * Time.deltaTime);
        }
    }
}
