using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Scr_BasicNPC_01 : MonoBehaviour {

    public bool hasInteracted = false, isInteractable = false, isInteracting = false, hasInformation = true;

    public int interactionTotal = 0;
    public int newInteractionsTotal;
    [Space(5)]
    public string dataName, playerTag;

    [Space(15)]

    public GameObject gameManager;
    Scr_GameManager_01 scr_GameManager;
    Text textBox;
    public int interactionCounter = 0;

    [Space(15)]

    private Scr_DataSaver_01 dataSaver;
    [Space(5)]
    public GameObject info, popup;
    //public ParticleSystem popUpParticle;
    Animator anim;


    // Use this for initialization
    void Start ()
    {
        scr_GameManager = gameManager.GetComponent<Scr_GameManager_01>();
        textBox = scr_GameManager.textboxImage.GetComponentInChildren<Text>();

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
                scr_GameManager.activated = true;
                isInteractable = hasInformation = false;
                isInteracting = true;
            }
            
        }
        else
        {
            //popUpParticle.Play(false);    dont play anim
            anim.SetBool("CanInteract", false);
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
                scr_GameManager.interactedWith = !scr_GameManager.interactedWith;
                scr_GameManager.ReplyOptions(GetComponent<CSVReader>().TextLine(interactionCounter / 2, 1).ToString(),
                    GetComponent<CSVReader>().TextLine(interactionCounter / 2, 2).ToString(),
                    GetComponent<CSVReader>().TextLine(interactionCounter / 2, 3).ToString(),
                    GetComponent<CSVReader>().TextLine(interactionCounter / 2, 4).ToString());
                interactionCounter +=1;
            }


        }




        if (GetComponent<CSVReader>().TextLine(interactionCounter/2, 0) == null || GetComponent<CSVReader>().TextLine(interactionCounter / 2, 0) == "")
        {
            textBox.text = "And that's all he wrote...";
        }
        else
        {
            textBox.text = GetComponent<CSVReader>().TextLine(interactionCounter/2, 0).ToString(); //"Tester";//
        }
    }

    private void LoadData() //loads data and number of previous interactions
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

            scr_GameManager.close = true;
            scr_GameManager.canAnswer = false;
            scr_GameManager.interactedWith = true;

            dataSaver.DataSaveInt(dataName, interactionTotal += 1);
            print("Interactin with " + dataName + ", "+ dataSaver.DataGetInt(dataName));
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
        scr_GameManager.close = scr_GameManager.canAnswer = scr_GameManager.interactedWith = false;
        isInteractable = false;
        isInteracting = false;
        print("Interaction finished with " + dataName + ", " + dataSaver.DataGetInt(dataName));
    }



}
