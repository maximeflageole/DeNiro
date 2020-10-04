using UnityEngine;
using UnityEngine.UI;

public class TowerUiButton : MonoBehaviour
{
	[SerializeField]
	private GameObject m_towerPrefab;
	[SerializeField]
	private Image m_buttonImage;
	[SerializeField]
	private TowerData m_towerData;

	void Start()
	{
		m_buttonImage.sprite = m_towerData.TowerSprite;

		Button btn = GetComponent<Button>();
		btn.onClick.AddListener(OnClick);
	}

	void OnClick()
	{
		var towerInPlacement = Instantiate(m_towerPrefab).GetComponent<TowerInPlacement>();
		towerInPlacement.Init(m_towerData);
		PlayerControls.Instance.StartPlacingTower(towerInPlacement);
	}
}