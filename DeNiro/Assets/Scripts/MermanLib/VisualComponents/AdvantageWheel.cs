using UnityEngine;
using UnityEngine.UI;

public class AdvantageWheel : MonoBehaviour
{
    [SerializeField]
    private GameObject m_iconPrefab;

    [SerializeField] private bool m_generateIcons;

    [SerializeField] private float m_circleSize;
    [SerializeField] private CreatureTypesChart m_typeChart;

    void OnValidate()
    {
        if (m_generateIcons)
        {
            for (int i = transform.childCount; i > 0; --i)
            DestroyImmediate(transform.GetChild(0).gameObject);
        
            var division = 360 / m_typeChart.TypesData.Count;
            var count = 0;
            
            
            foreach (var data in m_typeChart.TypesData)
            {
                var iconTrans = Instantiate(m_iconPrefab, transform).transform;
                iconTrans.localPosition = new Vector3(Mathf.Sin((count * division) * Mathf.Deg2Rad) * m_circleSize, Mathf.Cos((count * division) * Mathf.Deg2Rad) * m_circleSize, 0);
                count++;
                iconTrans.GetComponent<Image>().sprite = data.UncoloredSprite;
            }
                    
        }
    }
}