using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FadeInFadeOut : MonoBehaviour
{
    protected float m_maxAlpha = 0;
    protected Image m_rootImage;
    protected TextMeshProUGUI m_rootTmpro;
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
        if (m_rootImage == null && m_rootTmpro == null)
        {
            m_rootImage = GetComponent<Image>();
            m_rootTmpro = GetComponent<TextMeshProUGUI>();
            m_childList = GetComponentsInChildren<FadeInFadeOut>().ToList();
            m_childList.Remove(this);
            if (m_rootImage == null && m_rootTmpro == null)
            {
                Debug.LogError("A FadeInFadeOut component does not have an image or TMPro at it's root");
                return;
            }
            else if (m_rootImage != null)
            {
                m_maxAlpha = m_rootImage.color.a;
            }
            else
            {
                m_maxAlpha = m_rootTmpro.color.a;
            }
        }

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
            if (m_rootImage != null)
            {
                var tempColor = m_rootImage.color;
                tempColor.a = (m_fadeCurrentTimer / m_fadeInDuration) * m_maxAlpha;
                m_rootImage.color = tempColor;
            }
            else
            {
                var tempColor = m_rootTmpro.color;
                tempColor.a = (m_fadeCurrentTimer / m_fadeInDuration) * m_maxAlpha;
                m_rootTmpro.color = tempColor;
            }
            if (m_fadeCurrentTimer > m_fadeInDuration)
            {
                m_fadingIn = false;
                m_fadingOut = false;
            }
        }
        else
        {
            if (m_rootImage != null)
            {
                var tempColor = m_rootImage.color;
                tempColor.a = (1 - (m_fadeCurrentTimer / m_fadeInDuration)) * m_maxAlpha;
                m_rootImage.color = tempColor;
            }
            else
            {
                var tempColor = m_rootTmpro.color;
                tempColor.a = (1- (m_fadeCurrentTimer / m_fadeInDuration)) * m_maxAlpha;
                m_rootTmpro.color = tempColor;
            }

            if (m_fadeCurrentTimer > m_fadeOutDuration)
            {
                m_fadingOut = false;
                m_fadingIn = false;
                gameObject.SetActive(false);
            }
        }
    }
}
