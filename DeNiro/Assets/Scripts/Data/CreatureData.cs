using UnityEngine;

[CreateAssetMenu(fileName = "CreatureData", menuName = "Creature Data")]
public class CreatureData : ScriptableObject
{
    public TowerData TowerData;
    public EnemyData EnemyData;
}
