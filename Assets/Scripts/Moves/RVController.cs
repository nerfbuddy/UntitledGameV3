using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RVController : PlayerMoves
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
        if (player.lghtChrg > 0)
        {
            BattleSystem.points = 0;
            BS.ToggleButtons(false);
            BS.ToggleUIBox(false);
            Notes = Instantiate(MoveGame, miniGame);

            yield return new WaitForSeconds(4.9f);
            Destroy(Notes);
            
            BS.PDoMPChange(BattleSystem.points);
            player.lghtChrg -= 1;
            
            BS.PDoChrg(player, Unit.Types.Light);
            BS.ToggleUIBox(true);
            if (BattleSystem.points > 0)
            {
                //BS.DoTxt("Recovered " + BattleSystem.points / 2 + " MP!");
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
            OuttaLC();
        }

    }
    void OnButton()
    {
        StartCoroutine(StartGame());
    }
}
