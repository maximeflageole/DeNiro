using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "EnemyData", menuName = "Enemy Data")]
public class EnemyData : ScriptableObject
{
    public float Speed = 200.0f;
    public uint MaxHealth = 100;
    public uint BaseXpValue = 10;
}
