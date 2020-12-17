using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    public float m_damageMultiplier = 1.0f;

    public virtual void Init(ProjectileData data, TdUnit target, float damageMultiplier)
    {
    }

    public virtual void Init(ProjectileData data, Vector3 objective, float damageMultiplier)
    {
    }

    public static float GetFinalDamage(AttackData data, TdUnit target, float damageMultiplier)
    {
        return (data.Damage * damageMultiplier * GameManager.Instance.TypesManager.GetDamageTypeMultiplier(target.GetCreatureData(), data.DamageType));
    }
}
