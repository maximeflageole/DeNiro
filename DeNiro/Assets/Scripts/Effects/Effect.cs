using System;

[Serializable]
public class Effect
{
    public static uint NextId = 0;
    public EStat EffectType = EStat.Damage;
    public ETarget Target = ETarget.Enemies;
    public float Magnitude = 1.0f;
    public float Duration = 0.0f;
    public float CurrentDuration = 0.0f;
    public float Radius = 100.0f;
}

[Serializable]
public enum EStat
{
    Damage,
    Haste,
    HasteDebuff,
    MovementSpeed, //TODO
    MovementSpeedDebuff,
    Attack, //TODO
    AttackDebuff,
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