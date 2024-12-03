using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Unit : MonoBehaviour
{
    public GameObject[] UnitPrefab;
    protected GameObject BatSys;
    protected GameObject player;
    protected BattleSystem BS;
    protected Player playerUnit;
    public DialogueObject currentText;
    public BattleHUD HUD;
    protected bool IsPlayer = false;

    public enum Types
    {
        Neutral,
        Physical,
        Light,
        Dark
    }
    public Types MyType;
    public string unitName;
    public int damage;
    public int maxMP;
    public int currentMP;
    public int maxHP;
	public int currentHP;
    public int speed;
    public int lghtChrg = 0;
    public int drkChrg = 0;
    public int phyChrg = 0;

    public virtual void Start()
    {
        BatSys = GameObject.Find("Battle System");
        player = GameObject.Find("Player");
        BS = BatSys.GetComponent<BattleSystem>();
        playerUnit = player.GetComponent<Player>();

    }
    public virtual void MyTurn()
    {

    }
    public void ChangeMP(int amount)
    {
        if (IsPlayer)
        {
            StartCoroutine(BS.PDmgPop(amount, true));
        }
        else
        {
            StartCoroutine(BS.EDmgPop(amount, true));
        }
        currentMP += amount;
        if (currentMP > maxMP)
            currentMP = maxMP;
        if (currentMP < 0)
            currentMP = 0;
    }
    public void CheckType(bool dis)
    {
        if (lghtChrg > phyChrg && lghtChrg > drkChrg)
        {
            MyType = Types.Light;
        }
        else if (lghtChrg < phyChrg && phyChrg > drkChrg)
        {
            MyType = Types.Physical;
        }
        else if (phyChrg < drkChrg && lghtChrg < drkChrg)
        {
            MyType = Types.Dark;
        }
        else if(MyType == Types.Neutral && lghtChrg+drkChrg+phyChrg == 0)
        {
            MyType = Types.Neutral;
        }
        else
        {
            if(dis && phyChrg > 0)
            {
                MyType = Types.Physical;
            }
            else if (dis && lghtChrg > 0)
            {
                MyType = Types.Light;
            }
            else if (dis && drkChrg > 0)
            {
                MyType = Types.Dark;
            }
        }
        HUD.IndcateType(MyType);
    }
	public virtual void ChangeHP(int amount, Types Type)
	{
        switch(Type)
        {
            case Types.Physical:
                switch(MyType)
                {
                    case Types.Dark:
                        amount = amount / 2;
                        break;
                    case Types.Light:
                        amount = amount * 2;
                        break;
                    default:
                        break;
                }
                break;
            case Types.Dark:
                switch (MyType)
                {
                    case Types.Physical:
                        amount = amount * 2;
                        break;
                    case Types.Light:
                        amount = amount / 2;
                        break;
                    default:
                        break;
                }
                break;
            case Types.Light:
                switch (MyType)
                {
                    case Types.Dark:
                        amount = amount * 2;
                        break;
                    case Types.Physical:
                        amount = amount / 2;
                        break;
                    default:
                        break;
                }
                break;
            default:
                break;
        }
		currentHP += amount;
        if(IsPlayer)
        {
            StartCoroutine(BS.PDmgPop(amount, false));
        }
        else
        {
            StartCoroutine(BS.EDmgPop(amount, false));
        }
		if (currentHP > maxHP)
			currentHP = maxHP;
        if (currentHP < 0)
            currentHP = 0;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            playerUnit = player.GetComponent<Player>();
            playerUnit.SavePos();
            DontDestroyOnLoad(gameObject);
            SceneManager.LoadScene("BattleScene");
            BattleSystem.enemyPrefab = UnitPrefab;
            BattleSystem.enemyID = gameObject.name;
            StartCoroutine(kill(0.01f));
        }
    }
    public IEnumerator kill(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }

}
