using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CreatureTypesChart
{
    public Dictionary<ECreatureType, List<ECreatureType>> TypeAdvantageDictionary = new Dictionary<ECreatureType, List<ECreatureType>>();
    public Dictionary<ECreatureType, List<ECreatureType>> TypeDisdvantageDictionary = new Dictionary<ECreatureType, List<ECreatureType>>();

    [SerializeField]
    private List<CreatureTypesTuple> TypeChart = new List<CreatureTypesTuple>();

    public List<ECreatureType> GetTypeAdvantages(ECreatureType originalType) { return TypeAdvantageDictionary[originalType]; }
    public List<ECreatureType> GetTypeDisadvantages(ECreatureType originalType) { return TypeDisdvantageDictionary[originalType]; }


    public void OnAwake()
    {
        foreach (var creatureTuple in TypeChart)
        {
            TypeAdvantageDictionary.Add(creatureTuple.Type, creatureTuple.TypeChart.TypeAdvantages);
            TypeAdvantageDictionary.Add(creatureTuple.Type, creatureTuple.TypeChart.TypeDisadvantages);
        }
    }
}

[System.Serializable]
public struct CreatureTypesTuple
{
    public ECreatureType Type;
    public TypeChart TypeChart;
}

[System.Serializable]
public struct TypeChart
{
    public List<ECreatureType> TypeAdvantages;
    public List<ECreatureType> TypeDisadvantages;
}