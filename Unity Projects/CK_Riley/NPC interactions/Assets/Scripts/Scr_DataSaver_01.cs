using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_DataSaver_01 : MonoBehaviour {

    public void DataSaveInt(string dataName,  int dataValueInt)
    {
        PlayerPrefs.SetInt(dataName, dataValueInt);
    }

    public int DataGetInt(string dataName)
    {
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
}
