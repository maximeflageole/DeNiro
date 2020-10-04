using UnityEngine;

public class TowerInPlacement : MonoBehaviour
{
    [SerializeField]
    private GameObject m_towerToBuild;
    [SerializeField]
    private float m_radius;
    [SerializeField]
    private Transform m_radiusTransform;

    private void Start()
    {
        m_radiusTransform.localScale = Vector3.one * m_radius / 100.0f;
    }

    public void PlaceTower(Transform transform)
    {
        Instantiate(m_towerToBuild, transform.position, transform.rotation);
    }
}
