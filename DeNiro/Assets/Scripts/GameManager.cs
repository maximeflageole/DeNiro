using UnityEngine;

public class GameManager : MonoBehaviour
{
    public const float RATE_OF_CONVERSION = 0.3f;
	public static uint MAX_LEVEL = 10;

	public CurveData m_xpCurve;

    public static GameManager Instance;

	float deltaTime = 0.0f;

	void Update()
	{
		deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
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

	public float GetNextLevelXpPercentage(uint currentXp, uint currentLvl)
    {
		var currentLevelXp = EvaluateCurrentLevelInXp(currentLvl);
		var nextLevelXp = EvaluateCurrentLevelInXp(currentLvl + 1);

		return ((float)currentXp - (float)currentLevelXp) / ((float)nextLevelXp - (float)currentLevelXp);
	}

	public bool LevelUpCheck(uint currentXp, uint currentLvl)
    {
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
}
