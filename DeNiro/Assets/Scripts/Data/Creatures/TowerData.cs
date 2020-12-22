using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TowerData", menuName = "Creatures/Tower Data")]
public class TowerData : ScriptableObject
{
    public Sprite TowerSprite;
    public TowerStats Stats;
    public GameObject Prefab;
    public List<AbilityData> Abilities = new List<AbilityData>();
}
