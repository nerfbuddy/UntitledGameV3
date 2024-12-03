using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour
{
    public GameObject[] phyChrgUI = new GameObject[3];
    public GameObject[] drkChrgUI = new GameObject[3];
    public GameObject[] lightChrgUI = new GameObject[3];
    public Text nameText;
	public Text hpText;
    public Text mpText;
    public Slider HPBar;
    public Slider MPBar;
    
    public void SetHUD(Unit unit)
	{
		nameText.text = unit.unitName;
		hpText.text = "HP:" + unit.maxHP + "/" + unit.currentHP;
        mpText.text = "MP:" + unit.maxMP + "/" + unit.currentMP;
        HPBar.maxValue = unit.maxHP;
        HPBar.value = unit.currentHP;
        MPBar.maxValue = unit.maxMP;
        MPBar.value = unit.currentMP;
    }

	public void SetHP(int hp, int maxhp)
	{
		hpText.text = "HP:" + hp + "/" + maxhp;
        HPBar.value = hp;
    }

    public void SetMP(int mp, int maxmp)
    {
        mpText.text = "MP:" + mp + "/" + maxmp;
        MPBar.value = mp;
    }
    public void IndcateType(Unit.Types type)
    {
        switch (type)
        {
            case Unit.Types.Light:
                nameText.color = new Color32(0, 245, 211, 255);
                break;
            case Unit.Types.Dark:
                nameText.color = new Color32(130, 0, 190, 255);
                break;
            case Unit.Types.Physical:
                nameText.color = new Color32(255, 0, 0, 255);
                break;
        }
    }
    public void SetChrg(Unit.Types type, Unit unit)
    {
        switch(type)
        {
            case Unit.Types.Dark:
                foreach (GameObject g in drkChrgUI)
                {
                    g.SetActive(false);
                }
                for (int i = 0; i < unit.drkChrg; i++)
                {
                    drkChrgUI[i].SetActive(true);
                }
                break;
            case Unit.Types.Physical:
                foreach(GameObject g in phyChrgUI)
                {
                    g.SetActive(false);
                }
                for (int i = 0; i < unit.phyChrg; i++)
                {
                    phyChrgUI[i].SetActive(true);
                }
                break;
            case Unit.Types.Light:
                foreach (GameObject g in lightChrgUI)
                {
                    g.SetActive(false);
                }
                for (int i = 0; i < unit.lghtChrg; i++)
                {
                    lightChrgUI[i].SetActive(true);
                }
                break;
            default:
                break;
        }
    }

}
