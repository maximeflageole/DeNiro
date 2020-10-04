using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField]
    private Transform m_towerAnchor;

    public Transform GetTowerAnchor() { return m_towerAnchor; }

}
