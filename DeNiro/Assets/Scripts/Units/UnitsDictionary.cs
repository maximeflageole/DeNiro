using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UnitsDictionary", menuName = "Units Dictionary")]
public class UnitsDictionary : ScriptableObject
{
    public List<UnitTuple> m_unitsList = new List<UnitTuple>();
    public Dictionary<EUnit, string> m_unitsDictionary = new Dictionary<EUnit, string>();

    void Awake()
    {
        OnValidate();
    }

    public void OnValidate()
    {
        m_unitsDictionary.Clear();
        foreach (var tuple in m_unitsList)
        {
            m_unitsDictionary.Add(tuple.id, tuple.assetName);
        }
    }
}

[System.Serializable]
public enum EUnit
{
    Banana,
    Tomato,
    Pine,
    Cereal
}

[System.Serializable]
public struct UnitTuple
{
    public EUnit id;
    public string assetName;
}