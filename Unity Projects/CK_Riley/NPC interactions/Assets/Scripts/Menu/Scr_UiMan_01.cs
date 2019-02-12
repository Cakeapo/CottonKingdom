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
    public Animator invUIAnim0, invUIAnim1;
    bool invUIbool = false;
    public bool testing, inMenu;

    void Start()
    {
        if(testing == true)
        {
            inventoryCanvasUI.enabled = false;
        }
        invUIAnim0 = inventoryCanvasUI.transform.GetChild(0).gameObject.GetComponent<Animator>();
        invUIAnim1 = inventoryCanvasUI.transform.GetChild(1).gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) == true)
        {
            InventoryAnim();
        }
    }

    void InventoryAnim()
    {
        invUIbool = !invUIbool;
        inMenu = invUIbool; //works for temporary, once there are several menues it needs to check
                            // if there is another menu activated
        invUIAnim1.SetBool("IsUp", invUIbool);
        invUIAnim0.SetBool("IsUp", invUIbool);


    }
}
