using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scr_Change_01 : MonoBehaviour
{
    public Button btn_helmL, btn_helmR, btn_chestL, btn_chestR, btn_otherL, btn_otherR;


    public int currentHelm, currentChest, currentOther,
        totalHelm, totalChest, totalOther;

    public string helmUnlocked, chestUnlocked, otherUnlocked;

    public GameObject[] helmMDL, chestMDL, otherMDL;

    /*
      PlayerPrefs.SetInt("HelmEquip", 0); Sets player ehlmet to slot 0. 

        "HelmEquip"     = helmet
        "ChstEquip"     = Chest
        "OthrEquip"     = Other slot ( can be moded to support other customisations.
    */

    // Start is called before the first frame update
    void Start()
    {
        {
            /*this can be culled after the first test run
            PlayerPrefs.SetInt("HelmEquip", 0);
            PlayerPrefs.SetInt("ChstEquip", 0);
            PlayerPrefs.SetInt("OthrEquip", 0);
            *
            PlayerPrefs.SetString("helmUnlocked", "1010");
            PlayerPrefs.SetString("chestUnlocked", "1100");
            PlayerPrefs.SetString("otherUnlocked", "100");
            */
        }


        ReLoadPlayerOutfitData();

    }

    // Update is called once per frame
    void Update()
    {
        

    }

    //The following functions are called when the player changes an armour piece

    public void ChangeHelm(int tochangeValue)
    {
        currentHelm += tochangeValue;
        if (currentHelm > totalHelm) { currentHelm = 0; }
        else if (currentHelm < 0) { currentHelm = totalHelm; }

        foreach(GameObject obj in helmMDL)
        {
            obj.SetActive(false);
        }
        helmMDL[currentHelm].SetActive(true);

    }

    public void ChangeChest(int tochangeValue)
    {
        currentChest += tochangeValue;
        if (currentChest > totalChest) { currentChest = 0; }
        else if (currentChest < 0) { currentChest = totalChest; }
    }

    public void ChangeOther(int tochangeValue)
    {
        currentOther += tochangeValue;
        if (currentOther > totalOther) { currentOther = 0; }
        else if (currentOther < 0) { currentOther = totalOther; }
    }


    public void ConfirmOutfit()
    {

        //Needs to check if it is unlocked.
        /*
            ChestUnlocked = 1000;
            Chest piece in slot "0" is unclocke, 1, but all other are not, 0.

            playerprefs.setStr("chestUnlocked", 1000);
            chestUnlockedStr //is a local str// = playerprefs.GetStr("chestUnlocked");
            if(chestUnlocked[currentChest] == 1)
            {
                //set as chest
            }

        *
        print("Helm number: " + currentHelm);
        print("Is Unlocked: " + helmUnlocked[currentHelm]);
        */

        //int helmUnInt =  char.GetNumericValue(helmUnlocked[currentHelm]);

        if (char.GetNumericValue(helmUnlocked[currentHelm])/*helmUnInt helmUnlocked[currentHelm]*/ > 0)
        {
            print("Confirmed equip. " + helmUnlocked[currentHelm]);
            PlayerPrefs.SetInt("HelmEquip", currentHelm);
        }

        else
        {
            currentHelm = PlayerPrefs.GetInt("HelmEquip");
            print("Failed to equip Helm to player, Item not unlocked.");
        }

        /*
        else
        {
            PlayerPrefs.SetInt("HelmEquip", PlayerPrefs.GetInt("HelmEquip"));
        }
        */

        if (char.GetNumericValue(chestUnlocked[currentChest]) > 0)
        {
            //set as chest
            PlayerPrefs.SetInt("ChstEquip", currentChest);
        }
        else
        {
            currentChest = PlayerPrefs.GetInt("ChstEquip");
            print("Failed to equip Chest to player, Item not unlocked.");
        }

        if (char.GetNumericValue(otherUnlocked[currentOther]) > 0)
        {
            PlayerPrefs.SetInt("OthrEquip", currentOther);
        }
        else
        {
            currentOther = PlayerPrefs.GetInt("OthrEquip");
            print("Failed to equip Other to player, Item not unlocked.");
        }


    }

    public void ReLoadPlayerOutfitData()
    {
        helmUnlocked = PlayerPrefs.GetString("helmUnlocked");
        chestUnlocked = PlayerPrefs.GetString("chestUnlocked");
        otherUnlocked = PlayerPrefs.GetString("otherUnlocked");
        PlayerPrefs.SetInt("HelmEquip", currentHelm);
        PlayerPrefs.SetInt("ChstEquip", currentChest);
        PlayerPrefs.SetInt("OthrEquip", currentOther);

        totalHelm = helmUnlocked.Length - 1;
        totalChest = chestUnlocked.Length - 1;
        totalOther = otherUnlocked.Length - 1;
    }

    /*
    public void LoopDeactiveActive(LinkedList objList)
    {

    }*/
}
