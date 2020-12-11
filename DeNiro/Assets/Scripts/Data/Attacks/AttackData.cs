using UnityEngine;

public abstract class AttackData : ScriptableObject
{
    public uint Damage = 10;
    public ECreatureType DamageType = ECreatureType.None;
}