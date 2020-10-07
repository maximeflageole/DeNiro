using UnityEngine;

[CreateAssetMenu(fileName = "AoeData", menuName = "Aoe Data")]
public class AoEData : ScriptableObject
{
    public float Radius;
    public float EffectDuration;
    public uint Damage;
    public GameObject PrefabSpawned;
}
