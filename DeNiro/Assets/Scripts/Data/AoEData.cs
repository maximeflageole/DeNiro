using UnityEngine;

[CreateAssetMenu(fileName = "AoeData", menuName = "Aoe Data")]
public class AoEData : ScriptableObject
{
    public float Radius = 30.0f;
    public float EffectDuration = 1.0f;
    public float DamageTimeStart = 0.0f;
    public float DamageTimeStop = 0.0f;
    public uint Damage = 20;
    public GameObject PrefabSpawned;
}
