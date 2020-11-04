using System;
using UnityEngine;
using UnityEngine.UI;

public class TowerUiButton : MonoBehaviour
{
	[SerializeField]
	private Image m_buttonImage;
	[SerializeField]
	public CreatureData CreatureData { get; private set; }
	public Action<TowerUiButton> m_onClickCallback;

	void OnClick()
	{
		m_onClickCallback?.Invoke(this);
	}

	public void Init(CreatureData creatureData)
    {
		CreatureData = creatureData;

		m_buttonImage.sprite = CreatureData.TowerData.TowerSprite;

		Button btn = GetComponent<Button>();
		btn.onClick.AddListener(OnClick);
	}
}