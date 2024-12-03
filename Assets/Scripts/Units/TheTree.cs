using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheTree : Unit
{
    /*
    int randint = 0;
    void Update()
    {
        if (BattleSystem.eTurn)
        {
            randint = Random.Range(1, 5);
            if (currentMP >= 10 && currentHP <= 60)
            {
                Heal();
            }
            else
            {
                if(currentMP >= 15 && randint > 2)
                {
                    MAttack();
                }
                else
                {
                    Attack();
                }
            }
            BattleSystem.eTurn = false;
        }
    }
    void Attack()
    {
        BattleSystem.DisplayTxt = unitName + " attacks!";
        BattleSystem.pHPChange = -damage;
    }
    void Heal()
    {
        BattleSystem.DisplayTxt = unitName + " Heals 80 health";
        currentMP -= 10;
    }
    void MAttack()
    {
        BattleSystem.DisplayTxt = unitName + " Uses thier Magic Attack and does 90 damage!";
        currentMP -= 15;
        BattleSystem.pHPChange = -90;
    }
    */
}
