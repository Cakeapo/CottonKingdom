using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float runSpeed, walkSpeed, jumpForce;
    private float moveSpeed;
    public bool isGrounded, isRunning;

    public float velX, velY, velZ;
    public int jumpCount;

    public Rigidbody rb;
    public LayerMask groundedMask;

	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.eulerAngles = new Vector3(0 + rb.velocity.z, 0, 0 - rb.velocity.x);

        //Check if player is Grounded        
        isGrounded = Physics.CheckSphere(transform.position, 0.3f, groundedMask);

        //Check if walk input is pressed
        isRunning = !Input.GetButton("Walk");

        //Set sprint and walk speed
        if (isRunning)
            moveSpeed = runSpeed;
        else
            moveSpeed = walkSpeed;

        //basic movement
        rb.velocity = new Vector3(velX, rb.velocity.y, velZ);

        velX = Input.GetAxis("Horizontal") * moveSpeed;
        velZ = Input.GetAxis("Vertical") * moveSpeed;

        //jumpcounter for double jumping
        if (isGrounded)
            jumpCount = 1;

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
