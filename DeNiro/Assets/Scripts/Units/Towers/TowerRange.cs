using UnityEngine;

public class TowerRange : MonoBehaviour
{
    [SerializeField]
    protected Tower m_tower;

    protected TdEnemy m_target;

    private void OnTriggerStay(Collider other)
    {
        if (m_target != null)
        {
            return;
        }
        var enemy = other.GetComponent<TdEnemy>();
        if (enemy != null)
        {
            m_target = enemy;
            m_tower.AssignTarget(m_target);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var enemy = other.GetComponent<TdEnemy>();
        if (m_target != null && enemy == m_target)
        {
            m_target = null;
            m_tower.AssignTarget(null);
        }
    }
}
