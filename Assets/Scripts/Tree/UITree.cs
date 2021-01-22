using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITree : MonoBehaviour
{
    //This class is needed to keep Trees between scenes. 

    public static UITree Instance { get; private set; }

    void Start()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }
}
