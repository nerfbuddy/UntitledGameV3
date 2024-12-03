using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tanner : Unit
{
    //Attack
    //Presist
    //+3 light charge move low dmg
    //light slash type move with adds for discharge
    [SerializeField] DialogueObject diaLastWords;
    [SerializeField] DialogueObject diaPresist;
    [SerializeField] DialogueObject Cutter1;
    [SerializeField] DialogueObject Cutter2;
    [SerializeField] DialogueObject Cutter3;
    enum Combos { None, Presist, LightAttack }
    Combos combo;
    int randint = 0;
    int count = 0;
    public override void MyTurn()
    {
        randint = Random.Range(1, 5);

        switch (combo)
        {
            case Combos.Presist:
                switch (count)
                {
                    case 0:
                        StartCoroutine(Presist());
                        count += 1;
                        break;
                    case 1:
                        StartCoroutine(FuryCutter(Cutter1));
                        count += 1;
                        break;
                    case 2:
                        StartCoroutine(FuryCutter(Cutter2));
                        count += 1;
                        break;
                    case 3:
                        StartCoroutine(FuryCutter(Cutter3));
                        count += 1;
                        break;
                    case 4:
                        StartCoroutine(LastWords());
                        count = 0;
                        combo = Combos.None;
                        break;
                }
                break;
            case Combos.LightAttack:
                switch (count)
                {
                    case 0:
                        StartCoroutine(LightArrow());
                        count += 1;
                        break;
                    case 1:
                        StartCoroutine(SkyFacture());
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
    IEnumerator LastWords()
    {
        bool checking = false;
        BS.DoTalk(diaLastWords);
        checking = true;
        while (checking)
        {
            if (DialogueUI.IsOpen)
            {
                yield return null;
            }
            else
            {
                checking = false;
                yield return new WaitForSeconds(0f);
                currentHP = 0;
                BS.NextTurn();
                break;
            }
        }
    }
    IEnumerator Presist()
    {
        bool checking = false;
        BS.DoTalk(diaPresist);
        checking = true;
        while (checking)
        {
            if (DialogueUI.IsOpen)
            {
                yield return null;
            }
            else
            {
                checking = false;
                BS.DoTxt(unitName + " Uses Presist!");
                yield return new WaitForSeconds(1.5f);
                BS.NextTurn();
                break;
            }
        }

    }
    IEnumerator LightArrow()
    {
        BS.DoTxt(unitName + " Uses Light Arrow!");
        yield return new WaitForSeconds(1.5f);
        if (lghtChrg >= 3)
        {
            lghtChrg = 0;
            BS.PDoHPChange(-10 * 2, Unit.Types.Light);
        }
        else
        {
            BS.PDoHPChange(-10, Unit.Types.Light);
            lghtChrg = 3;
        }
        ChangeMP(-1);
        HUD.SetChrg(Unit.Types.Light, this);
        BS.NextTurn();
    }
    IEnumerator SkyFacture()
    {
        BS.DoTxt(unitName + " Uses Sky Facture!");
        yield return new WaitForSeconds(1.5f);
        if (lghtChrg >= 3)
        {
            lghtChrg = 0;
            BS.PDoHPChange(-damage, Unit.Types.Light);
            ChangeHP(10, Types.Neutral);
        }
        else
        {
            BS.PDoHPChange(-damage * 2, Unit.Types.Light);
            ChangeMP(-2);
        }
        HUD.SetChrg(Unit.Types.Light, this);
        BS.NextTurn();
    }
    IEnumerator FuryCutter(DialogueObject dialogue)
    {
        bool checking = false;
        BS.DoTalk(dialogue);
        checking = true;
        while (checking)
        {
            if (DialogueUI.IsOpen)
            {
                yield return null;
            }
            else
            {
                checking = false;
                BS.DoTxt(unitName + " Uses Fury Cutter!");
                yield return new WaitForSeconds(1.5f);
                if (playerUnit.MyType == Types.Dark)
                {
                    damage += 10;
                    BS.PDoHPChange(-damage, Unit.Types.Physical);
                }
                else
                {
                    damage += 7;
                    BS.PDoHPChange(-damage, Unit.Types.Physical);
                }
                HUD.SetChrg(Unit.Types.Physical, this);
                BS.NextTurn();
                break;
            }
        }
        
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
    void KillCheck()
    {
        switch (playerUnit.MyType)
        {
            case Types.Physical:
                StartCoroutine(Attack());
                break;
            default:
                if (currentMP > 0)
                {
                    combo = Combos.LightAttack;
                    count = 1;
                    StartCoroutine(LightArrow());
                }
                else
                    StartCoroutine(Attack());
                break;
        }
    }
    public override void ChangeHP(int amount, Types Type)
    {
        base.ChangeHP(amount, Type);
        if (currentHP <= 0)
        {
            currentHP = 1;
            combo = Combos.Presist;
        }
    }
    

    
}
