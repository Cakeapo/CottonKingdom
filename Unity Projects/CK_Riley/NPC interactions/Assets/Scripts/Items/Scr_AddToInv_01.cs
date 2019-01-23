using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_AddToInv_01 : MonoBehaviour {

    //Obj name to referance what gets added to inventory
    public string ObjName;
    public bool isCollected, toDelet;

	// Use this for initialization
	void Start () {

        if (ObjName == "")
        {
            Debug.LogError("memberVariable must be set to the corrasponding PlayerPrefs counter name for "
            + gameObject.name + ".");
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.transform.tag == "Player")
        {
            //play collect animation


            //make object invisible



            //add to inventory
            int tempInt = PlayerPrefs.GetInt(ObjName);
            if(ObjName != "")   
            {
                PlayerPrefs.SetInt(ObjName, (tempInt + 1));
            }
                /*If the Object is not named to the correct object that is assigned to a 
                PlayerPres account then it will return an error. 
                Plese make sure that it is the correct name and uses the correct naming conventions.
                */
            else
            {
                Debug.LogError("memberVariable must be set to the corrasponding PlayerPrefs counter name for "
                    + gameObject.name + ".");
                Debug.LogError("Autoassigned memberVarible name to: '" + gameObject.name + "'.");
                ObjName = gameObject.name;
            }
            
            
        
            //delet self
            if(toDelet == true)
            {
                Destroy(gameObject);
            }

        }

    }
}
