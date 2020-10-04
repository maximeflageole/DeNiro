using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileData", menuName = "Projectile Data")]
public class ProjectileData : ScriptableObject
{
    public float ProjectileSpeed = 500.0f;
    public uint Damage = 10;
}
