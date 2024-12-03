using UnityEngine;

[CreateAssetMenu(menuName = "GameData/PlayerData")]
public class PlayerData : ScriptableObject
{
    public int damage;
    public int maxMP;
    public int currentMP;
    public int maxHP;
    public int currentHP;
    public int speed;
    public Vector3 OWPos = new Vector3 (12345f, 0f, 0f);
    public string[] actvMoves;
}