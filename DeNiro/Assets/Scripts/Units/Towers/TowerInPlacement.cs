using UnityEngine;

public class TowerInPlacement : MonoBehaviour
{
    [SerializeField]
    private GameObject m_towerToBuild;
    [SerializeField]
    private Transform m_radiusTransform;
    private CreatureData m_creatureData;
    [SerializeField]
    protected MeshFilter m_towerMeshFilter;

    public void Init(CreatureData creatureData)
    {
        m_creatureData = creatureData;
        m_towerMeshFilter.mesh = m_creatureData.TowerData.TowerMesh;
    }

    private void Start()
    {
        m_radiusTransform.localScale = Vector3.one * m_creatureData.TowerData.Radius / 100.0f;
    }

    public void PlaceTower(Transform transform)
    {
        var tower = Instantiate(m_towerToBuild, transform.position, transform.rotation).GetComponent<Tower>();
        tower.Init(m_creatureData);
    }
}
