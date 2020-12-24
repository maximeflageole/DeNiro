using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipOnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    protected bool m_mouseOver;
    [SerializeField] protected GameObject m_tooltip;
    [SerializeField] protected float m_timeBeforeTooltip = 0.5f;
    protected float m_currentTimer;

    void Update()
    {
        if (m_mouseOver)
        {
            m_currentTimer += Time.deltaTime;
            if (m_currentTimer > m_timeBeforeTooltip)
            {
                DisplayTooltip();
            }
        }
    }
 
    public void OnPointerEnter(PointerEventData eventData)
    {
        m_mouseOver = true;
    }
 
    public void OnPointerExit(PointerEventData eventData)
    {
        m_currentTimer = 0;
        m_mouseOver = false;
        DisplayTooltip(false);
    }

    protected virtual void DisplayTooltip(bool display = true)
    {
        m_tooltip.SetActive(display);
    }
}
