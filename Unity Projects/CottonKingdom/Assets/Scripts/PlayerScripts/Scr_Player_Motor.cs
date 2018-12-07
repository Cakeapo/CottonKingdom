using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Player_Motor : MonoBehaviour
{
    public static Scr_Player_Motor instance;

    public float moveSpeed = 10;

    public Vector3 moveVector { get; set; }

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

        // Multiply moveVector by moveSpeed
        moveVector *= moveSpeed;

        // Multiply moveVector by deltaTime
        moveVector *= Time.deltaTime;

        // Move character in world space
        Scr_Player_Controller.characterController.Move(moveVector);

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
}
