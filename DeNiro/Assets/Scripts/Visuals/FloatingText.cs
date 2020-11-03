using TMPro;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    [SerializeField]
    protected TextMeshPro m_textMesh;
    [SerializeField]
    protected AnimationCurve m_animCurveYAxis;
    [SerializeField]
    protected float m_maxHeight = 1.5f;
    protected float m_xAxisVariation;
    protected float m_zAxisVariation;
    [SerializeField]
    protected float m_xAndYMaxVariation = 1.0f;
    protected float m_timeSinceAlive = 0.0f;
    protected float m_duration;
    protected Vector3 m_initialPos;

    public void Init(string value, float duration = 1)
    {
        m_duration = duration;
        m_textMesh.text = value;
        GetComponent<MeshRenderer>().material.renderQueue = 5000;
        m_xAxisVariation = Random.Range(-1.0f, 1.0f);
        m_zAxisVariation = Random.Range(-1.0f, 1.0f);
        m_initialPos = transform.position;
    }

    private void Update()
    {
        m_timeSinceAlive += Time.deltaTime;
        if (m_timeSinceAlive > m_duration)
        {
            Destroy(gameObject);
            return;
        }
        var lifetime = m_timeSinceAlive / m_duration;
        var evaluation = m_animCurveYAxis.Evaluate(lifetime);
        transform.localPosition = m_initialPos + new Vector3(m_xAxisVariation * evaluation * m_xAndYMaxVariation, evaluation * m_maxHeight, m_zAxisVariation * evaluation * m_xAndYMaxVariation);
    }
}
