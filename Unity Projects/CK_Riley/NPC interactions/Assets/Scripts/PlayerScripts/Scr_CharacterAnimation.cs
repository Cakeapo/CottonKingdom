using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_CharacterAnimation : MonoBehaviour
{
    public Animator anim;
    public Scr_Player_Motor playerMovement;

    public float moveForward, moveStrafe;

	// Use this for initialization
	void Start ()
    {
        playerMovement = GetComponent<Scr_Player_Motor>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        moveForward = playerMovement.moveVector.z;
        moveStrafe = playerMovement.moveVector.x;

        if (playerMovement)
        {
            anim.SetBool("Walking", false);
        }
        else
        {
            anim.SetBool("Walking", true);
        }

        if(moveForward == 0 && moveStrafe == 0)
        {
            anim.SetBool("Moving", false);
        }
        else
        {
            anim.SetBool("Moving", true);
        }

        anim.SetFloat("SpeedForward", moveForward);
        anim.SetFloat("SpeedStrafe", moveStrafe);
    }

    public void Jump()
    {
        anim.SetTrigger("Jump");
    }
}
