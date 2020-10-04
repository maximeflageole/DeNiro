using UnityEngine;
using UnityEngine.UIElements;

public class PlayerControls : MonoBehaviour
{
    public static PlayerControls Instance;

    [SerializeField]
    private TowerInPlacement m_towerInPlacement;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void StartPlacingTower(TowerInPlacement towerInPlacement)
    {
        if (m_towerInPlacement != null)
        {
            StopPlacingTower();
        }
        m_towerInPlacement = towerInPlacement;
    }

    public void StopPlacingTower(bool destroy = true)
    {
        if (m_towerInPlacement != null)
        {
            if (destroy)
            {
                Destroy(m_towerInPlacement.gameObject);
            }
            m_towerInPlacement = null;
        }
    }

    void Update()
    {
        if (m_towerInPlacement != null)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            LayerMask mask = LayerMask.GetMask("Units");

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, ~mask))
            {
                var tile = hit.transform.GetComponent<Tile>();
                if (tile != null)
                {
                    m_towerInPlacement.transform.position = tile.GetTowerAnchor().position;

                    if (Input.GetMouseButtonDown(0))
                    {
                        PlaceTower(tile);
                    }
                }
            }
        }
    }

    private void PlaceTower(Tile tile)
    {
        if (tile.IsOccupied)
        {
            return;
        }
        m_towerInPlacement.PlaceTower(tile.GetTowerAnchor());
        tile.IsOccupied = true;
        StopPlacingTower();
    }
}