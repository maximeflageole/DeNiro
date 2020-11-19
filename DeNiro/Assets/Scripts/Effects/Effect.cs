using System;

[Serializable]
public abstract class Effect
{
    public static uint NextId = 0;
    public ETarget Target = ETarget.Enemies;
    public float Radius = 100.0f;
}

[Serializable]
public class StatEffect: Effect
{
    public EStat EffectType = EStat.Damage;
    public float Magnitude = 1.0f;
    public float Duration = 0.0f;
}

[Serializable]
public class AttackEffect : Effect
{
    public float RateOfFire = 1.0f; //Rate of fire is how long in seconds it takes to launch a projectile
}

[Serializable]
public class ProjectileAttackEffect : AttackEffect
{
    public ProjectileData ProjectileData;
}

[Serializable]
public class InstantAttackEffect : AttackEffect
{
    public InstantAttackData InstantAttackData;
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