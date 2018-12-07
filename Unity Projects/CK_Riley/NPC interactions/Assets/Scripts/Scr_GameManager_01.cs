using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scr_GameManager_01 : MonoBehaviour {

    public GameObject player;
    [Space(5)]
    public Image textboxImage, answerImage;
    private CanvasGroup textboxGrp, answerGrp;
    public GameObject btn_Tp, btn_MdTp, btn_MdBt, btn_Bt;
    public bool canAnswer = false, close = false, activated = false, interactedWith = false;


	// Use this for initialization
	void Start ()
    {
        textboxGrp = textboxImage.GetComponent<CanvasGroup>();
        answerGrp = answerImage.GetComponent<CanvasGroup>();
        canAnswer = close = interactedWith = activated = false;
        //textboxImage.enabled = answerImage.enabled = (false);
        InteractionBoxStart();
        //AnswerBox(false);

    }
	
	// Update is called once per frame
	void Update ()
    {
        
        if(close == true)
        {
            if (activated == true)
            {
                if (interactedWith == true)
                {
                    AnswerBox(true);
                    //interactedWith = true;
                }
                else
                {
                    AnswerBox(false);
                }
            }
        }
        else
        {
            InteractionBoxStart();
            interactedWith = false;
        }
        
	}

    public void InteractionBoxStart()
    {
        btn_Tp.GetComponent<Button>().interactable = btn_MdTp.GetComponent<Button>().interactable = btn_MdBt.GetComponent<Button>().interactable = btn_Bt.GetComponent<Button>().interactable = false;
        btn_Tp.active = btn_MdTp.active = btn_MdBt.active = btn_Bt.active = true;
        textboxGrp.interactable = false;
        answerGrp.interactable = false;
        activated = false;
        answerGrp.alpha = textboxGrp.alpha = 0;
        
    }

    public void Interaction()
    {
        AnswerBox(false);
    }

    public void AnswerBox(bool isActive)
    {
        btn_Tp.GetComponent<Button>().interactable = btn_MdTp.GetComponent<Button>().interactable = btn_MdBt.GetComponent<Button>().interactable = btn_Bt.GetComponent<Button>().interactable = isActive;
        textboxGrp.interactable = !isActive;
        answerGrp.interactable = isActive;
        {
            if (isActive == true)
            {
                textboxGrp.alpha = 0;
                answerGrp.alpha = 1;
            }
            else
            {
                textboxGrp.alpha = 1;
                answerGrp.alpha = 0;
            }
        }//Set CanvasGroup alphas
        

    }

    public void ReplyOptions(string reply_00, string reply_01, string reply_02, string reply_03)
    {

        if (reply_00 == "" || reply_00 == "null" || reply_00 == null)
        {
            //disable that option
            btn_Tp.active= btn_MdTp.active = btn_MdBt.active = btn_Bt.active     = false;
            close = false;
        }
        else
        {
            btn_Tp.active = true;
            btn_Tp.GetComponentInChildren<Text>().text = reply_00;
        }
        if (reply_01 == "" || reply_01 == "null")
        {
            //disable that option
            btn_MdTp.active = btn_MdBt.active = btn_Bt.active = false;

        }
        else
        {
            btn_MdTp.active = true;
            btn_MdTp.GetComponentInChildren<Text>().text = reply_01;
        }

        if (reply_02 == "" || reply_02 == "null")
        {
            //disable that option
            btn_MdBt.active = btn_Bt.active = false;

        }
        else
        {
            btn_MdBt.active = true;
            btn_MdBt.GetComponentInChildren<Text>().text = reply_02;
        }

        if (reply_03 == "" || reply_03 == "null")
        {
            //disable that option
            btn_Bt.active = false;
        }
        else
        {
            btn_Bt.active = true;
            btn_Bt.GetComponentInChildren<Text>().text = reply_03;
        }

    }
}
