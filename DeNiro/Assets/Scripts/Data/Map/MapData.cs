using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MapData : ScriptableObject
{
    public int XSize;
    public int YSize;
    public string MapName;
    public List<TileDataTuple> Tiles = new List<TileDataTuple>();
    private static string FILE_PATH = "Assets/Resources/Data/Map/Maps/";

    public static void CreateOrOverrideFile(string mapName, List<TileDataTuple> tiles, int xSize, int ySize)
    {
#if UNITY_EDITOR

        var file = Resources.Load(FILE_PATH + mapName);
        if (file != null)
        {
            var MapData = (MapData)file;
            MapData.XSize = xSize;
            MapData.YSize = ySize;
            MapData.Tiles = tiles;
        }
        else
        {
            var MapData = CreateInstance<MapData>();

            MapData.MapName = mapName;
            MapData.XSize = xSize;
            MapData.YSize = ySize;
            MapData.Tiles = tiles;

            UnityEditor.AssetDatabase.CreateAsset(MapData, FILE_PATH + MapData.MapName + ".asset");
        }
        UnityEditor.AssetDatabase.SaveAssets();
        UnityEditor.AssetDatabase.Refresh();
#endif
    }
}

[System.Serializable]
public struct RowData
{
    public List<TileType> Tiles;
}

[System.Serializable]
public enum EDirection
{
    Up,
    Right,
    Down,
    Left,
    Count
}

[System.Serializable]
public struct TileDataTuple
{
    public TileType TileType;
    public EDirection Direction;
}