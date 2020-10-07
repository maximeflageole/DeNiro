using UnityEngine;

public class HomingProjectile : Projectile
{
    [SerializeField]
    protected HomingProjectileData m_data;

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

    public override void Init(ProjectileData data, TdEnemy target)
    {
        m_data = (HomingProjectileData)data;
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
