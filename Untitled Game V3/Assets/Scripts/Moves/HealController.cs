using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealController : MonoBehaviour
{
    GameObject BS;

    public Transform miniGame;
    public GameObject HealGame;
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
        Notes = Instantiate(HealGame, miniGame);

        yield return new WaitForSeconds(2.9f);
        Destroy(Notes);
        if (LSController.points > 0)
        {
            BattleSystem.DisplayTxt = "Healed " + 10 * LSController.points + " Health!";
        }
        else
        {
            BattleSystem.DisplayTxt = "The Spell Failed...";
        }
        BattleSystem.eHPChange = 0;
        BattleSystem.eMPChange = 0;
        BattleSystem.pHPChange = LSController.points * 10;
        BattleSystem.pMPChange = -1;
    }
    public void OnButton()
    {
        StartCoroutine(StartGame());
    }
}
