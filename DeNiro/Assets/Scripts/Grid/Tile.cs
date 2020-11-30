using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField]
    protected Material m_oddMaterial;
    [SerializeField]
    protected Material m_evenMaterial;
    [SerializeField]
    protected bool m_canHaveTower = false;
    [SerializeField]
    protected Transform m_towerAnchor;
    [SerializeField]
    protected MeshRenderer m_visualMeshRenderer;
    [SerializeField]
    private MeshRenderer m_gameplayMeshRenderer;
    [SerializeField]
    public Vector2Int Coordinates;
    public EDirection Orientation { get; protected set; }

    public bool IsOccupied { get; set; }

    public bool CanHaveTower() { return m_canHaveTower; }

    public Transform GetTowerAnchor() { return m_towerAnchor; }

    public void SetEnabled(bool enable)
    {
        m_gameplayMeshRenderer.enabled = enable;
    }

    public void Init(TileDataTuple tileData, Vector2Int coordinates)
    {
        Orientation = tileData.Direction;
        Coordinates = coordinates;
        if (m_evenMaterial != null && m_oddMaterial != null)
        {
            var sum = coordinates.x + coordinates.y;
            if (sum % 2 == 1)
            {
                m_visualMeshRenderer.material = m_evenMaterial;
            }
            else
            {
                m_visualMeshRenderer.material = m_oddMaterial;
            }
        }
    }
}