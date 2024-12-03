using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Victor1 : MonoBehaviour
{
    private GameObject player;
    private OverworldPlayer OWPlayer;
    [SerializeField] private DialogueObject dialogueObject;
    private bool run = false;
    public GameObject[] UnitPrefab;

    void Start()
    {
        player = GameObject.Find("Player");
        OWPlayer = player.GetComponent<OverworldPlayer>();
    }

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.TryGetComponent(out OverworldPlayer player))
        {
            OWPlayer.DialogueUI.ShowDialogue(dialogueObject);
            StartCoroutine(StartTheBattle());
        }
    }
    IEnumerator StartTheBattle()
    {
        run = true;
        while (run)
        {
            if(DialogueUI.IsOpen)
            {
                yield return null;
            }
            else
            {
                run = false;
                DontDestroyOnLoad(gameObject);
                SceneManager.LoadScene("BattleScene");
                BattleSystem.enemyPrefab = UnitPrefab;
                StartCoroutine(kill(0.01f));
            }
        }
    }
    public IEnumerator kill(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}
