using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    [SerializeField]
    protected List<TilesMaterials> m_tilesMaterials = new List<TilesMaterials>();
    [SerializeField]
    protected MapData m_mapData;
    [SerializeField]
    protected GameObject m_tilePrefab;
    [SerializeField]
    protected float m_tileSize;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < m_mapData.XSize; i++)
        {
            for (int j = 0; j < m_mapData.YSize; j++)
            {
                var tilePosition = new Vector3(i * m_tileSize, 0, j * m_tileSize) + transform.position;
                var meshRenderer = Instantiate(m_tilePrefab, tilePosition, Quaternion.identity, transform).GetComponentInChildren<MeshRenderer>();
                foreach (var tile in m_tilesMaterials)
                {
                    if (tile.TileType == m_mapData.Tiles[i * m_mapData.YSize + j])
                    {
                        meshRenderer.material = tile.Material;
                    }
                }
            }
        }
    }
}

[System.Serializable]
public struct TilesMaterials
{
    public TileType TileType;
    public Material Material;
}