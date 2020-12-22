using System;
using UnityEngine;

public abstract class EffectData: ScriptableObject
{
    public static uint NextId = 0;
    public ETarget TargetType = ETarget.Enemies;
    public float Range = 100.0f;
}

[Serializable]
public enum EStat
{
    Damage,
    Haste,
    HasteDebuff,
    MovementSpeed, //TODO
    MovementSpeedDebuff,
    AttackBuff, //TODO --> Should be changed to Attack Buff, not sure why it is effective right now
    DefenseDebuff,
    Range, //TODO
    RangeDebuff, //TODO
    Presence, //TODO
    PresenceDebuff //TODO
}

[Serializable]
public enum ETarget
{
    Towers,
    Enemies,
    Any,
    None
}

[Serializable]
public struct StatValueTuplet
{
    public EStat stat;
    public float value;
}