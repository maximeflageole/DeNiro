using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    public float m_damageMultiplier = 1.0f;

    public virtual void Init(ProjectileData data, TdEnemy target, float damageMultiplier)
    {
    }

    public virtual void Init(ProjectileData data, Vector3 objective, float damageMultiplier)
    {
    }
}
