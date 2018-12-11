using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Player_Animator : MonoBehaviour {

    public enum Direction
    {
        stationary, forward, backward, left, right,
        leftForward, rightForward, leftBackward, rightBackward
    }

    public static Scr_Player_Animator instance;

    public Direction moveDirection { get; set; }

	void Awake ()
    {
        instance = this;
	}
	
	void Update ()
    {
		
	}

    public void DetermineCurrentMoveDirection()
    {
        var forward = false;
        var backward = false;
        var left = false;
        var right = false;

        if (Scr_Player_Motor.instance.moveVector.z > 0)
            forward = true;
        if (Scr_Player_Motor.instance.moveVector.z < 0)
            backward = true;
        if (Scr_Player_Motor.instance.moveVector.x > 0)
            right = true;
        if (Scr_Player_Motor.instance.moveVector.x < 0)
            left = true;

        if (forward)
        {
            if (left)
                moveDirection = Direction.leftForward;
            else if (right)
                moveDirection = Direction.rightForward;
            else
                moveDirection = Direction.forward;
        }
        else if (backward)
        {
            if (left)
                moveDirection = Direction.leftBackward;
            else if (right)
                moveDirection = Direction.rightBackward;
            else
                moveDirection = Direction.backward;
        }
        else if (left)
        {
            moveDirection = Direction.left;
        }
        else if (right)
        {
            moveDirection = Direction.right;
        }
        else
        {
            moveDirection = Direction.stationary;
        }
    }
}
