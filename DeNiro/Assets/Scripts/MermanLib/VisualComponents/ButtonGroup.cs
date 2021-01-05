using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonGroup : MonoBehaviour
{
    [SerializeField] private List<Button> m_buttons;
    private Button m_currentlySelectedBtn;

    public void ChangeButtonExternal(int index)
    {
        m_buttons[index].Select();
        m_currentlySelectedBtn = m_buttons[index];
    }
    
    private void OnEnable()
    {
        foreach (var btn in m_buttons)
        {
            btn.onClick.AddListener(() => OnButtonSelected(btn));
        }
    }

    private void OnDisable()
    {
        foreach (var btn in m_buttons)
        {
            btn.onClick.RemoveAllListeners();
        }
    }

    private void OnButtonSelected(Button button)
    {
        foreach (var btn in m_buttons)
        {
            if (btn != button)
            {
                btn.OnDeselect(null);
            }
        }

        m_currentlySelectedBtn = button;
    }

    private void Update()
    {
        if (m_currentlySelectedBtn == null)
            return;
        if (EventSystem.current.currentSelectedGameObject == null || EventSystem.current.currentSelectedGameObject != m_currentlySelectedBtn.gameObject)
        {
            m_currentlySelectedBtn.OnSelect(null);
        }
    }
}
