using UnityEngine;

[CreateAssetMenu(fileName = "HomingProjectileData", menuName = "Homing Projectile Data")]
public class HomingProjectileData : ProjectileData
{
    public float ProjectileSpeed = 500.0f;
    public uint Damage = 10;
}
