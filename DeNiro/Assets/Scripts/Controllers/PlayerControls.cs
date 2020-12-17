using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerControls : MonoBehaviour
{
    public static PlayerControls Instance;
    
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

        if (m_currentlySelectedButton?.Tower != null)
        {
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, ~ignoreMasks))
            {
                if (m_hoveredTile != null && m_hoveredTile.CanHaveTower())
                {
                    m_currentlySelectedButton.Tower.transform.position = m_hoveredTile.GetTowerAnchor().position;

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

        button.Tower.BeginPlacement(button.Tower.GetCreatureData());
        if (m_currentlySelectedButton?.Tower != null)
        {
            StopPlacingTower(true, false);
        }
        m_currentlySelectedButton = button;
    }

    public void StopPlacingTower(bool returnToInventory = true, bool wasConstructed = true)
    {
        if (m_currentlySelectedButton?.Tower != null)
        {
            if (wasConstructed)
            {
                m_creaturesInventory.ConstructTower(m_currentlySelectedButton);
            }
            if (returnToInventory)
            {
                ReturnTowerToInventory(m_currentlySelectedButton.Tower);
            }
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
        m_currentlySelectedButton.Tower.PlaceTower(tile);
        m_towersInField.Add(m_currentlySelectedButton.Tower);
        tile.IsOccupied = true;
        StopPlacingTower(false);
    }

    public void CollectTower(CreatureData data)
    {
        m_creaturesInventory.AddTowerToInventory(data);
    }

    public void OnTowerSelectedToggle(Tower tower)
    {
        if (m_currentlySelectedTower == tower)
        {
            UnselectTower();
        }
        else
        {
            OnTowerSelected(tower);
        }
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

    public void ReturnTowerToInventory(Tower tower = null)
    {
        bool activateBtn = false;
        if (tower == null)
        {
            tower = m_currentlySelectedTower;
            activateBtn = true;
        }
        
        if (tower.CurrentTile != null)
        {
            tower.CurrentTile.IsOccupied = false;
            tower.CurrentTile = null;   
        }
        
        m_creaturesInventory.ReturnTowerToInventory(tower, activateBtn);
        m_towersInField.Remove(tower);
        UnselectTower();
    }

    public void RefreshUnitUI()
    {
        if (m_currentlySelectedTower != null)
        {
            m_towerPanel.AssignTowerData(m_currentlySelectedTower.GetCreatureData(), m_currentlySelectedTower.GetData().Stats, m_currentlySelectedTower.GetSaveData());
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