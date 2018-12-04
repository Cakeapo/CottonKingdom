using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Scr_BasicNPC_01 : MonoBehaviour {

    public bool hasInteracted = false, isInteractable = false, hasInformation = true;

    public int interactionTotal = 0;
    public int newInteractionsTotal;
    [Space(5)]
    public string dataName, playerTag;

    private Scr_DataSaver_01 dataSaver;
    [Space(5)]
    public GameObject info, popup;
    //public ParticleSystem popUpParticle;
    Animator anim;


    // Use this for initialization
    void Start ()
    {
        LoadData();

    }
	
	// Update is called once per frame
	void Update ()
    {
        if (hasInformation == true)
        {
            info.SetActive(true);
            //&& isInteractable == true
        }
        else info.SetActive(false);

        if (isInteractable == true)
        {
            //print("Hi.");
            //popUpParticle.Play(true);  play anim
            anim.SetBool("CanInteract", true);
            if (Input.GetAxis("Interact") > 0)
            {
                print("NPC Interaction_01");
                anim.SetBool("CanInteract", false);

                isInteractable = hasInformation = false;
            }
        }
        else
        {
            //popUpParticle.Play(false);    dont play anim
            anim.SetBool("CanInteract", false);
        }

    }

    private void LoadData()
    {
        //popUpParticle.Play(false);     set anim to 0
        anim = popup.GetComponent<Animator>();
        anim.SetBool("CanInteract", false);


        dataSaver = GetComponent<Scr_DataSaver_01>();
        print("Retrive interactions >>>");
        interactionTotal = dataSaver.DataGetInt(dataName);
        print("Set Interactions count >>>");
        if (interactionTotal > 0)
        {
            hasInteracted = false;
            print("First Interaction enabled >>>");
        }
        print("Loaded " + dataName);
    }

    public void OnTriggerEnter(Collider other)
    {
        print("Triggered.");
        if (other.transform.tag == playerTag)
        {
            isInteractable = true;
            dataSaver.DataSaveInt(dataName, interactionTotal += 1);
            print("Interactin with " + dataName + ", "+ dataSaver.DataGetInt(dataName));
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == playerTag)
        {
            isInteractable = false;
            print("Interaction finished with " + dataName + ", " + dataSaver.DataGetInt(dataName));
        }
    }
}
