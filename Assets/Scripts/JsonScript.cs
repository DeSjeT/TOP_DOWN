using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class JsonScript : MonoBehaviour
{
    [SerializeField]
    private GameObject _gameObj;
        
    static public string Path;
    
    [Serializable]
    public class Data
    {
        public Vector3 spawnPoint;
    }
    [Serializable]
    public class GameData {
        public List<Data> Items = new List<Data> ();
    }
    
    //LOAD CHESTS FROM JSON
    void Start()
    {
        Path = System.IO.Path.Combine(Application.dataPath, "save1.sjon");
        SaveSpawnPosition();
        var myData = LoadJSON.LoadingJSON<GameData>();
        foreach (var VARIABLE in myData.Items)
        {
            Instantiate(_gameObj, VARIABLE.spawnPoint, Quaternion.identity);
            //print("Position " + VARIABLE.spawnPoint);
        }
    }

    void SaveSpawnPosition()
    {
        GameData myData = new GameData();
        myData.Items.Add(new Data() {spawnPoint = new Vector3(-4, 2f, 2.89f)});
        myData.Items.Add(new Data() {spawnPoint = new Vector3(-4, 2f, 6.28f)});
        SaveJSON.SaveJSONFile(myData);
    }

    private void OnApplicationQuit()
    {
        //SaveJSON.SaveJSONFile(myData);
    }
}
