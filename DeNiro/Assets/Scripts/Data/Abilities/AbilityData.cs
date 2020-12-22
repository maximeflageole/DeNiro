using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AbilityData", menuName = "Abilities/Ability Data")]
public class AbilityData : ScriptableObject
{
    public string Name;
    public string Description;
    public ECreatureType CreatureType;
    
    public List<EffectData> Effects = new List<EffectData>();
}