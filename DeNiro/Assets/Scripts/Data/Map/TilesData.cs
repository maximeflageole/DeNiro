using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TilesData", menuName = "Tiles Data")]
public class TilesData : ScriptableObject
{
    public List<TileTuple> TilesList = new List<TileTuple>();
}

public enum TileType
{
    Spawn,
    Road,
    Ground,
    Obstacle,
    Pyre,
    Count
}

[System.Serializable]
public struct TileTuple
{
    public TileType Type;
    public Color Color;
}
