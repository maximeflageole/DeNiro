using UnityEngine;
using UnityEngine.UI;

public class BaseMenu : MonoBehaviour
{
    [SerializeField] protected Button m_exitButton;
    [SerializeField] protected FadeInFadeOut m_fadingComp;

    protected void Awake()
    {
        m_exitButton.onClick.AddListener(ExitMenu);
    }

    protected void ExitMenu()
    {
        if (m_fadingComp != null)
        {
            m_fadingComp.Activate(false);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    protected void OpenMenu()
    {
        if (m_fadingComp != null)
        {
            m_fadingComp.Activate(true);
        }
        else
        {
            gameObject.SetActive(true);
        }    }
}
