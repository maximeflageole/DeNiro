using TMPro;
using UnityEngine.UI;

public class EndLevelMenu : BaseMenu
{
    public const string VICTORY_TXT = "VICTORY!";
    public const string DEFEAT_TXT = "DEFEAT";

    public TextMeshProUGUI m_victoryText;
    public Button m_restartBtn;

    protected override void Awake()
    {
        //Just to override and not exit the menu with the exit btn
    }
    
    private void OnEnable()
    {
        m_exitButton.onClick.AddListener(GameManager.Instance.m_exitAction.Invoke);
        m_restartBtn.onClick.AddListener(GameManager.Instance.m_restartAction.Invoke);
    }

    private void OnDisable()
    {
        m_restartBtn.onClick.RemoveAllListeners();
        m_exitButton.onClick.RemoveAllListeners();
    }

    public void ToggleDisplay(bool victory = false, bool isEnd = false)
    {
        if (GameManager.GAME_OVER) return;
        if (gameObject.activeSelf)
        {
            Hide();
            return;
        }

        gameObject.SetActive(true);

        if (!isEnd)
        {
            m_victoryText.gameObject.SetActive(false);
            return;
        }
        m_victoryText.gameObject.SetActive(true);

        if (victory)
        {
            m_victoryText.text = VICTORY_TXT;
            return;
        }

        m_victoryText.text = DEFEAT_TXT;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        m_victoryText.gameObject.SetActive(false);
    }
}
