using UnityEngine;
using UnityEngine.UI;

public class TowerUiButton : MonoBehaviour
{
	[SerializeField]
	private GameObject m_towerPrefab;
	[SerializeField]
	private Image m_buttonImage;
	[SerializeField]
	private CreatureData m_creatureData;

	//TODO MF: Remove this and the Start entirely
	private bool m_isStarted;

	void Start()
	{
		if (m_creatureData == null || m_isStarted)
        {
			return;
        }
		m_buttonImage.sprite = m_creatureData.TowerData.TowerSprite;

		Button btn = GetComponent<Button>();
		btn.onClick.AddListener(OnClick);
		m_isStarted = true;
	}

	void OnClick()
	{
		var towerInPlacement = Instantiate(m_towerPrefab).GetComponent<TowerInPlacement>();
		towerInPlacement.Init(m_creatureData);
		PlayerControls.Instance.StartPlacingTower(towerInPlacement);
	}

	public void Init(CreatureData creatureData)
    {
		m_creatureData = creatureData;
		Start();
	}
}