
public class StatEffectData: EffectData
{
    public EStat EffectType = EStat.Damage;
    public float Magnitude = 1.0f; //Ex: 1.6 means you either multiply (if a buff) or divide (if a debuff) by 1.6
    public float Duration = 0.0f; //0 means as long as the unit is in range.
}