using UnityEngine;

//TODO MF: Remove ALL of this since I now use the Effect and EffectTrigger system

public class AoE : MonoBehaviour
{
    [SerializeField]
    protected CapsuleCollider m_collider;
    [SerializeField]
    protected float m_yPosition;

    private ArtilleryData m_data;
    protected float m_lifespan;
    protected float m_damageMultiplier;

    public void Init(ArtilleryData data, float damageMultiplier)
    {
        transform.position = new Vector3(transform.position.x, m_yPosition, transform.position.z);
        m_data = data;
        var scale = m_data.AoEData.Radius / 100.0f;
        transform.localScale = new Vector3(scale, scale, scale);
        m_lifespan = 0.0f;
        m_damageMultiplier = damageMultiplier;
    }

    protected void Update()
    {
        if (!m_data.AoEData.IsPermanent)
        {
            m_lifespan += Time.deltaTime;
            if (m_lifespan > m_data.AoEData.EffectTimeStop)
            {
                m_collider.enabled = false;
                return;
            }
            if (m_lifespan > m_data.AoEData.EffectTimeStart)
            {
                m_collider.enabled = true;
                return;
            }
            m_collider.enabled = false;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (m_data.AoEData.Target == ETarget.Enemies)
        {
            var enemy = other.gameObject.GetComponent<TdEnemy>();
            if (enemy != null)
            {
                if (m_data.AoEData.Effect == EStat.Damage)
                {
                    enemy.Damage(Projectile.GetFinalDamage(m_data, enemy, m_damageMultiplier));
                }
            }
        }
    }
}
