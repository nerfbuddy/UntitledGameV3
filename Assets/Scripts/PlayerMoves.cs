using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMoves : MonoBehaviour
{
    //public bool IsUsed = false;
    //public int slotAssigned;
    //public int moveNum;
    public string Name;
    public Color32 color;
    protected GameObject BatSys;
    protected Transform miniGame;
    [SerializeField] protected GameObject MoveGame;
    protected GameObject Notes;
    public BattleSystem BS;
    protected OverworldPlayer OWplayer;
    protected Player player;
    public GameObject playerObject;

    protected void OnStart()
    {
        OWplayer = gameObject.GetComponentInParent<OverworldPlayer>();
        player = gameObject.GetComponentInParent<Player>();
        miniGame = gameObject.transform;
    }
    protected void Setup()
    {
        BatSys = GameObject.Find("Battle System");
        BS = BatSys.GetComponent<BattleSystem>();
    }
    protected void OuttaMP()
    {
        BattleSystem.waitTime = 3f;
        StartCoroutine(BS.OutOfMP());
    }
    protected void OuttaLC()
    {
        BattleSystem.waitTime = 3f;
        StartCoroutine(BS.OutOfLC());
    }
    public virtual void PickTarget()
    {
        Debug.Log("worg fun");
    }
    public virtual void ActivateSlots(int slotnum)
    {
        Debug.Log("worg fun2");
    }

}
