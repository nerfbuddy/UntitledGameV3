using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheRock : Unit
{
    /*
    [SerializeField] public DialogueObject battleTxt;

    int randint = 0;

    private void Update()
    {
        if(BattleSystem.eTurn)
        {
            randint = Random.Range(1, 5);
            if (currentMP > 0 && randint < 2)
            {
                AtkBuff();
            }
            else
            {
                Attack();
            }
            BattleSystem.eTurn = false;
        }
        
    }

    void AtkBuff()
    {
        BattleSystem.DisplayTxt = unitName + " Buffs his attack!";
        currentMP -= 1;
        damage += 10;
        currentText = battleTxt;
        BattleSystem.tryToTalk = true;
    }
    void Attack()
    {
        BattleSystem.DisplayTxt = unitName + " attacks!";
        BattleSystem.pHPChange = -damage;
    }
    */
}
