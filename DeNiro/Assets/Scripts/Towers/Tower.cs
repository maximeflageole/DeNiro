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
    protected Transform m_radiusTransform;
    [SerializeField]
    protected Transform m_canon;

    protected TdEnemy m_target;
    protected float m_currentFireTimer;

    // Start is called before the first frame update
    void Start()
    {
        m_radiusTransform.localScale = Vector3.one * m_radius / 100.0f;
    }

    // Update is called once per frame
    void Update()
    {
        m_currentFireTimer += Time.deltaTime;
        if (m_currentFireTimer > m_rateOfFire)
        {
            Debug.Log("ReadyToShoot");
            if (m_target != null)
            {
                Shoot();
                ResetTimer();
            }
        }
    }

    protected void Shoot()
    {
        Debug.Log("Shooting");
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

    private void OnTriggerStay(Collider other)
    {
        if (m_target != null)
        {
            return;
        }
        var enemy = other.GetComponent<TdEnemy>();
        if (enemy != null)
        {
            Debug.Log("Target acquired");
            m_target = enemy;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var enemy = other.GetComponent<TdEnemy>();
        if (m_target != null && enemy == m_target)
        {
            m_target = null;
        }
    }
}
