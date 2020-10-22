using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public static PlayerControls Instance;

    private TowerInPlacement m_towerInPlacement;

    [SerializeField]
    private GameObject m_towerInPlacementPrefab;
    [SerializeField]
    private CreaturesInventory m_creaturesInventory;
    [SerializeField]
    private TowerUiButton m_currentlySelectedButton;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
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

    public void StartPlacingTower(TowerUiButton button)
    {
        var towerInPlacement = Instantiate(m_towerInPlacementPrefab).GetComponent<TowerInPlacement>();
        towerInPlacement.Init(button.CreatureData);
        if (m_towerInPlacement != null)
        {
            StopPlacingTower(true, false);
        }
        m_currentlySelectedButton = button;
        m_towerInPlacement = towerInPlacement;
    }

    public void StopPlacingTower(bool destroy = true, bool wasConstructed = true)
    {
        if (m_towerInPlacement != null)
        {
            if (wasConstructed)
            {
                m_creaturesInventory.ConstructTower(m_currentlySelectedButton);
            }
            if (destroy)
            {
                Destroy(m_towerInPlacement.gameObject);
            }
            m_towerInPlacement = null;
            m_currentlySelectedButton = null;
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

    public static void EndGame(bool victory)
    {
        Debug.LogWarning("Waves defeated! Congrats");
    }

    public void CollectTower(CreatureData data)
    {
        m_creaturesInventory.AddTowerToInventory(data);
    }
}