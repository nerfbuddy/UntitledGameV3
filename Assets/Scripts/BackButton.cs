using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackButton : MonoBehaviour
{
    GameObject BatSys;
    BattleSystem BS;
    [SerializeField] GameObject Button0;
    [SerializeField] GameObject Button1;
    [SerializeField] GameObject Button2;
    [SerializeField] GameObject Button3;
    // Start is called before the first frame update
    void Start()
    {
        BatSys = GameObject.Find("Battle System");
        BS = BatSys.GetComponent<BattleSystem>();
        
    }
    public void Back()
    {
        Button0.GetComponent<Button>().onClick.RemoveAllListeners();
        Button1.GetComponent<Button>().onClick.RemoveAllListeners();
        Button2.GetComponent<Button>().onClick.RemoveAllListeners();
        Button3.GetComponent<Button>().onClick.RemoveAllListeners();
        BS.ToggleTargetBox(false);
        BS.ToggleUIBox(true);
        BS.ToggleButtons(true);
    }
}
