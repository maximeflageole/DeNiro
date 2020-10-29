using UnityEngine;

//TODO MF: Remove ALL of this since I now use the Effect and EffectTrigger system

public class AoE : MonoBehaviour
{
    [SerializeField]
    protected CapsuleCollider m_collider;
    [SerializeField]
    protected float m_yPosition;

    private AoEData m_data;
    protected float m_lifespan;
    protected float m_damageMultiplier;

    public void Init(AoEData data, float damageMultiplier)
    {
        transform.position = new Vector3(transform.position.x, m_yPosition, transform.position.z);
        m_data = data;
        var scale = m_data.Radius / 100.0f;
        transform.localScale = new Vector3(scale, scale, scale);
        m_lifespan = 0.0f;
        m_damageMultiplier = damageMultiplier;
    }

    protected void Update()
    {
        if (!m_data.IsPermanent)
        {
            m_lifespan += Time.deltaTime;
            if (m_lifespan > m_data.EffectTimeStop)
            {
                m_collider.enabled = false;
                return;
            }
            if (m_lifespan > m_data.EffectTimeStart)
            {
                m_collider.enabled = true;
                return;
            }
            m_collider.enabled = false;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (m_data.Target == ETarget.Enemies)
        {
            var enemy = other.gameObject.GetComponent<TdEnemy>();
            if (enemy != null)
            {
                if (m_data.Effect == EStat.Damage)
                {
                    enemy.Damage(m_data.Magnitude * m_damageMultiplier);
                }
            }
        }
    }
}
