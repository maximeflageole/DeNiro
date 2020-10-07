using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField]
    protected GameObject m_homingProjectile;
    [SerializeField]
    protected GameObject m_artilleryProjectile;
    [SerializeField]
    protected TowerRange m_radiusTransform;
    [SerializeField]
    protected Transform m_canon;
    [SerializeField]
    protected MeshRenderer m_towerMeshRenderer;
    [SerializeField]
    protected MeshFilter m_towerMeshFilter;

    protected TdEnemy m_target;
    protected float m_currentFireTimer;
    protected TowerData m_data;

    public void Init(TowerData towerData)
    {
        m_towerMeshFilter.mesh = towerData.TowerMesh;
        m_towerMeshRenderer.materials = towerData.Materials.ToArray();
        m_data = towerData;
    }

    void Start()
    {
        m_radiusTransform.transform.localScale = Vector3.one * m_data.Radius / 100.0f;
    }

    void Update()
    {
        m_currentFireTimer += Time.deltaTime;
        if (m_currentFireTimer > m_data.RateOfFire)
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
        if (m_data.ProjectileData.GetType() == typeof(ArtilleryData))
        {
            var artilleryProjectile = Instantiate(m_artilleryProjectile, m_canon.transform.position, Quaternion.identity, m_canon).GetComponent<ArtilleryProjectile>();
            if (artilleryProjectile != null)
            {
                artilleryProjectile.Init(m_data.ProjectileData, m_target.transform.position);
            }
            return;
        }

        else if (m_data.ProjectileData.GetType() == typeof(HomingProjectileData))
        {
            var homingProjectile = Instantiate(m_homingProjectile, m_canon.transform.position, Quaternion.identity, m_canon).GetComponent<HomingProjectile>();
            if (homingProjectile != null)
            {
                homingProjectile.Init(m_data.ProjectileData, m_target);
            }
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
