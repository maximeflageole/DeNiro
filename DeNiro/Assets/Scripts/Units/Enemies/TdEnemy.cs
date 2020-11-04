using UnityEngine;
using Random = UnityEngine.Random;

public class TdEnemy: TdUnit
{
    [SerializeField]
    protected float m_speed = 10.0f;

    protected EnemyData m_data;

    protected Waypoint m_nextWaypoint;
    protected float m_waypointDistance = 1;
    protected Vector3 m_directionalVector;

    public void AssignWaypoint(Waypoint waypoint)
    {
        m_nextWaypoint = waypoint;
        var distanceVector = m_nextWaypoint.transform.position - transform.position;
        m_waypointDistance = Mathf.Abs(distanceVector.magnitude);
        Debug.Log("Assigning next waypoint in " + m_waypointDistance);
        m_directionalVector = (m_nextWaypoint.transform.position - transform.position).normalized;
    }

    protected override void Update()
    {
        base.Update();
        var directionalSpeed = m_directionalVector * m_speed * Time.deltaTime * GetSpeedMultiplier();
        GetComponent<Rigidbody>().velocity = directionalSpeed;
        m_waypointDistance -= (directionalSpeed * Time.deltaTime).magnitude;

        WaypointCheck();

        if (Input.GetKeyDown(KeyCode.H))
        {
            Damage(m_health.Max / 2.0f);
        }
    }

    protected void WaypointCheck()
    {
        if (m_waypointDistance <= 0)
        {
            m_nextWaypoint.GetReached();
            if (m_nextWaypoint.GetNextWaypoint() != null)
            {
                AssignWaypoint(m_nextWaypoint.GetNextWaypoint());
                return;
            }
            Die(false);
        }
    }

    protected override void GetKilled()
    {
        var randomValue = Random.Range(0.0f, 1.0f);
        if (randomValue < GameManager.RATE_OF_CONVERSION)
        {
            PlayerControls.Instance.CollectTower(m_creatureData);
        }
        PlayerControls.Instance.DistributeXp(m_data.BaseXpValue);
    }

    protected float GetSpeedMultiplier()
    {
        var speedMultiplier = Mathf.Max(1.0f, GetEffectMultiplier(EStat.MovementSpeed, false));
        speedMultiplier /= Mathf.Max(1.0f, GetEffectMultiplier(EStat.MovementSpeedDebuff, false));

        return speedMultiplier;
    }

    public void AssignData(CreatureData data)
    {
        m_creatureData = data;
        m_data = data.EnemyData;
        m_speed = m_data.Speed;
        Init(m_data.MaxHealth);
    }
}
