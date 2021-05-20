using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{

    public GameObject[] SpawnPoints;
    public GameObject ChestPrefab;
    public GameObject gameOverUI;
    public int countOfChests = 2;
    public int playerLevel = 0;

    private PlayerController _playerInfo;
    
    private static GameManager _GMinstance;
    

    #region Unity_block

    void Start()
    {
        _playerInfo = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        SpawnObjects();
    }

    public static GameManager GMinstance;
    private void Awake()
    {
        if(GMinstance == null)
        {    
            GMinstance = this;
            DontDestroyOnLoad(gameObject);
        }
        else 
        if(GMinstance != this) Destroy(gameObject);
    }
    
    #endregion

    private void SpawnObjects() //SPAWN CHESTS FOR THE FIRST TIME
    {
        if (SpawnPoints.Length < countOfChests)
        {
            countOfChests = SpawnPoints.Length;
        }
        
        for (int i = 0; i < countOfChests; i++)
        {
            var rand = Random.Range(0, SpawnPoints.Length);
            var spwnObj = Instantiate(ChestPrefab, SpawnPoints[rand].transform.position, Quaternion.identity);
            Destroy(SpawnPoints[rand]);
        }
        
        // for (int i = 0; i < SpawnPoints.Length; i++)
        // {
        //     var spwnObj = Instantiate(ChestPrefab, SpawnPoints[i].transform.position, Quaternion.identity);
        // }
    }

    public void GameOverUI()
    {
        Time.timeScale = 0.1f;
        gameOverUI.SetActive(true);
    }
    
    public void RestartGame()
    {
        Time.timeScale = 1f;
        gameOverUI.SetActive(false);

        _playerInfo.Health = 100;
        _playerInfo.ReciveDamage(0);
        // var objG = GameObject.FindGameObjectWithTag("Player");
        // if (objG)
        // {
        //     Destroy(objG);
        // }
        
        SceneManager.LoadScene(0);
    }
    
}
