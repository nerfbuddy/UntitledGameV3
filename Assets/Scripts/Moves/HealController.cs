using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealController : PlayerMoves
{
    private void Start()
    {
        color = new Color32(0, 245, 211, 255);
        OnStart();
        player.allMoves.Add(this);
    }
    public override void PickTarget()
    {
        OnButton();
    }
    public IEnumerator StartGame()
    {
        if(player.currentMP > 0)
        {
            BattleSystem.points = 0;
            BS.ToggleButtons(false);
            BS.ToggleUIBox(false);
            Notes = Instantiate(MoveGame, miniGame);

            yield return new WaitForSeconds(3.2f);
            Destroy(Notes);
            if (player.lghtChrg >= 3)
            {
                BS.PDoHPChange(BattleSystem.points * 8, Unit.Types.Neutral);
                player.lghtChrg = 0;
                player.CheckType(true);
            }
            else
            {
                BS.PDoHPChange(BattleSystem.points * 8, Unit.Types.Neutral);
                player.lghtChrg += 1;
                player.CheckType(false);
            }
            BS.PDoMPChange(-1);
            BS.PDoChrg(player, Unit.Types.Light);
            BS.ToggleUIBox(true);
            if (BattleSystem.points > 0)
            {
                //BS.DoTxt("Healed " + 5 * BattleSystem.points + " Health!");
                BS.DoTxt("Skylar used " + Name + "!");
            }
            else
            {
                BS.DoTxt("The Spell Failed...");
            }
            yield return new WaitForSeconds(2f);
            BS.NextTurn();
        }
        else
        {
            OuttaMP();
        }
    }

    void OnButton()
    {
        StartCoroutine(StartGame());
    }
}
