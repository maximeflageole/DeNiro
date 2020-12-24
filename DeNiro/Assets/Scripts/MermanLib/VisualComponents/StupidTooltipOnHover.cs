using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StupidTooltipOnHover : TooltipOnHover
{
    [SerializeField] private GameObject m_stupidButton;
    
    protected override void DisplayTooltip(bool display = true)
    {
        base.DisplayTooltip(display);
        m_stupidButton.SetActive(!display);
    }
}
