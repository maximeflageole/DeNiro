using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField]
    protected float m_radius = 50.0f;
    [SerializeField]
    protected float m_rateOfFire = 1.0f;
    [SerializeField]
    protected GameObject m_projectile;
    [SerializeField]
    protected TowerRange m_radiusTransform;
    [SerializeField]
    protected Transform m_canon;

    protected TdEnemy m_target;
    protected float m_currentFireTimer;

    void Start()
    {
        m_radiusTransform.transform.localScale = Vector3.one * m_radius / 100.0f;
    }

    void Update()
    {
        m_currentFireTimer += Time.deltaTime;
        if (m_currentFireTimer > m_rateOfFire)
        {
            if (m_target != null)
            {
                Shoot();
                ResetTimer();
            }
        }
    }

    protected void Shoot()
    {
        var projectile = Instantiate(m_projectile, m_canon.transform.position, Quaternion.identity, m_canon).GetComponent<Projectile>();
        if (projectile != null)
        {
            projectile.Launch(m_target);
        }
    }

    protected void ResetTimer()
    {
        m_currentFireTimer = 0.0f;
    }

    public void AssignTarget(TdEnemy target)
    {
        m_target = target;
    }
}
