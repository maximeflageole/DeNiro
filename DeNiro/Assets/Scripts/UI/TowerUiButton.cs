using UnityEngine;
using UnityEngine.UI;

public class TowerUiButton : MonoBehaviour
{
	[SerializeField]
	private GameObject m_towerPrefab;

	void Start()
	{
		Button btn = GetComponent<Button>();
		btn.onClick.AddListener(OnClick);
	}

	void OnClick()
	{
		var tower = Instantiate(m_towerPrefab).GetComponent<Tower>();
		PlayerControls.Instance.StartPlacingTower(tower);
	}
}