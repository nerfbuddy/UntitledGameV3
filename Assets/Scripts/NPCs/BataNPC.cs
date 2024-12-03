using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BataNPC : DialogueActivator
{
    private bool run = false;
    public GameObject[] UnitPrefab;
    
    public void StartTutorial()
    {
        StartCoroutine(StartTheBattle());
    }
    public IEnumerator StartTheBattle()
    {
        run = true;
        while (run)
        {
            if (DialogueUI.IsOpen)
            {
                yield return null;
            }
            else
            {
                run = false;
                DontDestroyOnLoad(gameObject);
                SceneManager.LoadScene("BattleScene");
                BattleSystem.enemyPrefab = UnitPrefab;
                StartCoroutine(kill(0.01f));
            }
        }
    }

    public IEnumerator kill(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
    public void SetMoves()
    {
        
        for (int i = 0; i < playerUnit.allMoves.Count; i++)
        {
            if(playerUnit.allMoves[i].Name == "Quad Slash")
            {
                playerUnit.actvMoves[1] = playerUnit.allMoves[i];
                playerUnit.playerdata.actvMoves[1] = playerUnit.allMoves[i].Name;
            }
            else if (playerUnit.allMoves[i].Name == "Dark Slash")
            {
                playerUnit.actvMoves[0] = playerUnit.allMoves[i];
                playerUnit.playerdata.actvMoves[0] = playerUnit.allMoves[i].Name;
            }
            else if (playerUnit.allMoves[i].Name == "Light Slash")
            {
                playerUnit.actvMoves[3] = playerUnit.allMoves[i];
                playerUnit.playerdata.actvMoves[3] = playerUnit.allMoves[i].Name;
            }
            else if (playerUnit.allMoves[i].Name == "Healing Melody")
            {
                playerUnit.actvMoves[2] = playerUnit.allMoves[i];
                playerUnit.playerdata.actvMoves[2] = playerUnit.allMoves[i].Name;
            }
        }
        
    }
}
