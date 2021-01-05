using TMPro;
using UnityEngine;

public class AbilityPanel : MonoBehaviour
{
    private AbilityData m_abilityData;

    [SerializeField] private int m_abilityIndex = 0;
    [SerializeField] private TextMeshProUGUI m_abilityName;
    [SerializeField] private TypeIcon m_icon;
    [SerializeField] private TextMeshProUGUI m_title;
    [SerializeField] private TextMeshProUGUI m_description;
    
    public void SelectAbilityAtIndex(int index)
    {
        var selected = m_abilityIndex == index;
    }
    
    public void AssignData(AbilityData data)
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

    public void OnClick(int index)
    {
        PlayerControls.Instance.OnAbilitySelected(index);
    }
}
