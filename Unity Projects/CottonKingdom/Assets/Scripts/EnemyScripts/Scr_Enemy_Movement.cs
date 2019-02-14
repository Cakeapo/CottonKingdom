using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Scr_Enemy_Movement : MonoBehaviour
{
    public Transform player;
    public Rigidbody rb;
    public NavMeshAgent nav;
    public bool stunned = false;
    public float stunTime = 2f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        nav = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if(nav.enabled)
            nav.SetDestination(player.position);

        if (stunned)
            nav.enabled = false;
        else
            nav.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "AttackBox")
        {
            StopCoroutine(TakeDamage());
            StartCoroutine(TakeDamage());
        }
    }

    IEnumerator TakeDamage()
    {
        print("hit");

        stunned = true;
        rb.AddForce(Vector3.up * 1, ForceMode.Force);
        rb.AddForce(transform.forward * -2, ForceMode.Force);
        yield return new WaitForSeconds(stunTime);
        stunned = false;

        //nav.velocity = new Vector3(nav.velocity.x, 5, nav.velocity.z);
    }
}
