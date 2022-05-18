using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }

public class BattleSystem : MonoBehaviour
{

	public GameObject playerPrefab;
	public static GameObject enemyPrefab;

	private Transform playerBattleStation;
	private Transform enemyBattleStation;
    public Transform NotePos;
    public Transform NotePosatk;
    public static int currentPlayerDmg = 0;
    int randint = 0;

    Unit playerUnit;
	Unit enemyUnit;

    public static int healamount;
    public static int atkamount;
    public static int Famount = 1;

    public static int eHPChange = 0;
    public static int eMPChange = 0;
    public static int pHPChange = 0;
    public static int pMPChange = 0;
    public static string DisplayTxt = "";
    public static float waitTime = 0f;

    public GameObject Buttons;
    public GameObject UIBox;
    public GameObject AtkGame;
    public GameObject FocusGame;
    public GameObject NoteHolder;
    public GameObject NoteHolderatk;
    public GameObject NoteHolderLS;
    private GameObject Notes;
    public Text dialogueText;
    

	public BattleHUD playerHUD;
	public BattleHUD enemyHUD;

	public BattleState state;
    public AudioSource audioSource;
    void Start()
    {
        state = BattleState.START;
		StartCoroutine(SetupBattle());
    }
    private void Awake()
    {
        audioSource.PlayDelayed(5.1f);
    }
    IEnumerator SetupBattle()
	{
		GameObject playerGO = Instantiate(playerPrefab, playerBattleStation);
		playerUnit = playerGO.GetComponent<Unit>();

		GameObject enemyGO = Instantiate(enemyPrefab, enemyBattleStation);
		enemyUnit = enemyGO.GetComponent<Unit>();
        enemyGO.transform.position = new Vector3(3f, 2f, -1f);

        dialogueText.text = "A wild " + enemyUnit.unitName + " attacks!";

		playerHUD.SetHUD(playerUnit);
		enemyHUD.SetHUD(enemyUnit);

		yield return new WaitForSeconds(1f);

		state = BattleState.PLAYERTURN;
		PlayerTurn();
	}

    public IEnumerator PlayerMove()
    {
        UIBox.gameObject.SetActive(false);
        currentPlayerDmg = playerUnit.damage;
        pHPChange = 0;
        pMPChange = 0;
        eMPChange = 0;
        eHPChange = 0;
        DisplayTxt = "";

        yield return new WaitForSeconds(waitTime);

        UIBox.gameObject.SetActive(true);
        dialogueText.text = DisplayTxt;
        enemyUnit.ChangeHP(eHPChange);
        playerUnit.ChangeHP(pHPChange);
        playerUnit.ChangeMP(pMPChange);
        enemyUnit.ChangeMP(eMPChange);
        enemyHUD.SetHP(enemyUnit.currentHP, enemyUnit.maxHP);
        playerHUD.SetMP(playerUnit.currentMP, playerUnit.maxMP);

        if (enemyUnit.currentHP <= 0)
        {
            state = BattleState.WON;
            StartCoroutine(EndBattle()); ;
        }
        else
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }

        yield return new WaitForSeconds(2f);
    }

    IEnumerator PlayerAttack()
	{
        atkamount = 0;
        UIBox.gameObject.SetActive(false);
        AtkGame.gameObject.SetActive(true);
        Notes = Instantiate(NoteHolderatk, NotePosatk);

        yield return new WaitForSeconds(3f);

        UIBox.gameObject.SetActive(true);
        AtkGame.gameObject.SetActive(false);
        Destroy(Notes);
        bool isDead = enemyUnit.TakeDamage(playerUnit.damage * atkamount);

        enemyHUD.SetHP(enemyUnit.currentHP, enemyUnit.maxHP);
        if (atkamount > 0)
        {
            dialogueText.text = "Hit for " + playerUnit.damage * atkamount + " damage!";
        }
	    else
        {
            dialogueText.text = "The attack missed...";
        }

		if(isDead)
		{
			state = BattleState.WON;
            StartCoroutine(EndBattle()); ;
		} else
		{
			state = BattleState.ENEMYTURN;
			StartCoroutine(EnemyTurn());
		}

        yield return new WaitForSeconds(2f);
    }
    IEnumerator PlayerF()
    {
        Famount = 0;
        UIBox.gameObject.SetActive(false);
        FocusGame.gameObject.SetActive(true);

        yield return new WaitForSeconds(4f);
        Famount *= 2;
        playerUnit.damage += Famount;

        UIBox.gameObject.SetActive(true);
        FocusGame.gameObject.SetActive(false);
        if (Famount > 0)
        {
            dialogueText.text = "Attack increced by "+ Famount;
        }
        else
        {
            dialogueText.text = "The move failed...";
        }

        yield return new WaitForSeconds(2f);

        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }
    IEnumerator PlayerLS()
    {
        if (playerUnit.currentMP > 0)
        {
            atkamount = 0;
            UIBox.gameObject.SetActive(false);
            AtkGame.gameObject.SetActive(true);
            Notes = Instantiate(NoteHolderLS, NotePosatk);
            playerUnit.ChangeMP(-1);
            playerHUD.SetMP(playerUnit.currentMP, playerUnit.maxMP);
            yield return new WaitForSeconds(1f);

            UIBox.gameObject.SetActive(true);
            AtkGame.gameObject.SetActive(false);
            Destroy(Notes);
            bool isDead = enemyUnit.TakeDamage(60 * atkamount);

            enemyHUD.SetHP(enemyUnit.currentHP, enemyUnit.maxHP);
            if (atkamount > 0)
            {
                dialogueText.text = "Hit for " + 60 * atkamount + " damage!";
            }
            else
            {
                dialogueText.text = "The attack missed...";
            }


            if (isDead)
            {
                state = BattleState.WON;
                StartCoroutine(EndBattle());
            }
            else
            {
                state = BattleState.ENEMYTURN;
                StartCoroutine(EnemyTurn());
            }

            yield return new WaitForSeconds(2f);
        }
        else
        {
            Buttons.gameObject.SetActive(false);
            dialogueText.text = "You are out of MP!";
            yield return new WaitForSeconds(1f);
            dialogueText.text = "Choose an action:";
            Buttons.gameObject.SetActive(true);
        }
    }
    IEnumerator EnemyTurn()
	{
        randint = Random.Range(1, 5);
        if (enemyUnit.currentMP > 0 && randint < 2)
        {
            yield return new WaitForSeconds(1f);
            dialogueText.text = enemyUnit.unitName + " Buffs his attack!";
            Buttons.gameObject.SetActive(false);
            enemyUnit.damage += 10;
            enemyUnit.ChangeMP(-1);
            enemyHUD.SetMP(enemyUnit.currentMP, enemyUnit.maxMP);
            yield return new WaitForSeconds(2f);
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }
        else
        {
            yield return new WaitForSeconds(1f);
            Buttons.gameObject.SetActive(false);
            dialogueText.text = enemyUnit.unitName + " attacks!";

            yield return new WaitForSeconds(1f);

            bool isDead = playerUnit.TakeDamage(enemyUnit.damage);

            playerHUD.SetHP(playerUnit.currentHP, playerUnit.maxHP);

            yield return new WaitForSeconds(1f);

            if (isDead)
            {
                state = BattleState.LOST;
                StartCoroutine(EndBattle());
            }
            else
            {
                state = BattleState.PLAYERTURN;
                PlayerTurn();
            }
        }
	}

	IEnumerator EndBattle()
	{
		if(state == BattleState.WON)
		{
            Buttons.gameObject.SetActive(false);
            dialogueText.text = "You won the battle!";
            StartCoroutine(enemyUnit.kill(0f));
            yield return new WaitForSeconds(3f);
            StartCoroutine(playerUnit.kill(0f));
            SceneManager.LoadSceneAsync("OverworldScene");
        } else if (state == BattleState.LOST)
		{
            Buttons.gameObject.SetActive(false);
            dialogueText.text = "You were defeated.";
            StartCoroutine(enemyUnit.kill(0f));
            StartCoroutine(playerUnit.kill(0f));
            yield return new WaitForSeconds(3f);
            SceneManager.LoadSceneAsync("Gameover");
        }
	}

	void PlayerTurn()
	{
		dialogueText.text = "Choose an action:";
        Buttons.gameObject.SetActive(true);
    }

	IEnumerator PlayerHeal()
	{
        if (playerUnit.currentMP > 0)
        {
            healamount = 0;
            Buttons.gameObject.SetActive(false);
            UIBox.gameObject.SetActive(false);
            Notes = Instantiate(NoteHolder, NotePos);
            playerUnit.ChangeMP(-1);
            playerHUD.SetMP(playerUnit.currentMP, playerUnit.maxMP);
            yield return new WaitForSeconds(3f);

            UIBox.gameObject.SetActive(true);
            Destroy(Notes);
            playerUnit.ChangeHP(10 * healamount);

            playerHUD.SetHP(playerUnit.currentHP, playerUnit.maxHP);
            if (healamount > 0)
            {
                dialogueText.text = "Healed " + 10 * healamount + " health!";
            }
            else
            {
                dialogueText.text = "The spell failed...";
            }

            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());

            yield return new WaitForSeconds(2f);
        }
        else
        {
            Buttons.gameObject.SetActive(false);
            dialogueText.text = "You are out of MP!";
            yield return new WaitForSeconds(1f);
            dialogueText.text = "Choose an action:";
            Buttons.gameObject.SetActive(true);
        }
    }

	public void OnAttackButton()
	{
		if (state != BattleState.PLAYERTURN)
			return;

        StartCoroutine(PlayerMove());
    }
    public void OnFButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        StartCoroutine(PlayerF());
    }
    public void OnLSButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        StartCoroutine(PlayerMove());
    }
    public void OnHealButton()
	{
		if (state != BattleState.PLAYERTURN)
			return;

		StartCoroutine(PlayerHeal());
	}

}
