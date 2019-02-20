using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Scr_BasicNPC_02 : MonoBehaviour {

    public bool hasInteracted = false, isInteractable = false, isInteracting = false, hasInformation = true;

    public int interactionTotal = 0;
    public int newInteractionsTotal;
    [Space(5)]
    public string dataName, playerTag;

    [Space(10)]

    [Space(15)]

    public GameObject gameManager;
    
    public int interactionCounter = 0;

    [Space(15)]
    public DialogNodeCanvas dialogCanvas;   //this is where the indevidual NPC dialoug trees are stored
    [Space(5)]
    public GameObject testObj;
    private DiagScripts diagScripts;
    [Space(5)]
    public GameObject info, popup;
    public Vector3 player;
    //public ParticleSystem popUpParticle;

    // Use this for initialization
    void Start ()
    {
        diagScripts = testObj.GetComponent<DiagScripts>();
        if(playerTag == null) {

            print("Autoassigned " + gameObject.name + "'s player tag to 'Player'.");
            playerTag = "Player";
        }
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

            gameObject.transform.LookAt(player);

            if (Input.GetAxis("Interact") > 0)
            {
                print("NPC Interaction_01");

                isInteractable = hasInformation = false;
                isInteracting = true;
            }
            
        }
        else
        {
            //popUpParticle.Play(false);    dont play anim
            gameObject.transform.LookAt(Vector3.zero);

        }

        if (isInteracting == true)
        {
            if (hasInteracted == false)
            {
                interactionCounter = -1;
            }
            if (Input.GetButtonDown("Interact"))
            {
                hasInteracted = true;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                interactionCounter +=1;
            }


        }
        else if(isInteracting == false)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }


    }

   

    public void OnTriggerEnter(Collider other)
    {
        print("Triggered.");
        if (other.transform.tag == playerTag)
        {
            player = new Vector3(other.transform.position.x, player.y ,other.transform.position.z);
            isInteractable = true;

            diagScripts.input = true;
            diagScripts.dialogCanvas = dialogCanvas;

        }
    }


    public void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == playerTag)
        {
            PlayerExits();
        }
    }

    public void PlayerExits()
    {
        interactionCounter = 0;
        isInteractable = false;
        isInteracting = false;
        diagScripts.input = false;


    }

}
