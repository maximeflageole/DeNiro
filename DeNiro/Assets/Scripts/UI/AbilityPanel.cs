using TMPro;
using UnityEngine;

public class AbilityPanel : MonoBehaviour
{
    private AbilityData m_abilityData;
    [SerializeField] private TextMeshProUGUI m_abilityName;
    [SerializeField] private TypeIcon m_icon;
    [SerializeField] protected TextMeshProUGUI m_title;
    [SerializeField] protected TextMeshProUGUI m_description;

    public void AssignData(AbilityData data, bool isSelected)
    {
        m_abilityData = data;
        Display(data != null);
        if (data != null)
        {
            m_icon.AssignType(m_abilityData.CreatureType, false);
            m_abilityName.text = m_abilityData.Name;
            m_title.text = m_abilityData.Name;
            m_description.text = m_abilityData.Description;
        }
    }

    public void Display(bool isActive)
    {
        gameObject.SetActive(isActive);
    }
}
