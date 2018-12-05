using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scr_GameManager_01 : MonoBehaviour {

    public GameObject player;
    [Space(5)]
    public Image textboxImage, answerImage;
    private CanvasGroup textboxGrp, answerGrp;
    public Button btn_Tp, btn_MdTp, btn_MdBt, btn_Bt;
    public bool canAnswer = false, close = false, interactedWith = false;


	// Use this for initialization
	void Start ()
    {
        textboxGrp = textboxImage.GetComponent<CanvasGroup>();
        answerGrp = answerImage.GetComponent<CanvasGroup>();
        canAnswer = close = interactedWith = false;
        //textboxImage.enabled = answerImage.enabled = (false);
        InteractionBoxStart();
        //AnswerBox(false);

    }
	
	// Update is called once per frame
	void Update ()
    {
        /*
        if(close == true && interactedWith == false)
        {
            AnswerBox(false);
            interactedWith = true;
        }
        else if( close == false && interactedWith == true)
        {
            InteractionBoxStart();
            interactedWith = false;
        }
        */
	}

    public void InteractionBoxStart()
    {
        btn_Tp.interactable = btn_MdTp.interactable = btn_MdBt.interactable = btn_Bt.interactable = false;
        textboxGrp.interactable = false;
        answerGrp.interactable = false;
        answerGrp.alpha = textboxGrp.alpha = 0;
    }

    public void Interaction()
    {
        AnswerBox(false);
    }

    public void AnswerBox(bool isActive)
    {
        btn_Tp.interactable = btn_MdTp.interactable = btn_MdBt.interactable = btn_Bt.interactable = isActive;
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
}
