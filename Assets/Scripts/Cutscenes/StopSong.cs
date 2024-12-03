using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopSong : MonoBehaviour
{
    private GameObject player;
    private OverworldPlayer OWPlayer;
    private bool run = false;
    public AudioClip song;
    public AudioSource audioSource;

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
            audioSource.Stop();
            //audioSource.Play();
            StartCoroutine(StartEvent());
        }
    }
    IEnumerator StartEvent()
    {
        run = true;
        while (run)
        {
            if (DialogueUI.IsOpen)
            {
                yield return null;
            }
            else
            {
                run = false;
                //DontDestroyOnLoad(gameObject);
                //SceneManager.LoadScene("BattleScene");
                //BattleSystem.enemyPrefab = UnitPrefab;
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
