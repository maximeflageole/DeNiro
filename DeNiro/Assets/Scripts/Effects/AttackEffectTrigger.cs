using System;
using UnityEngine;

public class AttackEffectTrigger : EffectTrigger
{
    private AttackData m_attackData;

    protected float m_timer;
    protected float m_currentTimer = 0;
    public Action<AttackData, AttackEffectTrigger> AttackInvoke;

    private void Update()
    {
        m_currentTimer += Time.deltaTime;
        if (m_currentTimer > m_timer)
        {
            TryAttack();
        }
    }

    public void Init(EffectData effectData)
    {
        base.Init(effectData);
        if (effectData is AttackData)
        {
            m_attackData = (AttackData)effectData;
            m_timer = m_attackData.RateOfFire;
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

        if (m_attackData.TargetType == ETarget.Enemies)
        {
            if (m_enemiesInCollider.Count > 0)
            {
                Attack();
            }
        }
        else if (m_attackData.TargetType == ETarget.Towers)
        {
            if (m_towersInCollider.Count > 0)
            {
                Attack();
            }
        }
    }

    protected void Attack()
    {
        if (m_attackData is ProjectileData)
        {
            AttackInvoke.Invoke((ProjectileData)m_attackData, this);
        }
        else if (m_attackData is InstantAttackData)
        {
            AttackInvoke.Invoke((InstantAttackData)m_attackData, this);
        }
        m_currentTimer = 0;
    }

    public TdUnit GetTarget()
    {
        if (m_attackData.TargetType == ETarget.Enemies)
        {
            if (m_enemiesInCollider.Count > 0)
            {
                return(m_enemiesInCollider[0]);
            }
        }
        else if (m_attackData.TargetType == ETarget.Towers)
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

        if (m_attackData.TargetType == ETarget.Enemies)
        {
            if (m_enemiesInCollider.Count > 0)
            {
                target = m_enemiesInCollider[0];
            }
        }
        else if (m_attackData.TargetType == ETarget.Towers)
        {
            if (m_towersInCollider.Count > 0)
            {
                target = m_towersInCollider[0];
            }
        }
        return target != null;
    }
}
