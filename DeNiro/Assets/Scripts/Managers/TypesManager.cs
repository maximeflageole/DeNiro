using System.Collections.Generic;
using UnityEngine;

public class TypesManager: MonoBehaviour
{
    public CreatureTypesChart m_creaturesTypeChart;
    public List<TypeData> m_typesData = new List<TypeData>();
    public Dictionary<ECreatureType, TypeData> m_typesDataDict = new Dictionary<ECreatureType, TypeData>();
    public float m_typeAdvantageDamageMultiplier;
    public float m_typeDisadvantageDamageMultiplier;

    private void Awake()
    {
        m_creaturesTypeChart.OnAwake();
        m_typesDataDict.Clear();
        foreach (var type in m_typesData)
        {
            m_typesDataDict.Add(type.Type, type);
        }
    }

    public float GetDamageTypeMultiplier(ECreatureType attackerType, ECreatureType targetType)
    {
        if (attackerType == ECreatureType.None || attackerType == ECreatureType.Count)
        {
            return 1.0f;
        }

        if (m_creaturesTypeChart.GetTypeAdvantages(attackerType).Contains(targetType))
        {
            return m_typeAdvantageDamageMultiplier;
        }

        if (m_creaturesTypeChart.GetTypeDisadvantages(attackerType).Contains(targetType))
        {
            return m_typeDisadvantageDamageMultiplier;
        }

        return 1.0f;
    }

    public float GetDamageTypeMultiplier(CreatureData targetData, ECreatureType attackType)
    {
        var returnValue = 1.0f;
        returnValue *= GetDamageTypeMultiplier(attackType, targetData.CreaturePrimaryType);
        returnValue *= GetDamageTypeMultiplier(attackType, targetData.CreatureSecondaryType);
        return returnValue;
    }
}
