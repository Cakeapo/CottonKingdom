using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scr_UiMan_01 : MonoBehaviour
{
    //self tag = "UI_Manager"

    //This manager is used to show what objects are collectd and when to show the appropriate UI's.
    //Such as having the popup UI show and having the collection UI popup show the corrosponding 
    // image to the item that was collected.

    public Canvas inventoryCanvasUI;
    public bool testing;

    void Start()
    {
        if(testing == true)
        {
            inventoryCanvasUI.enabled = false;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
