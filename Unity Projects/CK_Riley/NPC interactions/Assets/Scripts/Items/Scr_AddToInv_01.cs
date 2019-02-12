using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_AddToInv_01 : MonoBehaviour {

    //Obj name to referance what gets added to inventory
    public string objName;
    public bool selfAssignName;
    public bool isCollected, toDelet, isRespawn, isDeleted, canAdd;

    public GameObject objectToSpawn;

    public float respawnTimer, respawnAt;

	// Use this for initialization
	void Start () {



        isDeleted = false;
        canAdd = true;

        respawnTimer = 0;

        if (isRespawn == true && objectToSpawn == null)
        {
                Debug.LogError("memberVariable must be assigned. GameObject to respawn is empty for "
                + gameObject.name + ".");

        }

        if (selfAssignName == true)
        {
            objName = gameObject.name;
        }
        else
        {
            if (objName == "")
            {
                Debug.LogError("memberVariable must be set to the corrasponding PlayerPrefs counter _Name_ for "
                + gameObject.name + ".");
            }
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(isRespawn == true)
        {
            if(respawnTimer < respawnAt && isDeleted == true)
            {
                respawnTimer += Time.deltaTime;
            }
            else
            {
                if (isDeleted == true && toDelet == true)
                {
                    isDeleted = false;
                    GameObject objName = (GameObject)Instantiate(objectToSpawn, 
                        transform.position, transform.rotation);
                    objName.transform.parent = gameObject.transform;
                    toDelet = false;
                    respawnTimer = 0;
                    canAdd = true;

                }
            }

        }


	}

    public void OnTriggerEnter(Collider other)
    {
        if((other.gameObject.transform.tag == "Player" ) && (canAdd == true))
        {
            //play collect animation


            //make object invisible



            //add to inventory
            int tempInt = PlayerPrefs.GetInt(objName);
            
            if(objName != "")   
            {
                PlayerPrefs.SetInt(objName, (tempInt + 1));

                /*
                    this could be optimised by having a local variable that gets called at 
                    the start of the game and only saves during x item or afetr x time,
                    this could help by reducing time delays in having to get a prefs name
                    and int value and then giving it a new value less frequently.
                */

                toDelet = true;
            }


                /*
                    If the Object is not named to the correct object that is assigned to a 
                    PlayerPres account then it will return an error. 
                    Plese make sure that it is the correct name and uses the correct naming conventions.
                */
            else
            {
                Debug.LogError("memberVariable must be set to the corrasponding PlayerPrefs counter name for "
                    + gameObject.name + ".");
                Debug.LogError("Autoassigned memberVarible name to: '" + gameObject.name + "'.");
                objName = gameObject.name;
            }
            
            
        
            //delet self
            if(toDelet == true)
            {
                if (isRespawn == true)
                {
                    Destroy(gameObject.transform.GetChild(0).gameObject); //Delets the child that respawns after x seconds
                    
                    isDeleted = true;
                }

                else Destroy(gameObject);
            }
            canAdd = false;
        }

    }
}
