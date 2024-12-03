using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : Unit
{
    public PlayerMoves[] actvMoves = new PlayerMoves[4];
    public List<PlayerMoves> allMoves = new List<PlayerMoves>();
    [SerializeField] Sprite BattleSprite;
    public Vector3 OWPos;
    OverworldPlayer OWPlayer;

    public PlayerData playerdata;

    public GameObject menuButtons;
    public GameObject menuBox;
    public PlayerMoves selcMove;
    int slotPicked;
    [SerializeField] public GameObject MenuButtonPrefab;
    [SerializeField] public GameObject ReBox;

    public override void Start()
    {
        for (int i = 0; i < allMoves.Count; i++)
        {
            if (allMoves[i].Name == playerdata.actvMoves[0])
            {
                actvMoves[0] = allMoves[i];
            }
            else if (allMoves[i].Name == playerdata.actvMoves[1])
            {
                actvMoves[1] = allMoves[i];
            }
            else if (allMoves[i].Name == playerdata.actvMoves[2])
            {
                actvMoves[2] = allMoves[i];
            }
            else if (allMoves[i].Name == playerdata.actvMoves[3])
            {
                actvMoves[3] = allMoves[i];
            }
        }
        base.Start();
    }
    public void SavePos()
    {
        playerdata.OWPos = transform.position;
    }
    public void TogglePlayerScript(bool on)
    {
        BatSys = GameObject.Find("Battle System");
        BS = BatSys.GetComponent<BattleSystem>();
        gameObject.GetComponent<OverworldPlayer>().enabled = on;
        gameObject.GetComponent<BoxCollider2D>().enabled = on;
        gameObject.GetComponent<Animator>().enabled = on;
        gameObject.GetComponent<SpriteRenderer>().sprite = BattleSprite;
        gameObject.GetComponent<SpriteRenderer>().flipX = false;
        BS.SlotAssignment(actvMoves);
        IsPlayer = true;
    }

    public override void MyTurn()
    {
        BS.DoTxt("Choose an action:");
        BS.UIBox.gameObject.SetActive(true);
        BS.Buttons.gameObject.SetActive(true);
        base.MyTurn();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && OverworldPlayer.playerControl == true)
        {
            Menu();
        }
        
    }
    public void Menu()
    {
        OverworldPlayer.playerControl = false;
        menuBox.SetActive(true);
        ReBox.SetActive(true);
        for (int i = 0; i < allMoves.Count; i++)
        {
            GameObject button = Instantiate(MenuButtonPrefab);
            button.transform.SetParent(ReBox.transform);
            button.transform.position = new Vector3(300, 60 + i * 30, 0);
            button.GetComponentInChildren<Text>().text = allMoves[i].Name;
            MenuButton script = button.GetComponent<MenuButton>();
            script.index = i;
            script.player = this;
        }
    }
    public void SetMove(int num)
    {
        actvMoves[num] = selcMove;
        ReBox.SetActive(false);
        menuButtons.SetActive(false);
        menuBox.SetActive(false);

        playerdata.actvMoves[0] = actvMoves[0].Name;
        playerdata.actvMoves[1] = actvMoves[1].Name;
        playerdata.actvMoves[2] = actvMoves[2].Name;
        playerdata.actvMoves[3] = actvMoves[0].Name;
    }
    /*
     
    */
}
