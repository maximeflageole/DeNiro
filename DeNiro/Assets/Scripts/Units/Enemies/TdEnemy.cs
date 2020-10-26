using System;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class TdEnemy : TdUnit
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

    protected CreatureData m_creatureData;

    protected Waypoint m_nextWaypoint;
    protected Vector3 m_directionalVector;

    public Action OnDeathCallback;

    public void AssignWaypoint(Waypoint waypoint)
    {
        m_nextWaypoint = waypoint;
        m_directionalVector = (m_nextWaypoint.transform.position - transform.position).normalized;
    }

    protected void Update()
    {
        GetComponent<Rigidbody>().velocity = m_directionalVector * m_speed * Time.deltaTime * GetSpeedMultiplier();

        m_hpImage.fillAmount = m_currentHp / m_maxHp;
        UiRotationUpdate();

        if (Input.GetKeyDown(KeyCode.X))
        {
            Damage(m_maxHp / 2.0f);
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
            if (randomValue < GameManager.RateOfConversion)
            {
                PlayerControls.Instance.CollectTower(m_creatureData);
            }
        }
        Destroy(gameObject);
    }

    protected float GetSpeedMultiplier()
    {
        var speedMultiplier = Mathf.Max(1.0f, GetEffectMultiplier(EEffect.MovementSpeedBuff, false));
        speedMultiplier /= Mathf.Max(1.0f, GetEffectMultiplier(EEffect.MovementSpeedDebuff, false));

        return speedMultiplier;
    }

    protected float GetCalculatedDamage(float damageAmount)
    {
        var calculatedDamage = damageAmount * Mathf.Max(1.0f, GetEffectMultiplier(EEffect.DamageMultiplierDebuff, false));
        calculatedDamage /= Mathf.Max(1.0f, GetEffectMultiplier(EEffect.DamageMultiplierBuff, false));

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
