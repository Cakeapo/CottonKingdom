using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Player_Motor : MonoBehaviour
{
    public static Scr_Player_Motor instance;

    public float runSpeed = 10;
    public float backwardSpeed = 5;
    public float walkSpeed = 6;
    public float slideSpeed = 10f;
    public float chargeSpeed = 25f;
    public float chargeTurnRate = 0.5f;
    public float jumpLenience = 0.2f;
    public float jumpSpeed = 6f;
    public float gravity = 21f;
    public float terminalVelocity = 20f;
    public float slideThreshold = 0.6f;
    public float maxControllableSlideMagnitude = 0.4f;
    // float airMovementReduction = 0.5f;
    public float dashMultiplier = 5f;
    public float chargeMultiplier = 2.5f;
    public float chargeTurnReduction = 10f;
    public float overSpeedReduce = 0.5f;

    private Vector3 slideDirection;
    private float timeSpentAirborn;
    public float activeChargeMultipler;

    public Vector3 moveVector { get; set; }
    public float verticalVelocity { get; set; }
    public float forwardSpeed { get; set; }
    public bool Charging { get; set; }

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
        Charge();

        // Transform moveVector to world space
        moveVector = transform.TransformDirection(moveVector);

        // Normalize moveVector if mag > 1
        if (moveVector.magnitude > 1)
            moveVector = Vector3.Normalize(moveVector);

        // Apply slide if applicable
        ApplySlide();

        // Multiply moveVector by moveSpeed
        moveVector *= moveSpeed();

        // Reapply vertical velocity to moveVector.y
        moveVector = new Vector3(moveVector.x, verticalVelocity, moveVector.z);

        // Apply gravity
        ApplyGravity();

        // Move character in world space
        Scr_Player_Controller.characterController.Move(moveVector * Time.deltaTime);

        CheckAirbornTime();
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

        if(Physics.Raycast(transform.position, Vector3.down, out hitInfo))
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
        if(timeSpentAirborn < jumpLenience)
        {
            verticalVelocity = jumpSpeed;
            timeSpentAirborn += jumpLenience;
        }

//        if (Scr_Player_Controller.characterController.isGrounded)
//            verticalVelocity = jumpSpeed;
    }

    public void SnapAlignCharacterWithCamera()
    {
        if (!Charging)
        {
            if (moveVector.x != 0 || moveVector.z != 0)
            {
                transform.rotation = Quaternion.Euler(transform.eulerAngles.x,
                    Camera.main.transform.eulerAngles.y,
                    transform.eulerAngles.z);
            }
        }
    }

    public float moveSpeed()
    {
        var curMoveSpeed = 0f;

        switch (Scr_Player_Animator.instance.moveDirection)
        {
            case Scr_Player_Animator.Direction.stationary:
                curMoveSpeed = 0;
                break;
            case Scr_Player_Animator.Direction.forward:
                curMoveSpeed = forwardSpeed;
                break;
            case Scr_Player_Animator.Direction.backward:
                curMoveSpeed = backwardSpeed;
                break;
            case Scr_Player_Animator.Direction.left:
                curMoveSpeed = forwardSpeed;
                break;
            case Scr_Player_Animator.Direction.right:
                curMoveSpeed = forwardSpeed;
                break;
            case Scr_Player_Animator.Direction.leftForward:
                curMoveSpeed = forwardSpeed;
                break;
            case Scr_Player_Animator.Direction.rightForward:
                curMoveSpeed = forwardSpeed;
                break;
            case Scr_Player_Animator.Direction.leftBackward:
                curMoveSpeed = backwardSpeed;
                break;
            case Scr_Player_Animator.Direction.rightBackward:
                curMoveSpeed = backwardSpeed;
                break;
        }

        if (slideDirection.magnitude > 0)
            curMoveSpeed = slideSpeed;

        if (Charging)
            curMoveSpeed = chargeSpeed;

        return curMoveSpeed;
    }

    public void Charge()
    {
        if (Charging)
        {
            transform.rotation = Quaternion.Euler(transform.eulerAngles.x,
                    transform.eulerAngles.y + (moveVector.x * chargeTurnRate * Time.deltaTime),
                    transform.eulerAngles.z);

            moveVector = new Vector3(0, 0, 1);
        }
    }

    void CheckAirbornTime()
    {
        if (!Scr_Player_Controller.characterController.isGrounded)
            timeSpentAirborn += Time.deltaTime;
        else
            timeSpentAirborn = 0;
    }
}
