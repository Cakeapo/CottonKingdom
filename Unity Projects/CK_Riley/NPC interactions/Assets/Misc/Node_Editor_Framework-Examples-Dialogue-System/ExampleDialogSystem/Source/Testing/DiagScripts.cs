using UnityEngine;

public class DiagScripts : MonoBehaviour
{
    [SerializeField]
    private DialogManager _dialogManager;
    public int DialogIdToLoad = 1;
    public float value = 0.1f;

    public DialogNodeCanvas dialogCanvas;

    public bool input;

    void Start()
    {
        DialogBlackboard.SetValue(DialogBlackboard.EDialogMultiChoiceVariables.TryingThisToo, value);
    }

    void Update()
    {
        //Load
        if (input == true && Input.GetButtonDown("Interact"))
        {
            _dialogManager.dialogCanvas = dialogCanvas;
            _dialogManager.Reload();
            Debug.Log("TestScript Update about to load dialog " + DialogIdToLoad);
            _dialogManager.ShowDialogWithId(DialogIdToLoad, true);
        }

        //Load
        if (Input.GetKeyDown(KeyCode.A))
        {
            value += 0.1f;
            DialogBlackboard.SetValue(DialogBlackboard.EDialogMultiChoiceVariables.TryingThisToo, value);
        }

        ////Data
        //if (Input.GetKey(KeyCode.D))
        //{
        //    //BaseDialogNode node = _dialogManager.GetNodeForID(DialogIdToLoad);
        //    //Debug.Log("Character name : " + node.SayingCharacterName);
        //    //Debug.Log("Character says : " + node.WhatTheCharacterSays);
        //    //Debug.Log("Character portrait name : " + node.SayingCharacterPotrait.name);
        //}

        ////Next
        //if (Input.GetKey(KeyCode.N))
        //{
        //    //_dialogManager.GiveInputToDialog(DialogIdToLoad, EDialogInputValue.Next);
        //}
    }
}
