using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Victor : Unit
{
    [SerializeField] public DialogueObject firstTurn;
    [SerializeField] public DialogueObject smackTalk;
    //bool half = false;
    enum Combos { PLDischarge, Heal, None}
    Combos combo;
    int randint = 0;
    int count = 0;
    public override void MyTurn()
    {
        randint = Random.Range(1, 5);

        if (playerUnit.lghtChrg == 3)
            combo = Combos.PLDischarge;
        else if (currentHP < 20)
            combo = Combos.Heal;
        else
            combo = Combos.None;
        switch(combo)
        {
            case Combos.Heal:
                switch (count)
                {
                    case 0:
                        StartCoroutine(Crystalize());
                        count += 1;
                        break;
                    case 1:
                        StartCoroutine(Hold());
                        count = 0;
                        combo = Combos.None;
                        break;
                }
                break;
            case Combos.PLDischarge:
                switch(count)
                {
                    case 0:
                        StartCoroutine(Crystalize());
                        count += 1;
                        break;
                    case 1:
                        StartCoroutine(Hold());
                        count = 0;
                        combo = Combos.None;
                        break;
                }
                break;
            case Combos.None:
                KillCheck();
                break;
        }
            
    }

    IEnumerator Hold()
    {
        BS.DoTxt(unitName + " Must Recharge!");
        yield return new WaitForSeconds(1.5f);
        BS.NextTurn();
    }
    IEnumerator Attack()
    {

        BS.DoTxt(unitName + " attacks!");
        yield return new WaitForSeconds(1.5f);
        if (phyChrg >= 3)
        {
            phyChrg = 0;
            BS.PDoHPChange(-damage * 2, Unit.Types.Physical);
        }
        else
        {
            BS.PDoHPChange(-damage, Unit.Types.Physical);
            phyChrg += 1;
        }
        HUD.SetChrg(Unit.Types.Physical, this);
        BS.NextTurn();
    }

    IEnumerator DarkAttack()
    {
        BS.DoTxt(unitName + " Uses Leech Life!");
        yield return new WaitForSeconds(2f);
        if (drkChrg >= 3)
        {
            drkChrg = 0;
            BS.PDoHPChange(-damage, Unit.Types.Dark);
            BS.EDoHPChange(4 * damage / 3, Unit.Types.Neutral);
        }
        else
        {
            BS.PDoHPChange(-damage, Unit.Types.Dark);
            BS.EDoHPChange(2 * damage / 3, Unit.Types.Neutral);
            drkChrg += 1;
        }
        HUD.SetChrg(Unit.Types.Dark, this);
        BS.EDoMPChange(-2);

        BS.NextTurn();
    }
    IEnumerator Crystalize()
    {
        BS.DoTxt(unitName + " Uses Crystalize!");
        yield return new WaitForSeconds(2f);

        phyChrg = 3;
        BS.EDoHPChange(25, Types.Neutral);
        //Skip Next turn
        HUD.SetChrg(Unit.Types.Physical, this);
        BS.NextTurn();

    }
    IEnumerator Regenerate()
    {
        BS.DoTxt(unitName + " Uses Regenerate!");
        yield return new WaitForSeconds(1.5f);
        if (drkChrg >= 3)
        {
            drkChrg = 0;
            BS.EDoMPChange(12);
        }
        else
        {
            BS.EDoHPChange(-20, Unit.Types.Neutral);
            BS.EDoMPChange(12);
            drkChrg += 1;
        }
        HUD.SetChrg(Unit.Types.Dark, this);
        BS.NextTurn();
    }
    void KillCheck()
    {
        switch (playerUnit.MyType)
        {
            case Types.Physical:
                if (currentMP > 2)
                    StartCoroutine(DarkAttack());
                else
                    StartCoroutine(Regenerate());
                break;
            case Types.Dark:
                if (currentMP > 2)
                    StartCoroutine(DarkAttack());
                else
                    StartCoroutine(Regenerate());
                break;
            case Types.Light:
                if (phyChrg == 3)
                    StartCoroutine(Attack());
                else if (currentHP <= currentHP - 25)
                    combo = Combos.Heal;
                else
                    StartCoroutine(Attack());
                break;
            default:
                if (currentMP > 2)
                {
                    StartCoroutine(DarkAttack());
                }
                else
                    Attack();
                break;
        }
    }
}
