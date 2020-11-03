using UnityEngine;

[CreateAssetMenu(fileName = "CreatureData", menuName = "Creature Data")]
public class CreatureData : ScriptableObject
{
    public EUnit Id;
    public TowerData TowerData;
    public EnemyData EnemyData;
}
