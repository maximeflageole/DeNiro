using UnityEngine;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{
    [SerializeField] private BaseMenu m_menu;
    [SerializeField] private Button m_button;

    void Awake()
    {
        m_button.onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        m_menu.ToggleMenu();
    }
}