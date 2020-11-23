using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField]
    private Transform m_towerAnchor;
    [SerializeField]
    private MeshRenderer m_meshRenderer;
    public bool IsOccupied { get; set; }

    public Transform GetTowerAnchor() { return m_towerAnchor; }

    public void SetEnabled(bool enable)
    {
        m_meshRenderer.enabled = enable;
    }
}