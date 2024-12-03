using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void tryagain()
    {
        SceneManager.LoadSceneAsync("OverworldScene");
    }
    public void doExitGame()
    {
        Application.Quit();
    }
}
