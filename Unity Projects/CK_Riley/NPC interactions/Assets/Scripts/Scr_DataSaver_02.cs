using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_DataSaver_02 : MonoBehaviour {


    public int returnInt;
	// Use this for initialization

    public void DataSaveInt(string dataName,  int dataValueInt)
    {
        PlayerPrefs.SetInt(dataName, dataValueInt);
    }

    public int DataGetInt(string dataName)
    {
        //returnInt = PlayerPrefs.GetInt(dataName, 0);
        //ReturnInt();
        //return returnInt;
        if (PlayerPrefs.HasKey(dataName))
        {
            return PlayerPrefs.GetInt(dataName, 0);
        }
        else
        {
            PlayerPrefs.SetInt(dataName, 0);
            return 0;
        }
    }
    /*
    public int ReturnInt()
    {
        return returnInt;
    }*/
}
