using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scr_ResetGame_01 : MonoBehaviour
{

    //poupu ui and confirm that you want to delet your progress
    public bool confirmed;
    public CanvasGroup resetGrp;


    // Start is called before the first frame update
    void Start()
    {
        confirmed = false;
        CanvasEnabled(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(confirmed == true)
        {
            ResetGame();
        }
    }

    public void Confirm()
                //calls up the confirm button to reset game
    {
        CanvasEnabled(true);
    }

    public void CanvasEnabled(bool Enabled)
                //either shows or hides the cnfirm reset button with true or false resp
    {
        if (Enabled == false)
        {
            resetGrp.alpha = 0;
            resetGrp.interactable = false;
            resetGrp.blocksRaycasts = false;

        }
        else
        {
            resetGrp.alpha = 1;
            resetGrp.interactable = true;
            resetGrp.blocksRaycasts = true;

        }
    }

    public void ResetConfirm()
                //Button returns a confirm and allows for reset
    {
        confirmed = true;
    }

    public void ResetGame()
                //resets the game.
    {
        /*
          here we need to have a list of all items that need to be reset.

        eg; PlayerPrefs.SetInt("Respawn_Tester", 0); this sets "Respawn_Tester" to 0
        */
        PlayerPrefs.SetInt("Respawn_Tester", 0);
        print("Reset game.");
        CanvasEnabled(false);
        confirmed = false;

    }

    
}
