using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_BasicEnemy : MonoBehaviour
{
    public Rigidbody rb;

    void Start()
    {

    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "AttackBox")
        {
            TakeDamage();
        }
    }

    void TakeDamage()
    {
        print("hit");

        rb.AddForce(Vector3.up * 10, ForceMode.Impulse);
    }
}
