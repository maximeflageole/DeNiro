using UnityEngine;

public class ArtilleryProjectile : Projectile
{
    protected ArtilleryData m_data;
    [SerializeField]
    protected GameObject m_aoe;
    protected Vector3 m_objective;
    protected Vector3 m_initialPoint;
    protected float m_lifetime = 0.0f;

    public override void Init(ProjectileData data, Vector3 objective)
    {
        m_initialPoint = transform.position;
        m_objective = objective;
        m_data = (ArtilleryData)data;
    }

    private void Update()
    {
        m_lifetime += Time.deltaTime;
        if (m_lifetime < m_data.Lifespan)
        {
            float timePercentage = m_lifetime / m_data.Lifespan;
            transform.position = Vector3.Lerp(m_initialPoint, m_objective, timePercentage);
            transform.position = new Vector3(transform.position.x, m_data.VerticalPositionCurve.Evaluate(timePercentage), transform.position.z);
            return;
        }
        OnPositionReached();
    }

    protected void OnPositionReached()
    {
        var aoe = Instantiate(m_aoe, transform.position, Quaternion.identity).GetComponent<AoE>();
        aoe.transform.Rotate(Vector3.right, 90.0f);
        aoe.Init(m_data.AoEData);
        Destroy(gameObject);
    }
}
