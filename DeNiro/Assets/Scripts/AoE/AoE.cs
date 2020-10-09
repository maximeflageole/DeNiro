using UnityEngine;

public class AoE : MonoBehaviour
{
    [SerializeField]
    protected CapsuleCollider m_collider;
    [SerializeField]
    protected float m_yPosition;

    private AoEData m_data;
    protected float m_lifespan;

    public void Init(AoEData data)
    {
        transform.position = new Vector3(transform.position.x, m_yPosition, transform.position.z);
        m_data = data;
        var scale = m_data.Radius / 100.0f;
        transform.localScale = new Vector3(scale, scale, scale);
        m_lifespan = 0.0f;
    }

    protected void Update()
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
        if (m_data.Target == ETarget.Enemies)
        {
            var enemy = other.gameObject.GetComponent<TdEnemy>();
            if (enemy != null)
            {
                if (m_data.Effect == EEffect.Damage)
                {
                    enemy.Damage(m_data.Damage);
                }
            }
        }

        if (m_data.Target == ETarget.Towers)
        {
            var tower = other.gameObject.GetComponent<Tower>();
            if (tower != null)
            {
                if (m_data.Effect == EEffect.AttackSpeedBuff)
                {

                }
            }
        }
    }
}
