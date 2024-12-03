using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PogWyrem : Unit
{
    public override void MyTurn()
    {
        StartCoroutine(Attack());
        base.MyTurn();
    }

    IEnumerator Attack()
    {
                BS.DoTxt(unitName + " attacks!");
                yield return new WaitForSeconds(1.5f);
                if(phyChrg >= 3)
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
}
