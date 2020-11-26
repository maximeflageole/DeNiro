using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField]
    protected List<TilesPrefab> m_tilesPrefabs = new List<TilesPrefab>();
    [SerializeField]
    protected MapData m_mapData;
    [SerializeField]
    protected float m_tileSize;
    public static Dictionary<Vector2Int, Tile> TILES_DICTIONARY = new Dictionary<Vector2Int, Tile>();

    // Start is called before the first frame update
    void Start()
    {
        TILES_DICTIONARY.Clear();
        for (int i = 0; i < m_mapData.XSize; i++)
        {
            for (int j = 0; j < m_mapData.YSize; j++)
            {
                var tilePosition = new Vector3(i * m_tileSize, 0, j * m_tileSize) + transform.position;
                foreach (var tile in m_tilesPrefabs)
                {
                    var tileDataTuple = m_mapData.Tiles[i * m_mapData.YSize + j];
                    if (tile.TileType == tileDataTuple.TileType)
                    {
                        var go = Instantiate(tile.Prefab, tilePosition, Quaternion.identity, transform);
                        var tileInstance = go.GetComponentInChildren<Tile>();
                        var coordinates = new Vector2Int(i, j);
                        TILES_DICTIONARY.Add(coordinates, tileInstance);
                        tileInstance.Init(tileDataTuple, coordinates);
                    }
                }
            }
        }
    }

    public static Waypoint GetNextWaypoint(Waypoint originalTile)
    {
        if (typeof(FinalWaypoint) == originalTile.GetType())
        {
            return null;
        }
        var nextTile = TryGetCoordinates(originalTile.Coordinates, originalTile.Orientation);
        var nextWaypoint = (Waypoint)nextTile;
        if (nextWaypoint != null)
        {
            return nextWaypoint;
        }
        Debug.LogError("A waypoint is not pointing towards another waypoint!");
        return null;
    }

    private static Tile TryGetCoordinates(Vector2Int origin, EDirection direction)
    {
        var directionalVector = new Vector2Int();
        switch (direction)
        {
            case EDirection.Up:
                directionalVector = new Vector2Int(-1, 0);
                break;
            case EDirection.Right:
                directionalVector = new Vector2Int(0, 1);
                break;
            case EDirection.Down:
                directionalVector = new Vector2Int(1, 0);
                break;
            case EDirection.Left:
                directionalVector = new Vector2Int(0, -1);
                break; ;
        }

        if (TILES_DICTIONARY.ContainsKey(origin + directionalVector))
        {
            return TILES_DICTIONARY[origin + directionalVector];
        }
        Debug.LogError("A tile has coordinates pointing outside of the map!");
        return null;
    }
}

[System.Serializable]
public struct TilesPrefab
{
    public TileType TileType;
    public GameObject Prefab;
}