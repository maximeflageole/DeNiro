using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MapData : ScriptableObject
{
    public int XSize;
    public int YSize;
    public string MapName;
    public List<TileDataTuple> Tiles = new List<TileDataTuple>();
    private static string FILE_PATH = "Data/Map/Maps/";
    private static string RESOURCES_PATH = "Assets/Resources/";

    public static bool CreateOrOverrideFile(string mapName, List<TileDataTuple> tiles, int xSize, int ySize)
    {
#if UNITY_EDITOR

        var hasFileBeenCreated = false;
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

            UnityEditor.AssetDatabase.CreateAsset(MapData, RESOURCES_PATH + FILE_PATH + MapData.MapName + ".asset");
            hasFileBeenCreated = true;
        }
        UnityEditor.AssetDatabase.SaveAssets();
        UnityEditor.AssetDatabase.Refresh();
        return hasFileBeenCreated;
#endif
    }

    public TileDataTuple GetTileDataAtPos(int pos)
    {
        return Tiles[pos];
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