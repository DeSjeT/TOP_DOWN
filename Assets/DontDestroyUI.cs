using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyUI : MonoBehaviour
{
    static DontDestroyUI instance;
    
    public void Awake()
    {
        if(instance == null)
        {    
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else 
        if(instance != this) Destroy(gameObject);
    }
}
