using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour
{

	public Text nameText;
	public Text hpText;
    public Text mpText;


    public void SetHUD(Unit unit)
	{
		nameText.text = unit.unitName;
		hpText.text = "HP:" + unit.maxHP + "/" + unit.currentHP;
        mpText.text = "MP:" + unit.maxMP + "/" + unit.currentMP;
    }

	public void SetHP(int hp, int maxhp)
	{
		hpText.text = "HP:" + hp + "/" + maxhp;
	}

    public void SetMP(int mp, int maxmp)
    {
        mpText.text = "MP:" + mp + "/" + maxmp;
    }

}
