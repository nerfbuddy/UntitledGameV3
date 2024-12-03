using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChargeSwap : PlayerMoves
{
    private void Start()
    {
        color = new Color32(130, 0, 190, 255);
        OnStart();
        player.allMoves.Add(this);
    }
    public override void PickTarget()
    {

        if (player.currentMP > 0)
        {
            BS.ToggleButtons(false);
            BS.ToggleUIBox(false);
            BS.ToggleTargetBox(true);
            BS.targetButtons[0].GetComponentInChildren<Button>().onClick.AddListener(OnButton);
            BS.targetButtons[1].GetComponentInChildren<Button>().onClick.AddListener(OnButton);
            BS.targetButtons[2].GetComponentInChildren<Button>().onClick.AddListener(OnButton);
            BS.targetButtons[3].GetComponentInChildren<Button>().onClick.AddListener(OnButton);
        }
        else
        {
            OuttaMP();
        }
    }
    public IEnumerator StartGame()
    {
        BattleSystem.points = 0;
        BS.ToggleButtons(false);
        BS.ToggleUIBox(false);
        Notes = Instantiate(MoveGame, miniGame);

        yield return new WaitForSeconds(1.9f);
        Destroy(Notes);



        BS.PDoMPChange(-1);
        BS.ToggleUIBox(true);
        if (BattleSystem.points > 0)
        {
            BS.DoTxt("Skylar used " + Name + "!");
        }


        yield return new WaitForSeconds(2f);
        BS.NextTurn();

    }

    void OnButton()
    {
        StartCoroutine(StartGame());
        BS.targetButtons[0].GetComponent<Button>().onClick.RemoveListener(OnButton);
        BS.targetButtons[1].GetComponent<Button>().onClick.RemoveListener(OnButton);
        BS.targetButtons[2].GetComponent<Button>().onClick.RemoveListener(OnButton);
        BS.targetButtons[3].GetComponent<Button>().onClick.RemoveListener(OnButton);
        BS.ToggleTargetBox(false);
    }
    void StealCharge()
    {
        if (BS.enemyUnit[BattleSystem.targetedEnemy].drkChrg > 0 && player.drkChrg < 3)
        {
            BS.enemyUnit[BattleSystem.targetedEnemy].drkChrg = player.drkChrg;
            player.drkChrg += 1;
            BS.enemyHUD[BattleSystem.targetedEnemy].SetChrg(Unit.Types.Dark, BS.enemyUnit[BattleSystem.targetedEnemy]);
            player.HUD.SetChrg(Unit.Types.Dark, player);
        }
        else if (BS.enemyUnit[BattleSystem.targetedEnemy].phyChrg > 0 && player.phyChrg < 3)
        {
            BS.enemyUnit[BattleSystem.targetedEnemy].phyChrg -= 1;
            player.phyChrg += 1;
            BS.enemyHUD[BattleSystem.targetedEnemy].SetChrg(Unit.Types.Physical, BS.enemyUnit[BattleSystem.targetedEnemy]);
            player.HUD.SetChrg(Unit.Types.Physical, player);
        }
        else if (BS.enemyUnit[BattleSystem.targetedEnemy].lghtChrg > 0 && player.lghtChrg < 3)
        {
            BS.enemyUnit[BattleSystem.targetedEnemy].lghtChrg -= 1;
            player.lghtChrg += 1;
            BS.enemyHUD[BattleSystem.targetedEnemy].SetChrg(Unit.Types.Light, BS.enemyUnit[BattleSystem.targetedEnemy]);
            player.HUD.SetChrg(Unit.Types.Light, player);
        }

    }
}
