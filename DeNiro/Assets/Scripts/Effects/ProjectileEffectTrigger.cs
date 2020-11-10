using System;
using UnityEngine;

public class ProjectileEffectTrigger : EffectTrigger
{
    private ProjectileEffect m_projectileEffect;

    protected float m_timer;
    protected float m_currentTimer = 0;
    public Action<ProjectileData, TdUnit> ShootInvoke;

    private void Update()
    {
        m_currentTimer += Time.deltaTime;
        if (m_currentTimer > m_timer)
        {
            TryShoot();
        }
    }

    public override void Init(Effect effect)
    {
        base.Init(effect);
        if (effect.GetType() == typeof(ProjectileEffect))
        {
            m_projectileEffect = (ProjectileEffect)effect;
            m_timer = m_projectileEffect.RateOfFire;
        }
        else
        {
            Debug.LogError("A projectile Effect Trigger does not have a projectile Effect type");
        }
    }

    private void TryShoot()
    {
        //This is ugly af, but the OnTriggerExit is NOT called if we destroy an enemy, thanks unity for that
        while (m_enemiesInCollider.Count > 0 && m_enemiesInCollider[0] == null)
        {
            m_enemiesInCollider.RemoveAt(0);
        }
        while (m_towersInCollider.Count > 0 && m_towersInCollider[0] == null)
        {
            m_towersInCollider.RemoveAt(0);
        }

        if (m_projectileEffect.Target == ETarget.Enemies)
        {
            if (m_enemiesInCollider.Count > 0)
            {
                Shoot(m_enemiesInCollider[0]);
            }
        }
        else if (m_projectileEffect.Target == ETarget.Towers)
        {
            if (m_towersInCollider.Count > 0)
            {
                Shoot(m_towersInCollider[0]);
            }
        }
    }

    protected void Shoot(TdUnit unit)
    {
        ShootInvoke.Invoke(m_projectileEffect.ProjectileData, unit);
        m_currentTimer = 0;
    }
}
