﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DSController : PlayerMoves
{
    
    public int dmg = 4;
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
        if (BattleSystem.points <= 0)
            BattleSystem.points = 0;
        if (player.drkChrg >= 3)
        {
            BS.EDoHPChange(BattleSystem.points * -dmg * 2, Unit.Types.Dark);
            BS.PDoHPChange(BattleSystem.points * dmg / 2 *2, Unit.Types.Neutral);
            player.drkChrg = 0;
            player.CheckType(true);
        }
        else
        {
            BS.EDoHPChange(BattleSystem.points * -dmg, Unit.Types.Dark);
            BS.PDoHPChange(BattleSystem.points * dmg / 2, Unit.Types.Neutral);
            player.drkChrg += 1;
            player.CheckType(false);
        }
        BS.PDoChrg(player, Unit.Types.Dark);

        BS.PDoMPChange(-1);
        BS.ToggleUIBox(true);
        if (BattleSystem.points > 0)
        {
            //BS.DoTxt("Hit for " + dmg * BattleSystem.points + " damage!");
            BS.DoTxt("Skylar used "+Name+"!");
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
}