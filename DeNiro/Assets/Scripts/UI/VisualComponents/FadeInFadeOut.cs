using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FadeInFadeOut : MonoBehaviour
{
    [SerializeField] protected CanvasGroup m_canvasGroup;
    protected float m_maxAlpha = 1;
    protected List<FadeInFadeOut> m_childList = new List<FadeInFadeOut>();
    protected bool m_fadingIn;
    protected bool m_fadingOut;
    [SerializeField]
    protected float m_fadeInDuration = 0.1f;
    [SerializeField]
    protected float m_fadeOutDuration = 0.1f;
    protected float m_fadeCurrentTimer = 0f;

    public void Activate(bool active = true)
    {
        m_fadingIn = active;
        m_fadingOut = !active;

        if (active)
        {
            m_fadeCurrentTimer = 0;
            gameObject.SetActive(active);
        }
        else
        {
            m_fadeCurrentTimer = 0;
        }

        foreach (var fadeComp in m_childList)
        {
            fadeComp.Activate(active);
        }
    }

    private void Update()
    {
        if (!m_fadingIn && !m_fadingOut)
        {
            return;
        }

        m_fadeCurrentTimer += Time.deltaTime;

        if (m_fadingIn)
        {
            m_canvasGroup.alpha = (m_fadeCurrentTimer / m_fadeInDuration) * m_maxAlpha;

            if (m_fadeCurrentTimer > m_fadeInDuration)
            {
                m_fadingIn = false;
                m_fadingOut = false;
            }
        }
        else
        {
            m_canvasGroup.alpha = (1- (m_fadeCurrentTimer / m_fadeInDuration)) * m_maxAlpha;

            if (m_fadeCurrentTimer > m_fadeOutDuration)
            {
                m_fadingOut = false;
                m_fadingIn = false;
                gameObject.SetActive(false);
            }
        }
    }
}
