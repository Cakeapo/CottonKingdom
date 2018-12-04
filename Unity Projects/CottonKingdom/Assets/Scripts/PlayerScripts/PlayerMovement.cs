using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float runSpeed, walkSpeed, jumpForce, turnAroundTime;
    private float moveSpeed;
    public bool isGrounded, isRunning;

    public int jumpCount;

    public Rigidbody rb;
    public LayerMask groundedMask;

    public Vector3 input;

    public Transform cameraPivot;

	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        //basic movement
        rb.velocity = transform.TransformDirection(input * moveSpeed);
    }

    private void Update()
    {
        input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        //Check if player is Grounded        
        isGrounded = Physics.CheckSphere(transform.position, 0.3f, groundedMask);

        //Check if walk input is pressed
        isRunning = !Input.GetButton("Walk");

        //Set sprint and walk speed
        if (isRunning)
            moveSpeed = runSpeed;
        else
            moveSpeed = walkSpeed;

        if (isGrounded)
            jumpCount = 1;

        //look in camera direction on move input
        if (input.magnitude != 0)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, cameraPivot.rotation, turnAroundTime * Time.deltaTime);
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (jumpCount > 0)
            {
                rb.velocity += Vector3.up * jumpForce;
                jumpCount--;
            }
        }
    }
}
