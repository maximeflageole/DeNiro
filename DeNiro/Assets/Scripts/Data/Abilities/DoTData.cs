using UnityEngine;

[CreateAssetMenu(fileName = "DotData", menuName = "Effect/Dot Data")]
public class DoTData : StatEffectData
{ 
    //TODO: Custom editor so that changing the ticks amount, duration, Damage per tick or total damage changes the rest
    public int TicksAmount = 4; //Ticks are every quarter of a second
    public float DamagePerTick = 10f;
    public float TotalDamage = 40f;
    public ECreatureType ECreatureType = ECreatureType.None; //None == pure damage
    public bool AffectedByTypesMultipliers;
    public bool InfiniteDuration;
    public int MaxStackAmount = 1;
    public bool StackRefreshesCooldown;
}