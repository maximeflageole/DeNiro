using UnityEngine;

public class TowerInPlacement : MonoBehaviour
{
    [SerializeField]
    private GameObject m_towerToBuild;
    private CreatureData m_creatureData;
    [SerializeField]
    protected MeshFilter m_towerMeshFilter;
    [SerializeField]
    protected GameObject m_radiusDisplay;

    public void Init(CreatureData creatureData)
    {
        m_creatureData = creatureData;
        m_towerMeshFilter.mesh = m_creatureData.TowerData.TowerMesh;
        foreach (var ability in m_creatureData.TowerData.ProjectileEffects)
        {
            var trans = Instantiate(m_radiusDisplay, transform.position, m_radiusDisplay.transform.rotation, transform).transform;
            var radius = ability.Radius / 100.0f;
            trans.localScale = new Vector3(radius, radius, radius);
        }
        foreach (var ability in m_creatureData.TowerData.StatEffects)
        {
            var trans = Instantiate(m_radiusDisplay, transform.position, m_radiusDisplay.transform.rotation, transform).transform;
            var radius = ability.Radius / 100.0f;
            trans.localScale = new Vector3(radius, radius, radius);
        }
    }

    public Tower PlaceTower(Transform transform)
    {
        var tower = Instantiate(m_towerToBuild, transform.position, transform.rotation).GetComponent<Tower>();
        tower.Init(m_creatureData);
        return tower;
    }
}
