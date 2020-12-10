using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public const float RATE_OF_CONVERSION = 0.3f;
	public static uint MAX_LEVEL = 10;
	public static bool GAME_OVER = false;

	public CurveData m_xpCurve;
	public UnitsDictionary m_unitsDictionary;

	public WaveTimerUI m_waveTimerUI;

    public static GameManager Instance;

	public CreatureTypesChart m_creatureTypesChart;

	float deltaTime = 0.0f;

	[SerializeField]
	protected MainMenuPanel m_mainMenuPanel;
	public Action m_restartAction { get; protected set; }
	public Action m_exitAction { get; protected set; }

	void Update()
	{
		deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
		if (Input.GetKeyDown(KeyCode.Space))
        {
			m_mainMenuPanel.ToggleDisplay();
        }
	}

	void OnGUI()
	{
		int w = Screen.width, h = Screen.height;

		GUIStyle style = new GUIStyle();

		Rect rect = new Rect(0, 0, w, h * 2 / 100);
		style.alignment = TextAnchor.UpperLeft;
		style.fontSize = h * 2 / 100;
		style.normal.textColor = new Color(0.0f, 0.0f, 0.5f, 1.0f);
		float msec = deltaTime * 1000.0f;
		float fps = 1.0f / deltaTime;
		string text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
		GUI.Label(rect, text, style);
	}

	private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
		m_creatureTypesChart.OnAwake();
    }

    private void Start()
    {
		m_restartAction += RestartLevel;
		m_exitAction += ExitGame;
	}

    private void OnEnable()
    {
		GAME_OVER = false;
    }

    private void RestartLevel()
    {
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	private void ExitGame()
    {
		Application.Quit();
    }

	public float GetNextLevelXpPercentage(uint currentXp, uint currentLvl)
    {
		var currentLevelXp = EvaluateCurrentLevelInXp(currentLvl);
		var nextLevelXp = EvaluateCurrentLevelInXp(currentLvl + 1);

		return ((float)currentXp - (float)currentLevelXp) / ((float)nextLevelXp - (float)currentLevelXp);
	}

	public bool LevelUpCheck(uint currentXp, uint currentLvl)
    {
		if (currentLvl >= MAX_LEVEL)
        {
			return false;
        }
		return EvaluateCurrentLevelInXp(currentLvl + 1) <= currentXp;
    }

	public uint EvaluateCurrentLevelInXp(uint level)
    {
		if (level > MAX_LEVEL)
        {
			return 1000;
        }
		var levelIncrement = 1.0f / (float)MAX_LEVEL;
		return (uint)(m_xpCurve.Data.Evaluate(level * levelIncrement));
	}

	public void EndGame(bool victory)
    {
		m_mainMenuPanel.ToggleDisplay(victory, true);
		GAME_OVER = true;

	}
}
