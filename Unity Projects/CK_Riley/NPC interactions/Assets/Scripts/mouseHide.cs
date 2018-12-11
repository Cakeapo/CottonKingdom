﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseHide : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetButtonDown("Exit"))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
	}
}