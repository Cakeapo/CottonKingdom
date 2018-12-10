using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Player_Motor : MonoBehaviour
{
    public static Scr_Player_Motor instance;

    public float forwardSpeed = 10;
    public float backwardSpeed = 5;
    public float strafeSpeed = 7;
    public float jumpSpeed = 6f;
    public float gravity = 21f;
    public float terminalVelocity = 20f;
    public float slideThreshold = 0.6f;
    public float maxControllableSlideMagnitude = 0.4f;

    public Vector3 slideDirection;

    public Vector3 moveVector { get; set; }
    public float verticalVelocity { get; set; }

	void Awake()
    {
        instance = this;
	}
	
	public void UpdateMotor()
    {
        SnapAlignCharacterWithCamera();
        ProcessMotion();
    }

    public void ProcessMotion()
    {
        // Transform moveVector to world space
        moveVector = transform.TransformDirection(moveVector);

        // Normalize moveVector if mag > 1
        if (moveVector.magnitude > 1)
            moveVector = Vector3.Normalize(moveVector);

        // Apply slide if applicable
        ApplySlide();

        // Multiply moveVector by moveSpeed
        moveVector *= moveSpeed;

        // Reapply vertical velocity to moveVector.y
        moveVector = new Vector3(moveVector.x, verticalVelocity, moveVector.z);

        // Apply gravity
        ApplyGravity();

        // Move character in world space
        Scr_Player_Controller.characterController.Move(moveVector * Time.deltaTime);

    }

    void ApplyGravity()
    {
        if (moveVector.y > -terminalVelocity)
            moveVector = new Vector3(moveVector.x, moveVector.y - gravity * Time.deltaTime, moveVector.z);

        if(Scr_Player_Controller.characterController.isGrounded && moveVector.y < -1)
            moveVector = new Vector3(moveVector.x, -1, moveVector.z);
    }

    void ApplySlide()
    {
        if (!Scr_Player_Controller.characterController.isGrounded)
            return;

        slideDirection = Vector3.zero;

        RaycastHit hitInfo;

        if(Physics.Raycast(transform.position + Vector3.up, Vector3.down, out hitInfo))
        {
            if (hitInfo.normal.y < slideThreshold)
                slideDirection = new Vector3(hitInfo.normal.x, -hitInfo.normal.y, hitInfo.normal.z);
        }

        if (slideDirection.magnitude < maxControllableSlideMagnitude)
            moveVector += slideDirection;
        else
        {
            moveVector = slideDirection;
        }
    }

    public void Jump()
    {
        if (Scr_Player_Controller.characterController.isGrounded)
            verticalVelocity = jumpSpeed;
    }

    public void SnapAlignCharacterWithCamera()
    {
        if(moveVector.x != 0 || moveVector.z != 0)
        {
            transform.rotation = Quaternion.Euler(transform.eulerAngles.x,
                Camera.main.transform.eulerAngles.y,
                transform.eulerAngles.z);
        }
    }

    float moveSpeed()
    {
        var curMoveSpeed = 0;

        return curMoveSpeed;
    }
}
