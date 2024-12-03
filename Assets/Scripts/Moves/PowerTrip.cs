using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerTrip : PlayerMoves
{
    public int dmg = 20;
    private void Start()
    {
        color = new Color32(130, 0, 190, 255);
        OnStart();
        player.allMoves.Add(this);
    }
    public override void PickTarget()
    {

        if (player.currentMP > 1)
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
        if (player.drkChrg >= 3)
        {
            BS.EDoHPChange(BattleSystem.points * -dmg * 2, Unit.Types.Dark);
            player.drkChrg = 0;
            player.CheckType(true);
        }
        else
        {
            BS.EDoHPChange(BattleSystem.points * -dmg, Unit.Types.Dark);
            player.drkChrg += 1;
            player.CheckType(false);
        }
        BS.PDoChrg(player, Unit.Types.Dark);
        BS.PDoMPChange(-3);
        BS.ToggleUIBox(true);
        if (BattleSystem.points > 0)
        {
            BS.DoTxt("Skylar used " + Name + "!");
            StealCharge();
        }
        else
        {
            BS.DoTxt("The attack missed...");
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
        //Debug.Log("trySteal");
        if (BS.enemyUnit[BattleSystem.targetedEnemy].drkChrg > 0 && player.drkChrg < 3)
        {
            //Debug.Log("darkSteal")
            BS.enemyUnit[BattleSystem.targetedEnemy].drkChrg -= 1;
            player.drkChrg += 1;
            BS.enemyHUD[BattleSystem.targetedEnemy].SetChrg(Unit.Types.Dark, BS.enemyUnit[BattleSystem.targetedEnemy]);
            player.HUD.SetChrg(Unit.Types.Dark, player);
        }
        else if(BS.enemyUnit[BattleSystem.targetedEnemy].phyChrg > 0 && player.phyChrg < 3)
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
