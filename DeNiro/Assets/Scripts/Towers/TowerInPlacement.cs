using UnityEngine;

public class TowerInPlacement : MonoBehaviour
{
    [SerializeField]
    private GameObject m_towerToBuild;
    [SerializeField]
    private Transform m_radiusTransform;
    private TowerData m_towerData;
    [SerializeField]
    protected MeshFilter m_towerMeshFilter;

    public void Init(TowerData towerData)
    {
        m_towerData = towerData;
        m_towerMeshFilter.mesh = towerData.TowerMesh;
    }

    private void Start()
    {
        m_radiusTransform.localScale = Vector3.one * m_towerData.Radius / 100.0f;
    }

    public void PlaceTower(Transform transform)
    {
        var tower = Instantiate(m_towerToBuild, transform.position, transform.rotation).GetComponent<Tower>();
        tower.Init(m_towerData);
    }
}
