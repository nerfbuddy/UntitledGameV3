using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blacksmith : Unit
{
    [SerializeField] DialogueObject HowToQuad;
    [SerializeField] DialogueObject HowToLS;
    [SerializeField] DialogueObject HowToHeal;
    [SerializeField] DialogueObject HowToDS;
    [SerializeField] DialogueObject useLS;
    [SerializeField] DialogueObject HowToDischarge;
    [SerializeField] DialogueObject Done;
    Types playerType;
    int count = 0;
    public override void MyTurn()
    {
        switch (count)
        {
            case 0:
                count += 1;
                BS.Slot0.SetActive(false);
                BS.Slot1.SetActive(true);
                BS.Slot2.SetActive(false);
                BS.Slot3.SetActive(false);
                StartCoroutine(First());
                break;
            case 1:
                count += 1;
                BS.Slot0.SetActive(false);
                BS.Slot1.SetActive(false);
                BS.Slot2.SetActive(false);
                BS.Slot3.SetActive(true);
                StartCoroutine(Second());
                break;
            case 2:
                count += 1;
                BS.Slot0.SetActive(false);
                BS.Slot1.SetActive(false);
                BS.Slot2.SetActive(true);
                BS.Slot3.SetActive(false);
                StartCoroutine(Third());
                break;
            case 3:
                count += 1;
                BS.Slot0.SetActive(true);
                BS.Slot1.SetActive(false);
                BS.Slot2.SetActive(false);
                BS.Slot3.SetActive(false);
                StartCoroutine(Fourth());
                break;
            case 4:
                count += 1;
                BS.Slot0.SetActive(false);
                BS.Slot1.SetActive(false);
                BS.Slot2.SetActive(false);
                BS.Slot3.SetActive(true);
                StartCoroutine(Fifth());
                break;
            case 5:
                count += 1;
                BS.Slot0.SetActive(false);
                BS.Slot1.SetActive(false);
                BS.Slot2.SetActive(true);
                BS.Slot3.SetActive(true);
                StartCoroutine(Sixth());
                break;
            case 6:
                count += 1;
                BS.Slot0.SetActive(true);
                BS.Slot1.SetActive(true);
                BS.Slot2.SetActive(true);
                BS.Slot3.SetActive(true);
                StartCoroutine(Seventh());
                playerType = playerUnit.MyType;
                break;
            case 7:
                if(currentMP > 0)
                {
                    switch (playerType)
                    {
                        case Types.Physical:
                            StartCoroutine(Dark());
                            break;
                        case Types.Light:
                            StartCoroutine(Phyical());
                            break;
                        case Types.Dark:
                            StartCoroutine(Light());
                            break;
                        default:
                            StartCoroutine(Phyical());
                            break;
                    }
                }
                else
                {
                    StartCoroutine(Phyical());
                }
                playerType = playerUnit.MyType;
                break;
        }
    }
    IEnumerator Phyical()
    {
        BS.DoTxt(unitName + " Uses Quad Slash!");
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
        HUD.SetChrg(Types.Physical, this);
        BS.NextTurn();
    }
    IEnumerator Dark()
    {
        BS.DoTxt(unitName + " Uses Dark Slash!");
        yield return new WaitForSeconds(1.5f);
        if (drkChrg >= 3)
        {
            drkChrg = 0;
            BS.PDoHPChange(-damage * 2, Unit.Types.Dark);
        }
        else
        {
            BS.PDoHPChange(-damage, Unit.Types.Dark);
            drkChrg += 1;
        }
        ChangeMP(-1);
        HUD.SetMP(currentMP, maxMP);
        BS.EDoHPChange(damage/2, Types.Neutral);
        HUD.SetChrg(Unit.Types.Dark, this);
        BS.NextTurn();
    }
    IEnumerator Light()
    {
        BS.DoTxt(unitName + " Uses Light Slash!");
        yield return new WaitForSeconds(1.5f);
        if (lghtChrg >= 3)
        {
            lghtChrg = 0;
            BS.PDoHPChange(-20 * 2, Unit.Types.Light);
        }
        else
        {
            BS.PDoHPChange(-20, Unit.Types.Light);
            lghtChrg += 1;
        }
        ChangeMP(-1);
        HUD.SetMP(currentMP, maxMP);
        HUD.SetChrg(Unit.Types.Light, this);
        BS.NextTurn();
    }

    IEnumerator First()
    {
        bool checking = false;
        BS.DoTalk(HowToQuad);
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
                BS.DoTxt("Use Quad Slash");
                yield return new WaitForSeconds(1.5f);
                BS.NextTurn();
                break;
            }
        }
        
    }
    IEnumerator Second()
    {
        bool checking = false;
        BS.DoTalk(HowToLS);
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
                BS.DoTxt("Use Light Slash");
                yield return new WaitForSeconds(1.5f);
                BS.NextTurn();
                break;
            }
        }

    }
    IEnumerator Third()
    {
        bool checking = false;
        BS.DoTalk(HowToHeal);
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
                BS.DoTxt("Blacksmith does 20 damage");
                BS.PDoHPChange(-20, Types.Physical);
                yield return new WaitForSeconds(1.5f);
                BS.DoTxt("Use Healing Melody");
                yield return new WaitForSeconds(1.5f);
                BS.NextTurn();
                break;
            }
        }

    }
    IEnumerator Fourth()
    {
        bool checking = false;
        BS.DoTalk(HowToDS);
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
                BS.DoTxt("Use Dark Slash");
                yield return new WaitForSeconds(1.5f);
                BS.NextTurn();
                break;
            }
        }

    }
    IEnumerator Fifth()
    {
        bool checking = false;
        BS.DoTalk(useLS);
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
                BS.DoTxt("Blacksmith uses a dark move!");
                drkChrg += 1;
                HUD.SetChrg(Unit.Types.Dark, this);
                BS.PDoHPChange(-10, Types.Dark);
                yield return new WaitForSeconds(1.5f);
                BS.DoTxt("Use Light Slash For Effective Damage!");
                yield return new WaitForSeconds(1.5f);
                BS.NextTurn();
                break;
            }
        }

    }
    IEnumerator Sixth()
    {
        bool checking = false;
        BS.DoTalk(HowToDischarge);
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
                BS.DoTxt("Use Any Light Move");
                yield return new WaitForSeconds(1.5f);
                BS.NextTurn();
                break;
            }
        }

    }
    IEnumerator Seventh()
    {
        bool checking = false;
        BS.DoTalk(Done);
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
                BS.DoTxt("Time for a real fight!");
                yield return new WaitForSeconds(1.5f);
                BS.NextTurn();
                break;
            }
        }

    }
}
