using UnityEngine;

public class AoeEffectTrigger : EffectTrigger
{
    private StatEffectData _mStatEffectData;

    public override void Init(EffectData effectData)
    {
        base.Init(effectData);
        if (effectData.GetType() == typeof(StatEffectData))
        {
            _mStatEffectData = (StatEffectData)effectData;
        }
        else
        {
            Debug.LogError("An AOE Effect Trigger does not have a Stat Effect type");
        }
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (m_effectData.TargetType == ETarget.Enemies)
        {
            var enemy = other.GetComponent<TdEnemy>();
            if (enemy != null)
            {
                m_enemiesInCollider.Add(enemy);
                enemy.AddEffect(_mStatEffectData);
            }
            return;
        }
        if (m_effectData.TargetType == ETarget.Towers)
        {
            var tower = other.GetComponent<TdUnit>();
            if (tower != null)
            {
                m_towersInCollider.Add(tower);
                tower.AddEffect(_mStatEffectData);
            }
            return;
        }
    }

    protected override void OnTriggerExit(Collider other)
    {
        if (m_effectData.TargetType == ETarget.Enemies)
        {
            var enemy = other.GetComponent<TdEnemy>();
            if (enemy != null)
            {
                m_enemiesInCollider.Remove(enemy);
                enemy.RemoveEffect(_mStatEffectData);
            }
            return;
        }
        if (m_effectData.TargetType == ETarget.Towers)
        {
            var tower = other.GetComponent<TdUnit>();
            if (tower != null)
            {
                m_towersInCollider.Remove(tower);
                tower.RemoveEffect(_mStatEffectData);
            }
            return;
        }
    }

    public void OnDestroy()
    {
        foreach (var tower in m_towersInCollider)
        {
            tower.RemoveEffect(_mStatEffectData);
        }
        foreach (var enemy in m_enemiesInCollider)
        {
            enemy.RemoveEffect(_mStatEffectData);
        }
    }
}
