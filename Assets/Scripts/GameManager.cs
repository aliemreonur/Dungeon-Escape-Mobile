﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    private static GameManager _instance;
    public static GameManager Instance
    {
        get { if(_instance == null)
            {
                Debug.LogError("Game Manager Instance is null");
            }
            return _instance;
        }
    }

    [SerializeField] public bool HasKeyToCastle { get; set; }

    public Player Player { get; private set; }

    private void Awake()
    {
        _instance = this;
        Player = FindObjectOfType<Player>().GetComponent<Player>();
       
    }
  
}
