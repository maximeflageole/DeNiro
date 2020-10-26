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
    [SerializeField]
    private UnitPanel m_unitPanel;

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
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        LayerMask unitsMask = LayerMask.GetMask("Units");
        LayerMask gameplayMask = LayerMask.GetMask("Gameplay");
        LayerMask tilesMask = LayerMask.GetMask("Tiles");

        var ignoreMasks = unitsMask + gameplayMask;

        if (m_towerInPlacement != null)
        {
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, ~ignoreMasks))
            {
                var tile = hit.transform.GetComponent<Tile>();
                if (tile != null)
                {
                    m_towerInPlacement.transform.position = tile.GetTowerAnchor().position;

                    if (Input.GetMouseButtonDown(0))
                    {
                        PlaceTower(tile);
                    }
                    return;
                }
            }
        }

        ignoreMasks = tilesMask + gameplayMask;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, ~ignoreMasks))
        {
            var unit = hit.transform.GetComponent<TdUnit>();
            if (unit != null)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    OnUnitSelected(unit);
                }
            }
            else
            {
                m_unitPanel.EnablePanel(false);
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                m_unitPanel.EnablePanel(false);
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

    public void OnUnitSelected(TdUnit unit)
    {
        m_unitPanel.AssignData(unit.m_creatureData);
    }
}