using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TowerStats", menuName = "Tower Stats")]
public class TowerStats : UnitStats
{
    public Dictionary<EStat, float> CurrentStatDictionary;
    [SerializeField]
    public List<StatValueTuplet> StatPerLevelList = new List<StatValueTuplet>();
    public Dictionary<EStat, float> StatPerLevelDictionary;

    public void LevelUp()
    {
        foreach (var stat in StatPerLevelDictionary)
        {
            CurrentStatDictionary[stat.Key] += stat.Value;
        }
    }

    public void Init(uint currentLevel)
    {
        if (CurrentStatDictionary != null)
        {
            return;
        }

        CurrentStatDictionary = new Dictionary<EStat, float>();
        StatPerLevelDictionary = new Dictionary<EStat, float>();
        foreach (var tuplet in StatPerLevelList)
        {
            var value = tuplet.value * currentLevel;
            CurrentStatDictionary.Add(tuplet.stat, value);
            StatPerLevelDictionary.Add(tuplet.stat, tuplet.value);
        }
    }

    public float GetStat(EStat stat, float divider = 1.0f)
    {
        if (CurrentStatDictionary.ContainsKey(stat))
        {
            return CurrentStatDictionary[stat]/ divider;
        }
        return 0.0f;
    }
}