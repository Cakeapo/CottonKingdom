using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Player_Controller : MonoBehaviour
{
    public static CharacterController characterController;
    public static Scr_Player_Controller instance;

	void Awake ()
    {
        characterController = GetComponent<CharacterController>();
        instance = this;

        Scr_Camera.UseExistingOrCreateNewMainCamera();
	}
	
	void Update ()
    {
        if (Camera.main == null)
            return;

        GetLocomotionInput();
        HandleActionInput();

        Scr_Player_Motor.instance.UpdateMotor();
	}

    void GetLocomotionInput()
    {
        var deadZone = 0.1f;

        Scr_Player_Motor.instance.verticalVelocity = Scr_Player_Motor.instance.moveVector.y;
        Scr_Player_Motor.instance.moveVector = Vector3.zero;

        if(Input.GetAxis("Vertical") > deadZone || Input.GetAxis("Vertical") < -deadZone)
            Scr_Player_Motor.instance.moveVector += new Vector3(0, 0, Input.GetAxis("Vertical"));

        if (Input.GetAxis("Horizontal") > deadZone || Input.GetAxis("Horizontal") < -deadZone)
            Scr_Player_Motor.instance.moveVector += new Vector3(Input.GetAxis("Horizontal"), 0, 0);

        Scr_Player_Animator.instance.DetermineCurrentMoveDirection();
    }

    void HandleActionInput()
    {
        if (Input.GetButton("Jump"))
            Jump();
    }

    void Jump()
    {
        Scr_Player_Motor.instance.Jump();
    }
}
