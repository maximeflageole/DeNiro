using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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
    private TdUnit m_currentlySelectedUnit;
    [SerializeField]
    private UnitPanel m_unitPanel;
    private List<Tower> m_towersInField = new List<Tower>();
    private static int guiInt = -1;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    #if !UNITY_EDITOR
            guiInt = 0;
    #endif
    }

    void Update()
    {
#if UNITY_EDITOR
        UpdateHacks();
#endif
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        LayerMask unitsMask = LayerMask.GetMask("Units");
        LayerMask gameplayMask = LayerMask.GetMask("Gameplay");

        var ignoreMasks = unitsMask + gameplayMask;

        if (EventSystem.current.IsPointerOverGameObject(guiInt))    // is the touch on the GUI
        {
            // GUI Action
            return;
        }

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

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, unitsMask))
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
                UnselectUnit();
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                UnselectUnit();
            }
        }
    }

#if UNITY_EDITOR
    private void UpdateHacks()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (m_currentlySelectedUnit != null)
            {
                var tower = (Tower)m_currentlySelectedUnit;
                if (tower != null)
                {
                    tower.GiveXP(10);
                }
            }
        }
    }
#endif

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
        m_towersInField.Add(m_towerInPlacement.PlaceTower(tile.GetTowerAnchor()));
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

    private void OnUnitSelected(TdUnit unit)
    {
        m_currentlySelectedUnit = unit;
        RefreshUnitUI();
    }

    private void UnselectUnit()
    {
        m_currentlySelectedUnit = null;
        m_unitPanel.EnablePanel(false);
    }

    public void ReturnTowerToInventory()
    {
        m_creaturesInventory.AddTowerToInventory(m_currentlySelectedUnit.m_creatureData);
        m_towersInField.Remove(m_currentlySelectedUnit.GetComponent<Tower>());
        Destroy(m_currentlySelectedUnit.gameObject);
        UnselectUnit();
    }

    public void RefreshUnitUI()
    {
        if (m_currentlySelectedUnit != null)
        {
            var tower = m_currentlySelectedUnit.GetComponent<Tower>();
            if (tower != null)
            {
                m_unitPanel.AssignTowerData(m_currentlySelectedUnit.m_creatureData, tower.GetData().Stats, tower.GetSaveData());
                return;
            }
            m_unitPanel.AssignEnemyData(m_currentlySelectedUnit.m_creatureData, m_currentlySelectedUnit.m_stats);
        }
    }

    public void DistributeXp(uint amount)
    {
        uint individualAmount = (uint)Mathf.Ceil((float)amount / m_towersInField.Count);
        foreach (var tower in m_towersInField)
        {
            tower.GiveXP(individualAmount);
        }
    }
}