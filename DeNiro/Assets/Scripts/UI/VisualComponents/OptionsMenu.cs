using UnityEngine.UI;

public class OptionsMenu : BaseMenu
{
    public Button m_restartBtn;
    
    private void OnEnable()
    {
        m_restartBtn.onClick.AddListener(GameManager.Instance.m_restartAction.Invoke);
    }

    private void OnDisable()
    {
        m_restartBtn.onClick.RemoveAllListeners();
    }
}
