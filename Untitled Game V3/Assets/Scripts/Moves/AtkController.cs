using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtkController : MonoBehaviour
{
    GameObject BS;
    GameObject PP;

    public Transform miniGame;
    public GameObject AtkGame;
    GameObject Notes;
    BattleSystem BatSys;
    private void Start()
    {
        BS = GameObject.Find("Battle System");
    }

    public IEnumerator StartGame()
    {
        LSController.points = 0;
        BatSys = BS.GetComponent<BattleSystem>();
        BattleSystem.waitTime = 3f;
        BatSys.OnLSButton();
        Notes = Instantiate(AtkGame, miniGame);

        yield return new WaitForSeconds(2.9f);
        Destroy(Notes);
        if (LSController.points > 0)
        {
            BattleSystem.DisplayTxt = "Hit for " + BattleSystem.currentPlayerDmg * LSController.points + " damage!";
        }
        else
        {
            BattleSystem.DisplayTxt = "The attack missed...";
        }
        BattleSystem.eHPChange = LSController.points * -BattleSystem.currentPlayerDmg;
        BattleSystem.eMPChange = 0;
        BattleSystem.pHPChange = 0;
        BattleSystem.pMPChange = 0;
    }
    public void OnButton()
    {
        StartCoroutine(StartGame());
    }
}
