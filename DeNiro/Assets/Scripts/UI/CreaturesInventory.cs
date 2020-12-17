using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreaturesInventory : MonoBehaviour
{
    [SerializeField]
    private List<CreatureData> m_defaultButtons;
    [SerializeField]
    private GameObject m_towerBtnPrefab;

    [SerializeField] private List<Image> m_unitSlots = new List<Image>();
    private Dictionary<int, TowerUiButton> m_unitSlotsDict = new Dictionary<int, TowerUiButton>();

    private void Start()
    {
        m_unitSlotsDict.Clear();
        
        foreach (var slot in m_unitSlots)
        {
            m_unitSlotsDict.Add(m_unitSlotsDict.Count, null);
        }
        
        foreach (var data in m_defaultButtons)
        {
            AddTowerToInventory(data);
        }
    }
    
    public void AddTowerToInventory(CreatureData data)
    {
        if (IsDictionaryFull())
        {
            return;
        }
        
        var tower = Instantiate(data.TowerData.Prefab).GetComponent<Tower>();
        
        var towerBtn = Instantiate(m_towerBtnPrefab, m_unitSlots[GetNextDictionaryOpenKey()].transform).GetComponent<TowerUiButton>();
        towerBtn.transform.SetAsFirstSibling();
        towerBtn.Init(tower);
        m_unitSlotsDict[GetNextDictionaryOpenKey()] = towerBtn;
        towerBtn.m_onClickCallback += OnTowerButtonSelected;
    }
    
    public void ReturnTowerToInventory(Tower tower, bool activateBtn)
    {
        foreach (var btn in m_unitSlotsDict.Values)
        {
            if (btn.Tower == tower)
            {
                if (activateBtn)
                    btn.SetAvailable();
                tower.OnTowerReturnToInventory();
                return;
            }
        }
        Debug.LogError("ReturnTowerToInventory for a tower which was not in the CreaturesInventory");
    }

    public void OnTowerButtonSelected(int buttonIndex)
    {
        if (!m_unitSlotsDict.ContainsKey(buttonIndex) || m_unitSlotsDict[buttonIndex] == null)
        {
            return;
        }
        
        if (m_unitSlotsDict[buttonIndex].IsAvailable)
        {
            OnTowerButtonSelected(m_unitSlotsDict[buttonIndex]);
        }
        else
        {
            PlayerControls.Instance.OnTowerSelectedToggle(m_unitSlotsDict[buttonIndex].Tower);
        }
    }
    
    public void OnTowerButtonSelected(TowerUiButton button)
    {
        PlayerControls.Instance.StartPlacingTower(button);
    }

    public void ConstructTower(TowerUiButton button)
    {
        button.SetAvailable(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            OnTowerButtonSelected(0);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            OnTowerButtonSelected(1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            OnTowerButtonSelected(2);
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            OnTowerButtonSelected(3);
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            OnTowerButtonSelected(4);
        }

        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            OnTowerButtonSelected(5);
        }
    }

    private int GetNextDictionaryOpenKey()
    {
        foreach (var tuple in m_unitSlotsDict)
        {
            if (tuple.Value == null)
            {
                return tuple.Key;
            }
        }
        return -1;
    }

    private bool IsDictionaryFull()
    {
        foreach (var tuple in m_unitSlotsDict)
        {
            if (tuple.Value == null)
                return false;
        }

        return true;
    }
}
