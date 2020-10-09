using UnityEngine;

public class AoE : MonoBehaviour
{
    [SerializeField]
    private CapsuleCollider m_collider;
    [SerializeField]
    private float m_yPosition;

    private AoEData m_data;
    private float m_lifespan;

    public void Init(AoEData data)
    {
        transform.position = new Vector3(transform.position.x, m_yPosition, transform.position.z);
        m_data = data;
        var scale = m_data.Radius / 100.0f;
        transform.localScale = new Vector3(scale, scale, scale);
        m_lifespan = 0.0f;
    }

    private void Update()
    {
        m_lifespan += Time.deltaTime;
        if (m_lifespan > m_data.DamageTimeStop)
        {
            m_collider.enabled = false;
            return;
        }
        if (m_lifespan > m_data.DamageTimeStart)
        {
            m_collider.enabled = true;
            return;
        }
        m_collider.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        var enemy = other.gameObject.GetComponent<TdEnemy>();
        if (enemy != null)
        {
            enemy.Damage(m_data.Damage);
        }
    }
}
