using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusGame : MonoBehaviour
{
    public GameObject button0;
    public GameObject button1;
    public GameObject button2;
    public GameObject button3;
    public GameObject button4;
    public GameObject button5;
    public GameObject button6;
    public GameObject button7;
    public GameObject button8;
    int randint = 0;
    GameObject[] Buttons = new GameObject [9];
    GameObject Active;

    // Start is called before the first frame update
    void Start()
    {
        Buttons[0] = button0;
        Buttons[1] = button1;
        Buttons[2] = button2;
        Buttons[3] = button3;
        Buttons[4] = button4;
        Buttons[5] = button5;
        Buttons[6] = button6;
        Buttons[7] = button7;
        Buttons[8] = button8;
        
        
    }
    public void Startup()
    {
        StartCoroutine(ButtonSpawn());
    }
    public IEnumerator ButtonSpawn ()
    {
        
        randint = Random.Range(1, 9);
        yield return new WaitForSeconds(0.5f);
        
        Active = Buttons[randint];
        Active.gameObject.SetActive(true);

        yield return new WaitForSeconds(0.5f);

        Active.gameObject.SetActive(false);

        
        randint = Random.Range(1, 9);
        yield return new WaitForSeconds(0.5f);

        Active = Buttons[randint];
        Active.gameObject.SetActive(true);

        yield return new WaitForSeconds(0.5f);

        Active.gameObject.SetActive(false);

        
        randint = Random.Range(1, 9);
        yield return new WaitForSeconds(0.5f);

        Active = Buttons[randint];
        Active.gameObject.SetActive(true);

        yield return new WaitForSeconds(0.5f);

        Active.gameObject.SetActive(false);
    }
    public void ClickedButton()
    {
        BattleSystem.Famount += 1;
        Active.SetActive(false);
    }
}
