using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class TdEnemy: TdUnit
{
    protected static float ROTATION_SPEED = 15.0f;

    [SerializeField]
    protected float m_speed = 10.0f;
    [SerializeField]
    protected Animator m_animator;
    [SerializeField]
    protected GameObject m_body;
    [SerializeField]
    protected GameObject m_deathGO;

    protected EnemyData m_data;

    protected bool m_inDeathAnim;
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
        if (!m_inDeathAnim)
        {
            base.Update();
            Move();
        }
    }

    protected virtual void Move()
    {
        var directionalSpeed = m_directionalVector * m_speed * Time.fixedDeltaTime * GetSpeedMultiplier();
        GetComponent<Rigidbody>().velocity = directionalSpeed;
        m_waypointDistance -= (directionalSpeed * Time.deltaTime).magnitude;

        WaypointCheck();

        Quaternion OriginalRot = transform.rotation;
        transform.LookAt(m_nextWaypoint.transform);
        Quaternion NewRot = transform.rotation;
        transform.rotation = OriginalRot;
        transform.rotation = Quaternion.Lerp(transform.rotation, NewRot, ROTATION_SPEED * Time.deltaTime);
        m_animator.SetFloat("Speed", directionalSpeed.magnitude);
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
        m_inDeathAnim = true;
        m_body.SetActive(false);
        m_animator.enabled = false;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        if (m_hpImage != null)
        {
            m_hpImage.enabled = false;
        }

        if (m_deathGO != null)
        {
            m_deathGO.SetActive(true);
            Invoke("OnDeathComplete", m_deathGO.GetComponent<Animation>().clip.length);
        }
        else
        {
            OnDeathComplete();
        }

        var randomValue = Random.Range(0.0f, 1.0f);
        if (randomValue < GameManager.RATE_OF_CONVERSION)
        {
            PlayerControls.Instance.CollectTower(m_creatureData);
        }
        PlayerControls.Instance.DistributeXp(m_data.BaseXpValue);
    }

    public void OnDeathComplete()
    {
        Destroy(gameObject);
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
