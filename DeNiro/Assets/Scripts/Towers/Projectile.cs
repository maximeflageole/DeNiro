using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    protected ProjectileData m_data;

    protected TdEnemy m_target;

    protected void OnTriggerEnter(Collider other)
    {
        var enemy = other.GetComponent<TdEnemy>();
        if (enemy == m_target)
        {
            enemy.Damage(m_data.Damage);
            Destroy(gameObject);
        }
    }

    public void Init(ProjectileData data)
    {
        m_data = data;
    }

    public void Launch(TdEnemy target)
    {
        m_target = target;
    }

    private void Update()
    {
        if (m_target == null)
        {
            Destroy(gameObject);
            return;
        }

        var directionalVector = (m_target.transform.position - transform.position).normalized;
        GetComponent<Rigidbody>().velocity = directionalVector * Time.deltaTime * m_data.ProjectileSpeed;
    }
}
