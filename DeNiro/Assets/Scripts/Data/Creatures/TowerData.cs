using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TowerData", menuName = "Tower Data")]
public class TowerData : ScriptableObject
{
    public List<StatEffect> StatEffects = new List<StatEffect>();
    public List<ProjectileAttackEffect> ProjectileEffects = new List<ProjectileAttackEffect>();
    public List<InstantAttackEffect> InstantEffects = new List<InstantAttackEffect>();
    public Sprite TowerSprite;
    public TowerStats Stats;
    public GameObject Prefab;
}
