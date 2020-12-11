using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TowerPanel : MonoBehaviour
{
    public static string LEVEL_BASE_TEXT = "Lvl ";
    [SerializeField]
    protected Image m_towerImage;
    [SerializeField]
    protected TextMeshProUGUI m_towerName;
    [SerializeField]
    protected AbilityPanel m_ability1Panel;
    [SerializeField]
    protected AbilityPanel m_ability2Panel;
    [SerializeField]
    protected Button m_removeBtn;
    [SerializeField]
    protected Image m_xpImage;
    [SerializeField]
    protected TextMeshProUGUI m_levelTMPro;
    [SerializeField]
    protected TextMeshProUGUI m_attackValueTMPro;
    [SerializeField]
    protected TextMeshProUGUI m_hasteValueTMPro;
    [SerializeField]
    protected TextMeshProUGUI m_rangeValueTMPro;
    [SerializeField]
    protected TextMeshProUGUI m_presenceValueTMPro;
    [SerializeField]
    protected TypeIcon m_type1Icon;
    [SerializeField]
    protected TypeIcon m_type2Icon;

    protected CreatureData m_creatureData;

    public void AssignTowerData(CreatureData creatureData, TowerStats stats, TowerSaveData saveData)
    {
        gameObject.SetActive(creatureData != null);

        m_creatureData = creatureData;

        m_towerImage.sprite = m_creatureData.TowerData.TowerSprite;
        m_towerName.text = m_creatureData.TowerData.name;
        //TODO: Insert types when they are added to the game
        m_ability1Panel.AssignData(null);
        m_ability2Panel.AssignData(null);
        m_xpImage.fillAmount = GameManager.Instance.GetNextLevelXpPercentage(saveData.xp, saveData.level);
        m_levelTMPro.text = LEVEL_BASE_TEXT + saveData.level;
        m_attackValueTMPro.text = "+" + stats.GetStat(EStat.AttackBuff).ToString("F0") + "%";
        m_hasteValueTMPro.text = "+" + stats.GetStat(EStat.Haste).ToString("F0") + "%";
        m_rangeValueTMPro.text = "+" + stats.GetStat(EStat.Range).ToString("F0") + "%";
        m_presenceValueTMPro.text = "+" + stats.GetStat(EStat.Presence).ToString("F0") + "%";
        m_type1Icon.AssignType(creatureData.CreaturePrimaryType);
        m_type2Icon.AssignType(creatureData.CreatureSecondaryType);
    }

    public void EnablePanel(bool isEnabled)
    {
        gameObject.SetActive(isEnabled);
    }

    public void OnClickRemove()
    {
        PlayerControls.Instance.ReturnTowerToInventory();
    }
}
