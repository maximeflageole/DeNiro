using System.Collections.Generic;
using UnityEngine;

public class CreaturesInventory : MonoBehaviour
{
    private List<TowerUiButton> m_towerButtons = new List<TowerUiButton>();

    [SerializeField]
    private List<CreatureData> m_defaultButtons;
    [SerializeField]
    private GameObject m_towerBtnPrefab;

    private void Start()
    {
        m_towerButtons.Clear();
        foreach (var data in m_defaultButtons)
        {
            AddTowerToInventory(data);
        }
    }

    public void AddTowerToInventory(CreatureData data)
    {
        var towerBtn = Instantiate(m_towerBtnPrefab, transform).GetComponent<TowerUiButton>();
        towerBtn.Init(data);
        m_towerButtons.Add(towerBtn);
        towerBtn.m_onClickCallback += OnTowerButtonSelected;
    }

    public void OnTowerButtonSelected(TowerUiButton button)
    {
        PlayerControls.Instance.StartPlacingTower(button);
    }

    public void ConstructTower(TowerUiButton button)
    {
        m_towerButtons.Remove(button);
        Destroy(button.gameObject);
    }
}
