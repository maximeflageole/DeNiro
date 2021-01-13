using UnityEngine;

public class HomingProjectile : Projectile
{
    [SerializeField]
    protected ParticleSystem m_particleSystem;
    [SerializeField]
    protected HomingProjectileData m_data;

    private bool m_reachedTarget = false;

    protected TdUnit m_target;

    protected void OnTriggerEnter(Collider other)
    {
        var enemy = other.GetComponent<TdEnemy>();
        if (enemy != null && enemy == m_target && !m_reachedTarget)
        {
            enemy.Damage(GetFinalDamage(m_data, m_target, m_damageMultiplier));
            m_reachedTarget = true;
            m_particleSystem.Clear(true);
            GetComponent<ParticleCollisionInstance>().OnCollisionExternal();
        }
    }

    public override void Init(ProjectileData data, TdUnit target, float damageMultiplier)
    {
        m_data = (HomingProjectileData)data;
        m_target = target;
        m_damageMultiplier = damageMultiplier;
    }

    private void Update()
    {
        if (m_target == null)
        {
            Destroy(gameObject);
            return;
        }

        var velocity = Vector3.zero;

        if (!m_reachedTarget)
        {
            var directionalVector = (m_target.transform.position - transform.position).normalized;
            velocity = directionalVector * Time.deltaTime * m_data.ProjectileSpeed;
        }
        
        GetComponent<Rigidbody>().velocity = velocity;
    }
}
