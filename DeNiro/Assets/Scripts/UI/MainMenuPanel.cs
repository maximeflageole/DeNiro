using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuPanel : MonoBehaviour
{
    public const string VICTORY_TXT = "VICTORY!";
    public const string DEFEAT_TXT = "DEFEAT";

    public TextMeshProUGUI m_victoryText;
    public Button m_restartBtn;
    public Button m_exitBtn;

    private void OnEnable()
    {
        m_restartBtn.onClick.AddListener(GameManager.Instance.m_restartAction.Invoke);
        m_exitBtn.onClick.AddListener(GameManager.Instance.m_exitAction.Invoke);
    }

    private void OnDisable()
    {
        m_restartBtn.onClick.RemoveAllListeners();
        m_exitBtn.onClick.RemoveAllListeners();
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
