using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerControls : MonoBehaviour
{
    public static PlayerControls Instance;

    private Tower m_towerInPlacement;

    [SerializeField]
    private CreaturesInventory m_creaturesInventory;
    [SerializeField]
    private TowerUiButton m_currentlySelectedButton;
    [SerializeField]
    private Tower m_currentlySelectedTower;
    [SerializeField]
    private TowerPanel m_towerPanel;
    private Tile m_hoveredTile;
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
        UpdateHoveringAndClicks();
    }

    private void UpdateHoveringAndClicks()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        LayerMask unitsMask = LayerMask.GetMask("Units");
        LayerMask gameplayMask = LayerMask.GetMask("Gameplay");

        var ignoreMasks = unitsMask + gameplayMask;

        if (Input.GetMouseButtonDown(1))
        {
            StopPlacingTower(true, false);
            return;
        }

        if (EventSystem.current.IsPointerOverGameObject(guiInt))    // is the touch on the GUI
        {
            // GUI Action
            return;
        }

        m_hoveredTile?.SetEnabled(false);
        m_hoveredTile = null;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, ~ignoreMasks))
        {
            var tile = hit.transform.GetComponent<Tile>();
            if (tile != null)
            {
                tile.SetEnabled(true);
                m_hoveredTile = tile;
            }
        }

        if (m_towerInPlacement != null)
        {
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, ~ignoreMasks))
            {
                if (m_hoveredTile != null && m_hoveredTile.CanHaveTower())
                {
                    m_towerInPlacement.transform.position = m_hoveredTile.GetTowerAnchor().position;

                    if (Input.GetMouseButtonDown(0))
                    {
                        PlaceTower(m_hoveredTile);
                        UnselectTower();
                        return;
                    }
                }
                else if (Input.GetMouseButtonDown(0))
                {
                    StopPlacingTower(true, false);
                    return;
                }
            }
        }

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, unitsMask))
        {
            var unit = hit.transform.GetComponent<Tower>();
            if (unit != null)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    OnTowerSelected(unit);
                    return;
                }
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            StopPlacingTower(true, false);
            UnselectTower();
        }
    }

#if UNITY_EDITOR
    private void UpdateHacks()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (m_currentlySelectedTower != null)
            {
                m_currentlySelectedTower.GainXp(10);
            }
        }
    }
#endif

    public void StartPlacingTower(TowerUiButton button)
    {
        if (m_currentlySelectedButton == button)
        {
            StopPlacingTower(true, false);
            return;
        }
        var towerInPlacement = Instantiate(button.CreatureData.TowerData.Prefab).GetComponent<Tower>();

        towerInPlacement.BeginPlacement(button.CreatureData);
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
        if (tile.IsOccupied || !tile.CanHaveTower())
        {
            StopPlacingTower(true, false);
            return;
        }
        m_towerInPlacement.PlaceTower(tile);
        m_towersInField.Add(m_towerInPlacement);
        tile.IsOccupied = true;
        StopPlacingTower(false);
    }

    public void CollectTower(CreatureData data)
    {
        m_creaturesInventory.AddTowerToInventory(data);
    }

    private void OnTowerSelected(Tower tower)
    {
        UnselectTower();
        m_currentlySelectedTower = tower;
        tower.OnTowerSelected(true);
        RefreshUnitUI();
    }

    private void UnselectTower()
    {
        m_currentlySelectedTower?.OnTowerSelected(false);
        m_currentlySelectedTower = null;
        m_towerPanel.EnablePanel(false);
    }

    public void ReturnTowerToInventory()
    {
        m_currentlySelectedTower.CurrentTile.IsOccupied = false;
        m_currentlySelectedTower.CurrentTile = null;
        m_creaturesInventory.AddTowerToInventory(m_currentlySelectedTower.m_creatureData);
        m_towersInField.Remove(m_currentlySelectedTower);
        Destroy(m_currentlySelectedTower.gameObject);
        UnselectTower();
    }

    public void RefreshUnitUI()
    {
        if (m_currentlySelectedTower != null)
        {
            m_towerPanel.AssignTowerData(m_currentlySelectedTower.m_creatureData, m_currentlySelectedTower.GetData().Stats, m_currentlySelectedTower.GetSaveData());
            return;
        }
    }

    public void DistributeXp(uint amount)
    {
        uint individualAmount = (uint)Mathf.Ceil((float)amount / m_towersInField.Count);
        foreach (var tower in m_towersInField)
        {
            tower.GainXp(individualAmount);
        }
    }
}