using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Player_Combat : MonoBehaviour
{
    public Animator sword_Anim;
    public bool keepCombo = false;
    public bool canStartNextSwing = false;

    void Start()
    {
        
    }


    void Update()
    {
        //setComboState();

        if (Input.GetButtonDown("Attack"))
        {
            if (keepCombo == false)
            {
                Attack();
            }
        }
    }

    void Attack()
    {
        if (sword_Anim.GetCurrentAnimatorStateInfo(0).IsName("Sword_Idle"))
        {
            sword_Anim.SetTrigger("Attack");
        }
        else if (canStartNextSwing)
        {
            sword_Anim.SetTrigger("Attack");
            keepCombo = false;
            canStartNextSwing = false;
        }

        keepCombo = true;
    }

    public void stopCombo()
    {
        keepCombo = false;
        canStartNextSwing = false;
    }

    /*
    void setComboState()
    {
        if (canStartNextSwing)
        {
            sword_Anim.SetBool("KeepCombo", keepCombo);
        }
    }
    */

    public void openBuffer()
    {
        if (keepCombo)
        {
            sword_Anim.SetTrigger("Attack");
            keepCombo = false;
        }
        else
        {
            canStartNextSwing = true;
        }
    }
}
