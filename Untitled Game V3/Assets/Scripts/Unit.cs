using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Unit : MonoBehaviour
{
    public GameObject UnitPrefab;

	public string unitName;

	public int damage;

    public int maxMP;
    public int currentMP;
    SpriteRenderer sr;
    public int maxHP;
	public int currentHP;
    void Awake()
    {

    }
    public bool TakeDamage(int dmg)
	{
		currentHP -= dmg;
        if (currentHP < 0) currentHP = 0;
		if (currentHP <= 0)
			return true;
		else
			return false;
	}
    public void ChangeMP(int amount)
    {
        currentMP += amount;
        if (currentMP > maxMP)
            currentMP = maxMP;
        if (currentMP < 0)
            currentMP = 0;
    }
	public void ChangeHP(int amount)
	{
		currentHP += amount;
		if (currentHP > maxHP)
			currentHP = maxHP;
        if (currentHP < 0)
            currentHP = 0;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            DontDestroyOnLoad(gameObject);
            SceneManager.LoadScene("BattleScene");
            BattleSystem.enemyPrefab = UnitPrefab;
            StartCoroutine(kill(0.01f));


        }
    }
    public IEnumerator kill(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}
