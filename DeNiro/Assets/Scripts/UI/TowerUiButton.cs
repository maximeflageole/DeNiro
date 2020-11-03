using System;
using UnityEngine;
using UnityEngine.UI;

public class TowerUiButton : MonoBehaviour
{
	[SerializeField]
	private CreatureData m_defaultData;
	[SerializeField]
	private Image m_buttonImage;
	[SerializeField]
	public CreatureData CreatureData { get; private set; }
	public Action<TowerUiButton> m_onClickCallback;

	void Start()
	{
		if (CreatureData == null)
        {
			if (m_defaultData != null)
			{
				Init(m_defaultData);
			}
			else
            {
				return;
            }
        }
		m_buttonImage.sprite = CreatureData.TowerData.TowerSprite;

		Button btn = GetComponent<Button>();
		btn.onClick.AddListener(OnClick);
	}

	void OnClick()
	{
		m_onClickCallback?.Invoke(this);
	}

	public void Init(CreatureData creatureData)
    {
		CreatureData = creatureData;
		Start();
	}
}