using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_GameToHome_01 : MonoBehaviour {

    private string sceneNumber;

    public void ChangeScene(int sceneNumber)
    {
        Application.LoadLevel(sceneNumber);

    }

    
}
