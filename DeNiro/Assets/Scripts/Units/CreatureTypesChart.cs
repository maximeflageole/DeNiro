using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TypeChart", menuName = "Type Chart")]
public class CreatureTypesChart: ScriptableObject
{
    public Dictionary<ECreatureType, List<ECreatureType>> TypeAdvantageDictionary = new Dictionary<ECreatureType, List<ECreatureType>>();
    public Dictionary<ECreatureType, List<ECreatureType>> TypeDisdvantageDictionary = new Dictionary<ECreatureType, List<ECreatureType>>();

    public List<CreatureTypesTuple> TypeChart = new List<CreatureTypesTuple>();

    public List<ECreatureType> GetTypeAdvantages(ECreatureType originalType) { return TypeAdvantageDictionary[originalType]; }
    public List<ECreatureType> GetTypeDisadvantages(ECreatureType originalType) { return TypeDisdvantageDictionary[originalType]; }
    public List<TypeData> TypesData = new List<TypeData>();


    public void OnAwake()
    {
        TypeAdvantageDictionary.Clear();
        TypeDisdvantageDictionary.Clear();

        foreach (var creatureTuple in TypeChart)
        {
            Debug.Log("Adding type " + creatureTuple.Type + " in dictionary");
            TypeAdvantageDictionary.Add(creatureTuple.Type, creatureTuple.TypeChart.TypeAdvantages);
            TypeDisdvantageDictionary.Add(creatureTuple.Type, creatureTuple.TypeChart.TypeDisadvantages);
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