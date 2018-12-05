using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_CharacterAnimation : MonoBehaviour
{
    public Animator anim;
    public Scr_PlayerMovement playerMovement;

    public float moveForward, moveStrafe;

	// Use this for initialization
	void Start ()
    {
        playerMovement = GetComponent<Scr_PlayerMovement>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        moveForward = playerMovement.input.z;
        moveStrafe = playerMovement.input.x;

        if (playerMovement.isRunning)
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
