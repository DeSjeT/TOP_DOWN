using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class TeleportAndLoadScene : MonoBehaviour
{
    public GameObject spawnPosition;
    
    void Start()
    {
        //StartCoroutine(WaitFor());
        
        if (spawnPosition)
        {
            var obj = GameObject.FindGameObjectWithTag("Player").GetComponent<NavMeshAgent>();
            obj.Warp(spawnPosition.transform.position);
        }
    }

    // IEnumerator WaitFor()
    // {
    //     yield return new WaitForEndOfFrame();
    //     if (spawnPosition)
    //     {
    //         var obj = GameObject.FindGameObjectWithTag("Player").GetComponent<NavMeshAgent>();
    //         obj.Warp(spawnPosition.transform.position);
    //     }
    //     
    // }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (SceneManager.GetActiveScene().name == "MainScene")
            {
                SceneManager.LoadScene(1);
            }
            else
            {
                SceneManager.LoadScene(0);
            } 
        }
    }
}
