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
        var towerBtn = Instantiate(m_towerBtnPrefab, m_unitSlots[GetNextDictionaryOpenKey()].transform).GetComponent<TowerUiButton>();
        towerBtn.transform.SetAsFirstSibling();
        towerBtn.Init(data);
        m_unitSlotsDict[GetNextDictionaryOpenKey()] = towerBtn;
        towerBtn.m_onClickCallback += OnTowerButtonSelected;
    }

    public void OnTowerButtonSelected(int buttonIndex)
    {
        if (m_unitSlotsDict.ContainsKey(buttonIndex) && m_unitSlotsDict[buttonIndex] != null)
        {
            OnTowerButtonSelected(m_unitSlotsDict[buttonIndex]);
        }
    }
    
    public void OnTowerButtonSelected(TowerUiButton button)
    {
        PlayerControls.Instance.StartPlacingTower(button);
    }

    public void ConstructTower(TowerUiButton button)
    {
        for (var i = 0; i < m_unitSlots.Count; i++)
        {
            if (m_unitSlotsDict[i] == button)
            {
                m_unitSlotsDict[i] = null;
                break;
            }
        }
        Destroy(button.gameObject);
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
