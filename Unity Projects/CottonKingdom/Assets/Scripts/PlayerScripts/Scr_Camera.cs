using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Camera : MonoBehaviour
{
    public static Scr_Camera instance;

    public Transform TargetLookAt;

    void Awake()
    {
        instance = this;
    }

    void Start ()
    {
		
	}
	
	void Update ()
    {
		
	}

    public static void UseExistingOrCreateNewMainCamera()
    {
        GameObject tempCamera;
        GameObject targetLookAt;
        Scr_Camera myCamera;

        if(Camera.main != null)
        {
            tempCamera = Camera.main.gameObject;
        }
        else
        {
            tempCamera = new GameObject("Main Camera");
            tempCamera.AddComponent<Camera>();
            tempCamera.tag = "MainCamera";
        }

        tempCamera.AddComponent<Scr_Camera>();
        myCamera = tempCamera.GetComponent<Scr_Camera>();

        targetLookAt = GameObject.Find("targetLookAt");

        if(targetLookAt == null)
        {
            targetLookAt = new GameObject("targetLookAt");
            targetLookAt.transform.position = Vector3.zero;
        }

        myCamera.TargetLookAt = targetLookAt.transform;
    }
}
