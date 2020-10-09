using System;

[Serializable]
public class Effect
{
    public static uint NextId = 0;
    public EEffect EffectType = EEffect.Damage;
    public ETarget Target = ETarget.Enemies;
    public float Magnitude = 1.0f;
    public float Duration = 0.0f;
    public float CurrentDuration = 0.0f;
    public float Radius = 100.0f;
}

[Serializable]
public enum EEffect
{
    Damage,
    AttackSpeedBuff,
    AttackSpeedDebuff
}

[Serializable]
public enum ETarget
{
    Towers,
    Enemies,
    Any,
    None
}