using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyCameras : MonoBehaviour
{

    public bool isEntire = true;
    
    private static DontDestroyCameras _Instance;
    private void Awake()
    {
        if (!isEntire)
        {
            return;
        }
        
        if(_Instance == null)
        {    
            _Instance = this; // In first scene, make us the singleton.
            DontDestroyOnLoad(gameObject);
        }
        else if(_Instance != this)
            Destroy(gameObject);
    }
}
