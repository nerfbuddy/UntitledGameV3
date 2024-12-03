using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{
    [SerializeField] Button button;
    public int index;
    public Player player;
    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(WhichMove);
    }
    public void WhichMove()
    {
        player.selcMove = player.allMoves[index];
        player.ReBox.SetActive(false);
        player.menuButtons.SetActive(true);
    }
    private void OnDisable()
    {
        Destroy(gameObject);
    }
    public void BackButton()
    {

    }
}
