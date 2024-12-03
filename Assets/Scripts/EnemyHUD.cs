using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHUD : BattleHUD
{
    public GameObject Button;
    public int EnemyRep;

    private void Start()
    {
        Button.GetComponent<Button>().onClick.AddListener(SetTargetedEnemy);
    }

    public void SetTargetedEnemy()
    {
        BattleSystem.targetedEnemy = EnemyRep;
        Button.GetComponent<Button>().onClick.RemoveListener(SetTargetedEnemy);
    }
}
