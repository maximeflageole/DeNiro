using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TowerData", menuName = "Tower Data")]
public class TowerData : ScriptableObject
{
    public List<StatEffect> StatEffects = new List<StatEffect>();
    public List<ProjectileEffect> ProjectileEffects = new List<ProjectileEffect>();
    public Sprite TowerSprite;
    public Mesh TowerMesh;
    public List<Material> Materials;
    public TowerStats Stats;
}
