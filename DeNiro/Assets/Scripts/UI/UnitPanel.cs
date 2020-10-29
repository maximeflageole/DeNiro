using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnitPanel : MonoBehaviour
{
    public static string LEVEL_BASE_TEXT = "Lvl ";
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

    protected CreatureData m_creatureData;

    public void AssignEnemyData(CreatureData creatureData, UnitStats stats)
    {
        m_removeBtn.gameObject.SetActive(false);
        gameObject.SetActive(creatureData != null);

        m_creatureData = creatureData;

        m_unitImage.sprite = m_creatureData.TowerData.TowerSprite;
        m_unitName.text = m_creatureData.TowerData.name;
        //TODO: Insert types when they are added to the game
        m_ability1Panel.AssignData(null);
        m_ability2Panel.AssignData(null);
    }

    public void AssignTowerData(CreatureData creatureData, TowerStats stats)
    {
        m_removeBtn.gameObject.SetActive(true);
        gameObject.SetActive(creatureData != null);

        m_creatureData = creatureData;

        m_unitImage.sprite = m_creatureData.TowerData.TowerSprite;
        m_unitName.text = m_creatureData.TowerData.name;
        //TODO: Insert types when they are added to the game
        m_ability1Panel.AssignData(null);
        m_ability2Panel.AssignData(null);
        var towerStats = stats;
        m_xpImage.fillAmount = GameManager.Instance.GetNextLevelXpPercentage(towerStats.CurrentXp, towerStats.CurrentLevel);
        m_levelTMPro.text = LEVEL_BASE_TEXT + stats.CurrentLevel;
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
