﻿using UnityEngine;
using UnityEngine.UI;

public class TypeIcon : MonoBehaviour
{
    [SerializeField]
    private Image m_background;
    [SerializeField]
    private Image m_frame;
    [SerializeField]
    private Image m_icon;

    //TODO: REMOVE THIS
#if UNITY_EDITOR
    public int type = 0;
#endif
    public void AssignType(ECreatureType type, bool colored = true)
    {
        if (type == ECreatureType.None || type == ECreatureType.Count)
        {
            gameObject.SetActive(false);
            return;
        }

        if (GameManager.Instance.TypesManager.m_typesDataDict.TryGetValue(type, out TypeData value))
        {
            gameObject.SetActive(true);
            if (m_background != null) m_background.color = value.PrimaryColor;
            if (m_frame != null) m_frame.color = value.SecondaryColor;
            m_icon.sprite = colored? value.Sprite : value.UncoloredSprite;
            return;
        }
        Debug.LogWarning("The creature type " + type + " has not been implemented yet in TypeData");
    }

#if UNITY_EDITOR

    //TODO: REMOVE THIS
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            type++;
            type %= (int) ECreatureType.Count;
            AssignType((ECreatureType)type);
        }
    }
#endif

}
