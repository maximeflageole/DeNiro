﻿using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnitPanel : MonoBehaviour
{
    [SerializeField]
    protected Image m_unitImage;
    [SerializeField]
    protected TextMeshProUGUI m_unitName;
    [SerializeField]
    protected TextMeshProUGUI m_unitTypes;
    [SerializeField]
    protected AbilityPanel m_ability1Panel;
    [SerializeField]
    protected AbilityPanel m_ability2Panel;
    [SerializeField]
    protected Button m_removeBtn;

    protected CreatureData m_creatureData;

    public void AssignData(CreatureData creatureData, bool isTower = false)
    {

        m_removeBtn.gameObject.SetActive(isTower);
        gameObject.SetActive(creatureData != null);

        m_creatureData = creatureData;

        m_unitImage.sprite = m_creatureData.TowerData.TowerSprite;
        m_unitName.text = m_creatureData.TowerData.name;
        //TODO: Insert types when they are added to the game
        m_ability1Panel.AssignData(null);
        m_ability2Panel.AssignData(null);
    }

    public void EnablePanel(bool isEnabled)
    {
        gameObject.SetActive(isEnabled);
    }

    public void OnClickRemove()
    {
        PlayerControls.Instance.ReturnUnitToInventory();
    }
}
