using System.Collections.Generic;
using UnityEngine;

public class TdUnit : MonoBehaviour
{
    public CreatureData m_creatureData { get; protected set; }
    protected Dictionary<EEffect, List<Effect>> m_effectsDictionary = new Dictionary<EEffect, List<Effect>>();


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

    protected float GetEffectMultiplier(EEffect effect, bool startsAt1 = true)
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

    protected float GetEffectMultiplier(EEffect buff, EEffect debuff, bool clamping = false, float clampMin = 0, float clampMax = 1)
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

    public void OnClick()
    {

    }
}
