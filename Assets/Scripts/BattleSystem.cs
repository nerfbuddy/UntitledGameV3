using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }

public class BattleSystem : MonoBehaviour
{
    public static int targetedEnemy;
    public static int points = 0;
    public static float waitTime = 0f;

    public static string enemyID;
    public int currentTurn = 0;
    public int objectIndex;
    [SerializeField] public GameObject targetbox;
    public GameObject[] targetButtons = new GameObject[4];
    public GameObject Slot0;
    public GameObject Slot1;
    public GameObject Slot2;
    public GameObject Slot3;
    public GameObject[] Slots = new GameObject[4];
    public Text tSlot0;
    public Text tSlot1;
    public Text tSlot2;
    public Text tSlot3;
    Text[] tSlots = new Text[4];

    public GameObject Buttons;
    public GameObject UIBox;
    public GameObject FocusGame;
    public TMP_Text dialogueText;
    public GameObject PDmgPopUp;
    public GameObject PMPPopUp;

    public GameObject EDmgPopUp;
    public GameObject EMPPopUp;

    public BattleHUD playerHUD;
	public BattleHUD[] enemyHUD;

    Player playerUnit;
    public Unit[] enemyUnit;

    public List<Unit> turnOrder = new List<Unit>();

    [SerializeField] GameObject playerPrefabHolder;
    [SerializeField] public static GameObject playerPrefab;
    GameObject playerGO;

    public static GameObject[] enemyPrefab;

    private TMP_Text PDmgTxt;
    private TMP_Text PMPTxt;
    private TMP_Text EDmgTxt;
    private TMP_Text EMPTxt;
    private TypewriterEffect typewriterEffect;
    public BattleState state;
    public AudioSource audioSource;
    public DialogueUI DialogueUI => dialogueUI;
    [SerializeField] private DialogueUI dialogueUI;
    GameObject CVS;
    OverworldPlayer OWPlayer;


    void Start()
    {
        CVS = GameObject.Find("Canvas");
        dialogueUI = CVS.GetComponent<DialogueUI>();
        PDmgTxt = PDmgPopUp.GetComponent<TMP_Text>();
        PMPTxt = PMPPopUp.GetComponent<TMP_Text>();
        EDmgTxt = EDmgPopUp.GetComponent<TMP_Text>();
        EMPTxt = EMPPopUp.GetComponent<TMP_Text>();

        typewriterEffect = GetComponent<TypewriterEffect>();


        Slots[0] = Slot0;
        Slots[1] = Slot1;
        Slots[2] = Slot2;
        Slots[3] = Slot3;
        tSlots[0] = tSlot0;
        tSlots[1] = tSlot1;
        tSlots[2] = tSlot2;
        tSlots[3] = tSlot3;
        StartCoroutine(SetupBattle());
    }
    private void Awake()
    {
        //audioSource.PlayDelayed(10.11f);
        audioSource.PlayDelayed(5.1f);
    }
    public IEnumerator SetupBattle()
	{
		playerGO = GameObject.FindGameObjectWithTag("Player");
        OWPlayer = playerGO.GetComponent<OverworldPlayer>();
        playerUnit = playerGO.GetComponent<Player>();
        playerGO.transform.position = new Vector3(-6f, 2f, -1f);
        
        playerUnit.TogglePlayerScript(false);
        
        playerGO.transform.GetChild(0).gameObject.SetActive(true);
        int len = enemyPrefab.Length;
        enemyUnit = new Unit[len];
        
        for (int i = 0; i < enemyPrefab.Length; i++)
        {
            GameObject enemyGO = Instantiate(enemyPrefab[i]);
            enemyGO.transform.position = new Vector3(6f, 1f+len-i*2, -1f);
            enemyGO.transform.localScale = new Vector3(1f/len, 1f/len, 1f/len);
            enemyUnit[i] = enemyGO.GetComponent<Unit>();
            enemyHUD[i].SetHUD(enemyUnit[i]);
            enemyUnit[i].HUD = enemyHUD[i];
            turnOrder.Add(enemyUnit[i]);
            targetButtons[i].SetActive(true);
        }

        StartCoroutine(RunTypingEffect("Enemy" + " attacks!"));

		playerHUD.SetHUD(playerUnit);
        playerUnit.HUD = playerHUD;

        turnOrder.Add(playerUnit);
        turnOrder = turnOrder.OrderByDescending(unit => unit.speed).ToList();
        //SlotAssignment(playerUnit.actvMoves);
		yield return new WaitForSeconds(1.5f);

       
        turnOrder[0].MyTurn();
	}

    public void PDoChrg(Unit unit, Unit.Types type)
    {
        playerHUD.SetChrg(type, unit);
    }
    public void EDoHPChange(int dmg, Unit.Types Type)
    {
        enemyUnit[targetedEnemy].ChangeHP(dmg, Type);
        enemyHUD[targetedEnemy].SetHP(enemyUnit[targetedEnemy].currentHP, enemyUnit[targetedEnemy].maxHP);
    }
    public void PDoHPChange(int dmg, Unit.Types Type)
    {
        playerUnit.ChangeHP(dmg, Type);
        playerHUD.SetHP(playerUnit.currentHP, playerUnit.maxHP);
    }

    public void PDoMPChange(int dmg)
    {
        playerUnit.ChangeMP(dmg);
        playerHUD.SetMP(playerUnit.currentMP, playerUnit.maxMP);
    }
    public void EDoMPChange(int dmg)
    {
        enemyUnit[targetedEnemy].ChangeMP(dmg);
        enemyHUD[targetedEnemy].SetMP(enemyUnit[targetedEnemy].currentMP, enemyUnit[targetedEnemy].maxMP);
    }

    public void ToggleUIBox(bool on)
    {
        UIBox.gameObject.SetActive(on);
    }
    public void ToggleButtons(bool on)
    {
        Buttons.gameObject.SetActive(on);
    }
    public void ToggleTargetBox(bool on)
    {
        targetbox.gameObject.SetActive(on);
    }

    public void DoTxt(string txt)
    {
        StartCoroutine(RunTypingEffect(txt));
    }
    public void DoTalk(DialogueObject dialogue)
    {
        UIBox.gameObject.SetActive(true);
        dialogueUI.ShowDialogue(dialogue);
        DialogueUI.IsOpen = true;
    }
    public void NextTurn()
    {
        
        if (playerUnit.currentHP <= 0)
        {
            state = BattleState.LOST;
            StartCoroutine(EndBattle());
        }
        else
        {
            for (int i = 0; i < enemyUnit.Length; i++)
            {
                /*
                Debug.Log(targetButtons[i]);
                
                */
                if (enemyUnit[i].currentHP <= 0)
                {
                    turnOrder.Remove(enemyUnit[i]);
                    StartCoroutine(enemyUnit[i].kill(0f));
                    targetButtons[i].SetActive(false);
                }
            }
            /*
            for (int i = 0; i < turnOrder.Count - 1; i++)
            {
                //Debug.Log();
                //targetButtons[i].SetActive(true);
                enemyHUD[i].SetHUD(enemyUnit[i]);
                enemyUnit[i].HUD = enemyHUD[i];
            }
            */
            if (turnOrder.Count() == 1)
            {
                state = BattleState.WON;
                StartCoroutine(EndBattle());
            }
            else
            {
                if (currentTurn + 1 >= turnOrder.Count)
                {
                    currentTurn = 0;
                    turnOrder[currentTurn].MyTurn();
                }
                else
                {
                    currentTurn += 1;
                    turnOrder[currentTurn].MyTurn();
                }
            }
        }
        foreach(Unit unit in enemyUnit)
        {
            unit.CheckType(false);
        }
        playerUnit.CheckType(false);
    }

    public IEnumerator PDmgPop(int dmg, bool mp)
    {
        if (mp)
        {
            PMPPopUp.SetActive(true);
            if (dmg > 0)
            {
                PMPTxt.color = new Color32(0, 245, 211, 255);
                PMPTxt.text = dmg.ToString();
            }
            else
            {
                PMPTxt.color = new Color32(130, 0, 190, 255);
                PMPTxt.text = dmg.ToString();
            }
            yield return new WaitForSeconds(1f);
            PMPPopUp.SetActive(false);
        }
        else
        {
            PDmgPopUp.SetActive(true);
            if (dmg > 0)
            {
                PDmgTxt.color = new Color32(0, 255, 0, 255);
                PDmgTxt.text = dmg.ToString();
            }
            else
            {
                PDmgTxt.color = new Color32(255, 0, 0, 255);
                PDmgTxt.text = dmg.ToString();
            }
            yield return new WaitForSeconds(1f);
            PDmgPopUp.SetActive(false);
        }
    }
    public IEnumerator EDmgPop(int dmg, bool mp)
    {
        if (mp)
        {
            EMPPopUp.SetActive(true);
            if (dmg > 0)
            {
                EMPTxt.color = new Color32(0, 245, 211, 255);
                EMPTxt.text = dmg.ToString();
            }
            else
            {
                EMPTxt.color = new Color32(130, 0, 190, 255);
                EMPTxt.text = dmg.ToString();
            }
            yield return new WaitForSeconds(1f);
            EMPPopUp.SetActive(false);
        }
        else
        {
            EDmgPopUp.SetActive(true);
            if (dmg > 0)
            {
                EDmgTxt.color = new Color32(0, 255, 0, 255);
                EDmgTxt.text = dmg.ToString();
            }
            else
            {
                EDmgTxt.color = new Color32(255, 0, 0, 255);
                EDmgTxt.text = dmg.ToString();
            }
            yield return new WaitForSeconds(1f);
            EDmgPopUp.SetActive(false);
        }
    }

    public IEnumerator OutOfMP()
    {
        Buttons.gameObject.SetActive(false);

        StartCoroutine(RunTypingEffect("You don't have enough MP for this move!"));

        yield return new WaitForSeconds(waitTime);

        Buttons.gameObject.SetActive(true);
        StartCoroutine(RunTypingEffect("Choose an action:"));
    }
    public IEnumerator OutOfLC()
    {
        Buttons.gameObject.SetActive(false);

        StartCoroutine(RunTypingEffect("You don't have enough Light charges for this move!"));

        yield return new WaitForSeconds(waitTime);

        Buttons.gameObject.SetActive(true);
        StartCoroutine(RunTypingEffect("Choose an action:"));
    }
    IEnumerator EndBattle()
	{
		if(state == BattleState.WON)
        { 
            Buttons.gameObject.SetActive(false);
            StartCoroutine(RunTypingEffect("You won the battle!"));
            MinigameState(false);
            yield return new WaitForSeconds(3f);
            SceneManager.LoadSceneAsync("OverworldScene");
            //SceneManager.LoadSceneAsync("TempEnding");
            //playerUnit.LoadPos();
            GameManager.KIB = enemyID;
            playerUnit.kill(0f);
        } else if (state == BattleState.LOST)
		{
            Buttons.gameObject.SetActive(false);
            StartCoroutine(RunTypingEffect("You were defeated."));
            StartCoroutine(enemyUnit[targetedEnemy].kill(0f));
            StartCoroutine(playerUnit.kill(0f));
            MinigameState(false);
            yield return new WaitForSeconds(3f);
            SceneManager.LoadSceneAsync("Gameover");
        }
	}

    private void MinigameState(bool state)
    {
        playerGO.transform.GetChild(0).gameObject.SetActive(state);
    }

    private IEnumerator RunTypingEffect(string dialogue)
    {
        typewriterEffect.Run(dialogue, dialogueText);

        while (typewriterEffect.IsRunning)
        {
            yield return null;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                typewriterEffect.Stop();
            }
        }
    }

    public void SlotAssignment(PlayerMoves[] move)
    {
        for (int i = 0; i < move.Length; i++)
        {
            move[i].BS = this;
            Slots[i].gameObject.SetActive(true);
            tSlots[i].text = move[i].Name;
            Slots[i].GetComponent<Button>().onClick.AddListener(move[i].PickTarget);
            Slots[i].GetComponent<Image>().color = move[i].color;
        }

    }
}
