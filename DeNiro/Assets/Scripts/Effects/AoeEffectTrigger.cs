using UnityEngine;

public class AoeEffectTrigger : EffectTrigger
{
    private StatEffect m_statEffect;

    public override void Init(Effect effect)
    {
        base.Init(effect);
        if (effect.GetType() == typeof(StatEffect))
        {
            m_statEffect = (StatEffect)effect;
        }
        else
        {
            Debug.LogError("An AOE Effect Trigger does not have a Stat Effect type");
        }
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (m_effect.Target == ETarget.Enemies)
        {
            var enemy = other.GetComponent<TdEnemy>();
            if (enemy != null)
            {
                m_enemiesInCollider.Add(enemy);
                enemy.AddEffect(m_statEffect);
            }
            return;
        }
        if (m_effect.Target == ETarget.Towers)
        {
            var tower = other.GetComponent<TdUnit>();
            if (tower != null)
            {
                m_towersInCollider.Add(tower);
                tower.AddEffect(m_statEffect);
            }
            return;
        }
    }

    protected override void OnTriggerExit(Collider other)
    {
        if (m_effect.Target == ETarget.Enemies)
        {
            var enemy = other.GetComponent<TdEnemy>();
            if (enemy != null)
            {
                m_enemiesInCollider.Remove(enemy);
                enemy.RemoveEffect(m_statEffect);
            }
            return;
        }
        if (m_effect.Target == ETarget.Towers)
        {
            var tower = other.GetComponent<TdUnit>();
            if (tower != null)
            {
                m_towersInCollider.Remove(tower);
                tower.RemoveEffect(m_statEffect);
            }
            return;
        }
    }

    public void OnDestroy()
    {
        foreach (var tower in m_towersInCollider)
        {
            tower.RemoveEffect(m_statEffect);
        }
        foreach (var enemy in m_enemiesInCollider)
        {
            enemy.RemoveEffect(m_statEffect);
        }
    }
}
