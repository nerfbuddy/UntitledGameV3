using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public SceneData SD;
    public static string KIB;

    /*
    GameObject GameManager1;
    public GameObject playerObject;
    Player player;
    GameManager GM;
    public static List<GameObject> sceneObjects = new List<GameObject>();
    [SerializeField] List<GameObject> startList;
    /*
    private void Start()
    {

        GameManager1 = GameObject.Find("GameManager");
        GM = GameManager1.GetComponent<GameManager>();
        GM.SD = this;
        KillObject(GameManager.KIB);
        AddPlayer();
        SetScene();
    }
    /*
    public void SetScene()
    {
        foreach (GameObject gameObject in sceneObjects)
        {
            gameObject.SetActive(true);
        }
    }

    public void KillObject(GameObject gameObject)
    {
        if(gameObject != null)
            sceneObjects.Remove(gameObject);
    }

    void AddPlayer()
    {
        //playerObject = GameObject.Find("Player");
        if(playerObject.active == false)
        {
            playerObject.SetActive(true);
        }
        else
        {
            //playerObject.SetActive(true);
        }
        //playerObject.SetActive(true);
    }
    */
}
