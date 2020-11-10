using UnityEngine;

public class TowerInPlacement : MonoBehaviour
{
    [SerializeField]
    private GameObject m_towerToBuild;
    private CreatureData m_creatureData;
    [SerializeField]
    protected MeshFilter m_towerMeshFilter;

    public void Init(CreatureData creatureData)
    {
        m_creatureData = creatureData;
        m_towerMeshFilter.mesh = m_creatureData.TowerData.TowerMesh;
    }

    public Tower PlaceTower(Transform transform)
    {
        var tower = Instantiate(m_towerToBuild, transform.position, transform.rotation).GetComponent<Tower>();
        tower.Init(m_creatureData);
        return tower;
    }
}
