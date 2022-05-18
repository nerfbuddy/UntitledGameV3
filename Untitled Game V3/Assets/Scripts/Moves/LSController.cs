using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LSController : MonoBehaviour
{
    public static int points = 0;
    GameObject BS;

    public Transform miniGame;
    public GameObject LSGame;
    GameObject Notes;
    BattleSystem BatSys;
    private void Start()
    {
        BS = GameObject.Find("Battle System");
    }

    public IEnumerator StartGame()
    {
        points = 0;
        BatSys = BS.GetComponent<BattleSystem>();
        BattleSystem.waitTime = 1.1f;
        BatSys.OnLSButton();
        Notes = Instantiate(LSGame, miniGame);

        yield return new WaitForSeconds(1f);
        Destroy(Notes);
        if (points > 0)
        {
            BattleSystem.DisplayTxt = "Hit for " + 60 * points + " damage!";
        }
        else
        {
            BattleSystem.DisplayTxt = "The attack missed...";
        }
        BattleSystem.eHPChange = points * -60;
        BattleSystem.eMPChange = 0;
        BattleSystem.pHPChange = 0;
        BattleSystem.pMPChange = -1;
    }
    public void OnButton()
    {
        StartCoroutine(StartGame());
    }
}
