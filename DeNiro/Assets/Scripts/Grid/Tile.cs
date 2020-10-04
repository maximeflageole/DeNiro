using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField]
    private Transform m_towerAnchor;
    public bool IsOccupied { get; set; }

    public Transform GetTowerAnchor() { return m_towerAnchor; }

}