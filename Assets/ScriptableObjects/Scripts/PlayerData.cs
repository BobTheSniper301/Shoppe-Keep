using UnityEngine;

[CreateAssetMenu]
public class PlayerData : ScriptableObject
{
    public float currentHealth;
    public float maxHealth;
    public float currentMana;
    public float maxMana;
    public float gold;
    public Vector3 playerPos;
}
