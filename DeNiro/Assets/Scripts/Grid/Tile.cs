using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField]
    private Transform m_towerAnchor;
    [SerializeField]
    private MeshRenderer m_meshRenderer;
    [SerializeField]
    public Vector2Int Coordinates;
    public EDirection Orientation { get; protected set; }

    public bool IsOccupied { get; set; }

    public Transform GetTowerAnchor() { return m_towerAnchor; }

    public void SetEnabled(bool enable)
    {
        m_meshRenderer.enabled = enable;
    }

    public void Init(TileDataTuple tileData, Vector2Int coordinates)
    {
        Orientation = tileData.Direction;
        Coordinates = coordinates;
    }
}