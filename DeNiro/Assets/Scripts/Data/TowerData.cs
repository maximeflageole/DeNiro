using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TowerData", menuName = "Tower Data")]
public class TowerData : ScriptableObject
{
    public float Radius = 50.0f;
    public float RateOfFire = 1.0f; //Rate of fire is how long in seconds it takes to launch a projectile
    public ProjectileData ProjectileData;
    public Sprite TowerSprite;
    public Mesh TowerMesh;
    public List<Material> Materials;
}
