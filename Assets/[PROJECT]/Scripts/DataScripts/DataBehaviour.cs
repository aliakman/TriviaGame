using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataBehaviour : MonoBehaviour
{
    public GameData gameData;

    private void OnEnable()
    {
        DataManager.LoadData(gameData);
    }

    private void OnDisable()
    {
        DataManager.SaveData(gameData);
    }


    private void OnApplicationFocus(bool focus)
    {
        if (!focus)
            DataManager.SaveData(gameData);
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
            DataManager.SaveData(gameData);
    }

    private void OnApplicationQuit()
    {
        DataManager.SaveData(gameData);
    }
}
