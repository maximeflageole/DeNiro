using UnityEngine;

public class AbilityPanel : MonoBehaviour
{
    private AbilityData m_abilityData;

    public void AssignData(AbilityData data)
    {
        m_abilityData = data;
        Display(data != null);
    }

    public void Display(bool isActive)
    {
        gameObject.SetActive(isActive);
    }
}
