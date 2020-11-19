using UnityEngine;

[CreateAssetMenu(fileName = "ArtilleryData", menuName = "Attacks/Artillery Data")]
public class ArtilleryData : ProjectileData
{
    public AoEData AoEData;
    public AnimationCurve VerticalPositionCurve;
    public float Lifespan = 1.0f;
}
