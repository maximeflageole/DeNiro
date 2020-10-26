using System.Collections.Generic;
using UnityEngine;

public class EffectTrigger : MonoBehaviour
{
    [SerializeField]
    protected Effect m_onEnterEffect = null;
    protected List<TdEnemy> m_enemiesInCollider = new List<TdEnemy>();
    protected List<Tower> m_towersInCollider = new List<Tower>();

    public void Init(Effect onEnterEffect)
    {
        var radius = onEnterEffect.Radius/100.0f;
        transform.localScale = new Vector3(radius, 5.0f, radius);
        m_onEnterEffect = onEnterEffect;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (m_onEnterEffect.Target == ETarget.Enemies)
        {
            var enemy = other.GetComponent<TdEnemy>();
            if (enemy != null)
            {
                m_enemiesInCollider.Add(enemy);
                enemy.AddEffect(m_onEnterEffect);
            }
            return;
        }
        if (m_onEnterEffect.Target == ETarget.Towers)
        {
            var tower = other.GetComponent<Tower>();
            if (tower != null)
            {
                m_towersInCollider.Add(tower);
                tower.AddEffect(m_onEnterEffect);
            }
            return;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (m_onEnterEffect.Target == ETarget.Enemies)
        {
            var enemy = other.GetComponent<TdEnemy>();
            if (enemy != null)
            {
                m_enemiesInCollider.Remove(enemy);
                enemy.RemoveEffect(m_onEnterEffect);
            }
            return;
        }
        if (m_onEnterEffect.Target == ETarget.Towers)
        {
            var tower = other.GetComponent<Tower>();
            if (tower != null)
            {
                m_towersInCollider.Remove(tower);
                tower.RemoveEffect(m_onEnterEffect);
            }
            return;
        }
    }

    public void OnDestroy()
    {
        foreach (var tower in m_towersInCollider)
        {
            tower.RemoveEffect(m_onEnterEffect);
        }
        foreach (var enemy in m_enemiesInCollider)
        {
            enemy.RemoveEffect(m_onEnterEffect);
        }
    }
}
