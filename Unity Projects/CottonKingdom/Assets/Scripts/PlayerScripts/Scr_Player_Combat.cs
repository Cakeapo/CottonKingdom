using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Player_Combat : MonoBehaviour
{
    public Animator sword_Anim;
    public Animator shield_Anim;

    public bool keepCombo = false;
    public bool canStartNextSwing = false;

    public bool isAttacking = false;
    public bool isBlocking = false;

    public BoxCollider AttackBox;

    void Start()
    {
        AttackBox.enabled = false;
    }


    void Update()
    {
        HandleActionInput();
        CheckIfIdle();

        shield_Anim.SetBool("Blocking", isBlocking);
    }


    void HandleActionInput()
    {
        if (Input.GetButtonDown("Attack"))
        {
            if (keepCombo == false)
                Attack();
        }

        if (Input.GetButton("Block"))
        {
            Block();
        }
        else
            isBlocking = false;
    }

    void Block()
    {
        if (!isAttacking)
            isBlocking = true;
        else
            isBlocking = false;
    }

    void Attack()
    {
        if (!isAttacking) //If idling start an attack
        {
            sword_Anim.SetTrigger("Attack");
        }
        else if (canStartNextSwing) // if late into the animation start the next attack and reset keepCombo
        {
            sword_Anim.SetTrigger("Attack");
            keepCombo = false;
            canStartNextSwing = false;
        }

        keepCombo = true; //tell animator to continue attacking when ready
    }

    public void stopCombo() //gets triggered at the start of each new attack animation
    {
        keepCombo = false;
        canStartNextSwing = false;
    }

    public void openBuffer() //gets triggered near the end of each attack animation
    {
        if (keepCombo) // if another attack is buffered then start an attack
        {
            sword_Anim.SetTrigger("Attack");
            keepCombo = false;
        }
        else // else allow another attack to be triggerd
        {
            canStartNextSwing = true;
        }
    }

    void CheckIfIdle()
    {
        if (sword_Anim.GetCurrentAnimatorStateInfo(0).IsName("Sword_Idle"))
            isAttacking = false;
        else
            isAttacking = true;
    }

    public void TriggerAttackBox()
    {
        if (AttackBox.enabled)
            AttackBox.enabled = false;
        else
            AttackBox.enabled = true;
    }
}
