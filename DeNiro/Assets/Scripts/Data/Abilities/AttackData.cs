using UnityEngine;

public abstract class AttackData : EffectData
{
    public uint Damage = 10;
    public ECreatureType DamageType = ECreatureType.None;
    public float RateOfFire = 1;
}