using System;
using UnityEngine;

public class InstantEffectTrigger : EffectTrigger
{
    private AttackEffect m_attackEffect;

    protected float m_timer;
    protected float m_currentTimer = 0;
    public Action<AttackData, InstantEffectTrigger> AttackInvoke;

    private void Update()
    {
        m_currentTimer += Time.deltaTime;
        if (m_currentTimer > m_timer)
        {
            TryAttack();
        }
    }

    public override void Init(Effect effect)
    {
        base.Init(effect);
        var attackEffect = (AttackEffect)effect;
        if (attackEffect != null)
        {
            m_attackEffect = attackEffect;
            m_timer = m_attackEffect.RateOfFire;
        }
        else
        {
            Debug.LogError("An attack Effect Trigger does not have an attack Effect type");
        }
    }

    private void TryAttack()
    {
        //This is ugly af, but the OnTriggerExit is NOT called if we destroy an enemy, thanks unity for that
        while (m_enemiesInCollider.Count > 0 && (m_enemiesInCollider[0] == null || m_enemiesInCollider[0].IsDying))
        {
            m_enemiesInCollider.RemoveAt(0);
        }
        while (m_towersInCollider.Count > 0 && m_towersInCollider[0] == null)
        {
            m_towersInCollider.RemoveAt(0);
        }

        if (m_attackEffect.Target == ETarget.Enemies)
        {
            if (m_enemiesInCollider.Count > 0)
            {
                Attack();
            }
        }
        else if (m_attackEffect.Target == ETarget.Towers)
        {
            if (m_towersInCollider.Count > 0)
            {
                Attack();
            }
        }
    }

    protected void Attack()
    {
        if (m_attackEffect.GetType() == typeof(ProjectileAttackEffect))
        {
            AttackInvoke.Invoke(((ProjectileAttackEffect)m_attackEffect).ProjectileData, this);
        }
        else if (m_attackEffect.GetType() == typeof(InstantAttackEffect))
        {
            AttackInvoke.Invoke(((InstantAttackEffect)m_attackEffect).InstantAttackData, this);
        }
        m_currentTimer = 0;
    }

    public TdUnit GetTarget()
    {
        if (m_attackEffect.Target == ETarget.Enemies)
        {
            if (m_enemiesInCollider.Count > 0)
            {
                return(m_enemiesInCollider[0]);
            }
        }
        else if (m_attackEffect.Target == ETarget.Towers)
        {
            if (m_towersInCollider.Count > 0)
            {
                return(m_towersInCollider[0]);
            }
        }
        return null;
    }

    public bool TryGetTarget( out TdUnit target)
    {
        target = null;

        if (m_attackEffect.Target == ETarget.Enemies)
        {
            if (m_enemiesInCollider.Count > 0)
            {
                target = m_enemiesInCollider[0];
            }
        }
        else if (m_attackEffect.Target == ETarget.Towers)
        {
            if (m_towersInCollider.Count > 0)
            {
                target = m_towersInCollider[0];
            }
        }
        return target != null;
    }
}
