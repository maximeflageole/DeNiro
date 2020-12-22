using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class TowerUiButton : MonoBehaviour
{
	[SerializeField]
	private Image m_buttonImage;

	[SerializeField] private Image m_disabledImage;

	public Tower Tower { get; private set; }
	public Action<TowerUiButton> m_onClickCallback;
	public bool IsAvailable { get; private set; }

	void OnClick()
	{
		m_onClickCallback?.Invoke(this);
	}

	public void Init(Tower tower)
    {
	    Tower = tower;

		m_buttonImage.sprite = Tower.GetCreatureData().TowerData.TowerSprite;
		SetAvailableForConstruction();
		
		Button btn = GetComponent<Button>();
		btn.onClick.AddListener(OnClick);
    }

	public void SetAvailableForConstruction(bool available = true)
	{
		IsAvailable = available;
		m_disabledImage.gameObject.SetActive(!available);
	}
}