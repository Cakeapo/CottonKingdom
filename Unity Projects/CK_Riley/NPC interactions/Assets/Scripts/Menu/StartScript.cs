using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScript : MonoBehaviour {

    private string sceneName;

    private void Start()
    {
        Time.timeScale = 1;
    }

    public void ChangeScene(int sceneName)
    {
        Application.LoadLevel(sceneName);

    }

    
}
