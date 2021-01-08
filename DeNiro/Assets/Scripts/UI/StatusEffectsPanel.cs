using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StatusEffectsPanel : MonoBehaviour
{
    [SerializeField] private List<StatusEffectUI> m_effectUis;
    private Dictionary<EStat, StatusEffectUI> m_statEffectDictionnary = new Dictionary<EStat, StatusEffectUI>();
    
    public void UpdatePanel(StatEffectData data, int amount)
    {
        if (m_statEffectDictionnary.ContainsKey(data.EffectType))
        {
            m_statEffectDictionnary[data.EffectType].SetStackAmount(amount);
            
            if (m_statEffectDictionnary[data.EffectType].StackAmount <= 0)
            {
                m_statEffectDictionnary.Remove(data.EffectType);
            }
        }
        else
        {
            foreach (var effectUi in m_effectUis)
            {
                if (!effectUi.gameObject.activeSelf)
                {
                    effectUi.Init(data.EffectSprite, amount);
                    m_statEffectDictionnary.Add(data.EffectType, effectUi);
                    break;
                }
            }
        }
    }
}
