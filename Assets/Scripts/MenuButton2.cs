using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuButton2 : MonoBehaviour
{
    Player player;
    GameObject play;
    [SerializeField] int index;
    // Start is called before the first frame update
    private void OnEnable()
    {
        play = GameObject.Find("Player");
        player = play.GetComponent<Player>();
        gameObject.GetComponent<Image>().color = player.actvMoves[index].color;
        gameObject.GetComponentInChildren<TMP_Text>().text = player.actvMoves[index].Name;
    }
    public void BackButton()
    {

    }
}
