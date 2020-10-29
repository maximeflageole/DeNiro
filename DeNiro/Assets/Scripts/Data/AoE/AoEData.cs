using UnityEngine;

[CreateAssetMenu(fileName = "AoeData", menuName = "Aoe Data")]
public class AoEData : ScriptableObject
{
    public ETarget Target = ETarget.Enemies;
    public EStat Effect = EStat.Damage;
    public bool IsPermanent = false;
    public bool Ticking = false;
    public float Radius = 30.0f;
    public float EffectDuration = 1.0f;
    public float EffectTimeStart = 0.0f;
    public float EffectTimeStop = 0.0f;
    public float Magnitude = 20.0f;
    public GameObject PrefabSpawned;
}
