﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gameover : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void doExitGame()
    {
        SceneManager.LoadSceneAsync("StartGame");
    }

    public void tryagain()
    {
        SceneManager.LoadSceneAsync("OverworldScene");
    }
}