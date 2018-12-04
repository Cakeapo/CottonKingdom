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
    public GameObject info;

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
            print("NPC Interaction_01");
        }

    }

    private void LoadData()
    {
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
