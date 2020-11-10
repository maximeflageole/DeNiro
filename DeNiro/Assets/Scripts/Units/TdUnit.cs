using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TdUnit: MonoBehaviour
{
    public CreatureData m_creatureData { get; protected set; }

    public Action OnDeathCallback;
    public UnitStats m_stats { get; protected set; }

    [SerializeField]
    protected Image m_hpImage;
    [SerializeField]
    protected Canvas m_healthCanvas;
    [SerializeField]
    protected Color m_damageTextColor;

    protected ResourceContainer m_health;
    protected Dictionary<EStat, List<Effect>> m_effectsDictionary = new Dictionary<EStat, List<Effect>>();
    protected uint level = 1;
    protected bool m_markedForDestruction;

    public void Init(float maxHp)
    {
        if (m_health == null)
        {
            m_health = new ResourceContainer();
        }
        m_health.Init(maxHp);
    }

    protected virtual void Update()
    {
        if (m_hpImage != null)
        {
            m_hpImage.fillAmount = m_health.Current / m_health.Max;
        }
    }

    [SerializeField]
    protected GameObject m_textDisplayPrefab;

    public void AddEffect(Effect effect)
    {
        if (!m_effectsDictionary.ContainsKey(effect.EffectType))
        {
            m_effectsDictionary.Add(effect.EffectType, new List<Effect>());
        }
        m_effectsDictionary[effect.EffectType].Add(effect);
    }

    public void RemoveEffect(Effect effect)
    {
        if (!m_effectsDictionary.ContainsKey(effect.EffectType))
        {
            return;
        }
        for (var i = 0; i < m_effectsDictionary[effect.EffectType].Count; i++)
        {
            if (m_effectsDictionary[effect.EffectType][i] == effect)
            {
                m_effectsDictionary[effect.EffectType].RemoveAt(i);
                i--;
            }
        }
        if (m_effectsDictionary[effect.EffectType].Count == 0)
        {
            m_effectsDictionary.Remove(effect.EffectType);
        }
    }

    protected float GetEffectMultiplier(EStat effect, bool startsAt1 = true)
    {
        float multiplier = startsAt1 ? 1.0f: 0.0f;
        if (m_effectsDictionary.ContainsKey(effect))
        {
            foreach (var buff in m_effectsDictionary[effect])
            {
                multiplier += buff.Magnitude;
            }
        }
        return multiplier;
    }

    protected float GetEffectMultiplier(EStat buff, EStat debuff, bool clamping = false, float clampMin = 0, float clampMax = 1)
    {
        float multiplier = 1.0f;
        if (m_effectsDictionary.ContainsKey(buff))
        {
            foreach (var buffEffect in m_effectsDictionary[buff])
            {
                multiplier += buffEffect.Magnitude;
            }
        }
        if (m_effectsDictionary.ContainsKey(debuff))
        {
            foreach (var debuffEffect in m_effectsDictionary[debuff])
            {
                multiplier -= debuffEffect.Magnitude;
            }
        }

        if (clamping)
        {
            multiplier = Mathf.Clamp(multiplier, clampMin, clampMax);
        }

        return multiplier;
    }

    public void DisplayText(string value, Color textColor, bool moveOnXAxis = false, float duration = 1.0f)
    {
        var floatingText = Instantiate(m_textDisplayPrefab, transform.position, Quaternion.identity).GetComponent<FloatingText>();
        floatingText.Init(value, textColor, moveOnXAxis, duration);
    }

    public void Damage(float damageAmount)
    {
        if (m_health.RemoveResource(GetCalculatedDamage(damageAmount)))
        {
            Die();
        }
        DisplayText(damageAmount.ToString("0"), m_damageTextColor, true);
    }

    public virtual void Die(bool wasKilled = true)
    {
        OnDeathCallback?.Invoke();
        if (wasKilled)
        {
            GetKilled();
        }
        Destroy(gameObject);
    }

    protected virtual void GetKilled()
    {

    }

    protected float GetCalculatedDamage(float damageAmount)
    {
        var calculatedDamage = damageAmount * Mathf.Max(1.0f, GetEffectMultiplier(EStat.DefenseDebuff, false));
        calculatedDamage /= Mathf.Max(1.0f, GetEffectMultiplier(EStat.DefenseBuff, false));

        return calculatedDamage;
    }
}
