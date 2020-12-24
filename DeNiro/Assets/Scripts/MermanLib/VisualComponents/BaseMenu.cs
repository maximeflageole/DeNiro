using UnityEngine;
using UnityEngine.UI;

public class BaseMenu : MonoBehaviour
{
    [SerializeField] protected Button m_exitButton;
    [SerializeField] protected FadingComponent m_fadingComp;
    protected bool m_isOpen;

    protected virtual void Awake()
    {
        m_exitButton.onClick.AddListener(delegate{OpenMenu(false);});
    }

    public void OpenMenu(bool open = true)
    {
        if (m_fadingComp != null)
        {
            m_fadingComp.Activate(open);
        }
        else
        {
            gameObject.SetActive(open);
        }

        m_isOpen = open;
    }

    public void ToggleMenu()
    {
        OpenMenu(!m_isOpen);
    }
}
