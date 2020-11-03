using System;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class TdEnemy: TdUnit
{
    [SerializeField]
    protected float m_speed = 10.0f;
    [SerializeField]
    protected float m_maxHp = 100.0f;
    [SerializeField]
    protected Image m_hpImage;
    [SerializeField]
    protected Canvas m_healthCanvas;

    protected float m_currentHp;
    protected EnemyData m_data;

    protected Waypoint m_nextWaypoint;
    protected float m_waypointDistance = 1;
    protected Vector3 m_directionalVector;

    public Action OnDeathCallback;

    public void AssignWaypoint(Waypoint waypoint)
    {
        m_nextWaypoint = waypoint;
        var distanceVector = m_nextWaypoint.transform.position - transform.position;
        m_waypointDistance = Mathf.Abs(distanceVector.magnitude);
        Debug.Log("Assigning next waypoint in " + m_waypointDistance);
        m_directionalVector = (m_nextWaypoint.transform.position - transform.position).normalized;
    }

    protected void Update()
    {
        var directionalSpeed = m_directionalVector * m_speed * Time.deltaTime * GetSpeedMultiplier();
        GetComponent<Rigidbody>().velocity = directionalSpeed;
        m_waypointDistance -= (directionalSpeed * Time.deltaTime).magnitude;

        WaypointCheck();

        m_hpImage.fillAmount = m_currentHp / m_maxHp;
        UiRotationUpdate();

        if (Input.GetKeyDown(KeyCode.H))
        {
            Damage(m_maxHp / 2.0f);
        }
    }

    protected void WaypointCheck()
    {
        if (m_waypointDistance <= 0)
        {
            if (m_nextWaypoint.GetNextWaypoint() != null)
            {
                AssignWaypoint(m_nextWaypoint.GetNextWaypoint());
                return;
            }
            Die(false);
        }
    }

    protected void UiRotationUpdate()
    {
        Quaternion camrot = Camera.main.transform.rotation;
        m_healthCanvas.transform.rotation = camrot;
    }

    public void Damage(float damageAmount)
    {
        m_currentHp = Mathf.Clamp(m_currentHp - GetCalculatedDamage(damageAmount), 0.0f, m_maxHp);
        if (m_currentHp <= 0.0f)
        {
            Die();
        }
    }

    public void Die(bool wasKilled = true)
    {
        OnDeathCallback?.Invoke();
        if (wasKilled)
        {
            var randomValue = Random.Range(0.0f, 1.0f);
            if (randomValue < GameManager.RATE_OF_CONVERSION)
            {
                PlayerControls.Instance.CollectTower(m_creatureData);
            }
        }
        Destroy(gameObject);
    }

    protected float GetSpeedMultiplier()
    {
        var speedMultiplier = Mathf.Max(1.0f, GetEffectMultiplier(EStat.MovementSpeed, false));
        speedMultiplier /= Mathf.Max(1.0f, GetEffectMultiplier(EStat.MovementSpeedDebuff, false));

        return speedMultiplier;
    }

    protected float GetCalculatedDamage(float damageAmount)
    {
        var calculatedDamage = damageAmount * Mathf.Max(1.0f, GetEffectMultiplier(EStat.AttackDebuff, false));
        calculatedDamage /= Mathf.Max(1.0f, GetEffectMultiplier(EStat.Attack, false));

        return calculatedDamage;
    }

    public void AssignData(CreatureData data)
    {
        m_creatureData = data;
        m_data = data.EnemyData;
        m_currentHp = m_data.MaxHealth;
        m_speed = m_data.Speed;
    }
}
