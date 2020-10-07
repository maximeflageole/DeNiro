using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    public virtual void Init(ProjectileData data, TdEnemy target)
    {
    }

    public virtual void Init(ProjectileData data, Vector3 objective)
    {
    }
}
