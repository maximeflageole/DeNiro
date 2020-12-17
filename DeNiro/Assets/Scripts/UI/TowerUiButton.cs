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
		SetAvailable();
    }

	public void SetAvailable(bool available = true)
	{
		IsAvailable = available;
		Button btn = GetComponent<Button>();
		
		m_disabledImage.gameObject.SetActive(!available);

		if (available)
		{
			btn.onClick.AddListener(OnClick);
		}
		else
		{
			btn.onClick.RemoveAllListeners();
		}
	}
}