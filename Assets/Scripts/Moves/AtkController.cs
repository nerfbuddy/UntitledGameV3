using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AtkController : PlayerMoves
{

    private void Start()
    {
        color = new Color32(255, 0, 0, 255);
        OnStart();
        player.allMoves.Add(this);
    }

    public IEnumerator StartGame()
    {
        BattleSystem.points = 0;

        BS.ToggleButtons(false);
        BS.ToggleUIBox(false);
        Notes = Instantiate(MoveGame, miniGame);

        yield return new WaitForSeconds(2.9f);
        Destroy(Notes);
        if (player.phyChrg >= 3)
        {
            BS.EDoHPChange(BattleSystem.points * -player.damage * 2, Unit.Types.Physical);
            player.phyChrg = 0;
            player.CheckType(true);
        }
        else
        {
            BS.EDoHPChange(BattleSystem.points * -player.damage, Unit.Types.Physical);
            player.phyChrg += 1;
            player.CheckType(false);
        }
        BS.PDoChrg(player, Unit.Types.Physical);
        BS.ToggleUIBox(true);
        if (BattleSystem.points > 0)
        {
            //BS.DoTxt("Hit for " + player.damage * BattleSystem.points + " damage!");
            BS.DoTxt("Skylar used " + Name + "!");
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
    public override void PickTarget()
    {
        BS.ToggleButtons(false);
        BS.ToggleUIBox(false);
        BS.ToggleTargetBox(true);
        BS.targetButtons[0].GetComponent<Button>().onClick.AddListener(OnButton);
        BS.targetButtons[1].GetComponent<Button>().onClick.AddListener(OnButton);
        BS.targetButtons[2].GetComponent<Button>().onClick.AddListener(OnButton);
        BS.targetButtons[3].GetComponent<Button>().onClick.AddListener(OnButton);
        
    }
}
