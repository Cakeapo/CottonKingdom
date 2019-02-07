using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_InvOpn_01 : MonoBehaviour {

    public bool inventoryActivate = false;
    public bool changeInv;


	// Use this for initialization
	void Start ()
    {
        changeInv = inventoryActivate = false;


    }
	
	// Update is called once per frame
	void Update ()
    {
		if(/*nput.GetAxis("Inventory") > 0*/ Input.GetKeyDown(KeyCode.I))
        {
            changeInv = !changeInv;
            //inventoryActive = !inventoryActive;
        }

        if (changeInv)
        {
            ChangeInventory();
            /*
            if (inventoryActive)
            {
                //call function inventory up
            }
            */
        }
	}



    void ChangeInventory()
    {
        inventoryActivate = !inventoryActivate;
        if (inventoryActivate)
        {
            print("Inv open.");
            //animate the inventory camer down to show the inventory menu.
            changeInv = !changeInv;
        }
        else
        {
            print("Inv closed.");
            //animate inventory out of view
            changeInv = !changeInv;
        }
    }
}
