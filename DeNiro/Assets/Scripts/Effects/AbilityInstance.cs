using System;
using System.Collections.Generic;
using UnityEngine;

public class AbilityInstance : MonoBehaviour
{
    [SerializeField]
    protected GameObject m_aoeTriggerPrefab;
    [SerializeField]
    protected GameObject m_projectileTriggerPrefab;
    
    protected List<AoeEffectTrigger> m_effectTriggers = new List<AoeEffectTrigger>();
    protected List<AttackEffectTrigger> m_attackTrigger = new List<AttackEffectTrigger>();

    public void Init(AbilityData data, Action<AttackData, AttackEffectTrigger> attackInvoke)
    {
        foreach (var effect in data.Effects)
        {
            if (effect is StatEffectData)
            {
                var effectTrigger = Instantiate(m_aoeTriggerPrefab, transform).GetComponent<AoeEffectTrigger>();
                effectTrigger.Init(effect);
                m_effectTriggers.Add(effectTrigger);      
            }
            else
            {
                var attackTrigger = Instantiate(m_projectileTriggerPrefab, transform).GetComponent<AttackEffectTrigger>();
        
                if (attackTrigger != null)
                {
                    attackTrigger.Init(effect);
                    m_attackTrigger.Add(attackTrigger);
                    attackTrigger.AttackInvoke += attackInvoke;   
                }   
            }
        }  
    }
    
    public void SetEnabled(bool enabled)
    {
        foreach (var effect in m_effectTriggers)
        {
            effect.SetEnabled(enabled);
        }

        foreach (var attack in m_attackTrigger)
        {
            attack.SetEnabled(enabled);
        }
    }

    public void SetSelected(bool selected)
    {
        foreach (var effect in m_effectTriggers)
        {
            effect.DisplayRadius(selected);
        }
        foreach (var effect in m_attackTrigger)
        {
            effect.DisplayRadius(selected);
        }
    }
}
