using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
	private static float TICK_TIMER = 0.5f;
	private static float TICK_CURRENT_TIMER = 0f;
	
    public const float RATE_OF_CONVERSION = 0.3f;
	public static uint MAX_LEVEL = 10;
	public static bool GAME_OVER = false;

	public List<TdUnit> UnitList = new List<TdUnit>();

	public CurveData m_xpCurve;
	public UnitsDictionary m_unitsDictionary;

	public WaveTimerUI m_waveTimerUI;

    public static GameManager Instance;

	public TypesManager TypesManager;

	float deltaTime = 0.0f;

	[FormerlySerializedAs("m_mainMenuPanel")] [SerializeField]
	protected EndLevelMenu mEndLevelMenu;
	public Action m_restartAction { get; protected set; }
	public Action m_exitAction { get; protected set; }

	public void RegisterUnit(TdUnit unit)
	{
		UnitList.Add(unit);
	}

	public void UnregisterUnit(TdUnit unit)
	{
		UnitList.Remove(unit);
	}
	
	void Update()
	{
		deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
		if (Input.GetKeyDown(KeyCode.Space))
        {
			mEndLevelMenu.ToggleDisplay();
        }
	}

	private void FixedUpdate()
	{
		TICK_CURRENT_TIMER += Time.deltaTime;
		if (TICK_CURRENT_TIMER > TICK_TIMER)
		{
			TICK_CURRENT_TIMER = 0f;
			foreach (var unit in UnitList)
			{
				unit.Tick();
			}
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
		mEndLevelMenu.ToggleDisplay(victory, true);
		GAME_OVER = true;

	}
}
