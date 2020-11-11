using System.Collections.Generic;
using UnityEngine;

public class Tower: TdUnit
{
    [SerializeField]
    protected GameObject m_homingProjectile;
    [SerializeField]
    protected GameObject m_artilleryProjectile;
    [SerializeField]
    protected Transform m_canon;
    [SerializeField]
    protected GameObject m_aoeTriggerPrefab;
    [SerializeField]
    protected GameObject m_projectileTriggerPrefab;
    [SerializeField]
    protected MeshRenderer m_towerMeshRenderer;
    [SerializeField]
    protected MeshFilter m_towerMeshFilter;
    [SerializeField]
    protected Color m_xpGainTxtColor;
    [SerializeField]
    protected Color m_levelUpTxtColor;

    [SerializeField]
    protected List<AoeEffectTrigger> m_effectTriggers = new List<AoeEffectTrigger>();
    [SerializeField]
    protected List<ProjectileEffectTrigger> m_projectileEffectTriggers = new List<ProjectileEffectTrigger>();

    protected TowerData m_data;
    protected TowerSaveData m_saveData;
    public TowerData GetData() { return m_data; }
    public TowerSaveData GetSaveData() { return m_saveData; }

    public void Init(CreatureData creatureData, TowerSaveData saveData = null)
    {
        m_creatureData = creatureData;
        m_data = m_creatureData.TowerData;
        m_towerMeshFilter.mesh = m_data.TowerMesh;
        m_towerMeshRenderer.materials = m_data.Materials.ToArray();

        foreach (var effect in m_data.StatEffects)
        {
            var effectTrigger = Instantiate(m_aoeTriggerPrefab, transform).GetComponent<AoeEffectTrigger>();
            effectTrigger.Init(effect);
            m_effectTriggers.Add(effectTrigger);
        }
        foreach (var projectileEffect in m_data.ProjectileEffects)
        {
            var effectTrigger = Instantiate(m_projectileTriggerPrefab, transform).GetComponent<ProjectileEffectTrigger>();
            effectTrigger.Init(projectileEffect);
            m_projectileEffectTriggers.Add(effectTrigger);
            effectTrigger.ShootInvoke += Shoot;
        }

        if (saveData != null)
        {
            m_saveData = saveData;
        }
        else
        {
            m_saveData = new TowerSaveData() { id = creatureData.Id };
        }
        m_data.Stats.Init(m_saveData.level);
    }

    protected void Shoot(ProjectileData data, TdUnit target)
    {
        if (data.GetType() == typeof(ArtilleryData))
        {
            var artilleryProjectile = Instantiate(m_artilleryProjectile, m_canon.transform.position, Quaternion.identity, m_canon).GetComponent<ArtilleryProjectile>();
            if (artilleryProjectile != null)
            {
                artilleryProjectile.Init(data, target.transform.position, GetFinalStat(EStat.DefenseBuff));
            }
            return;
        }

        else if (data.GetType() == typeof(HomingProjectileData))
        {
            var homingProjectile = Instantiate(m_homingProjectile, m_canon.transform.position, Quaternion.identity, m_canon).GetComponent<HomingProjectile>();
            if (homingProjectile != null)
            {
                homingProjectile.Init(data, target, GetFinalStat(EStat.DefenseBuff));
            }
        }
    }

    public void GainXp(uint xpAmount)
    {
        m_saveData.xp += xpAmount;
        DisplayText(xpAmount.ToString("0"), m_xpGainTxtColor, false, 0.8f);
        while (GameManager.Instance.LevelUpCheck(m_saveData.xp, m_saveData.level))
        {
            LevelUp();
            DisplayText("Level up!", m_levelUpTxtColor, false, 1.6f);
        }
        PlayerControls.Instance.RefreshUnitUI();
    }

    protected void LevelUp()
    {
        m_saveData.level++;
        m_data.Stats.LevelUp();
    }

    public float GetFinalStat(EStat stat)
    {
        var statValue = 1 + m_data.Stats.GetStat(stat, 100.0f); //Default to 1.0f if stat does not have a base value
        return statValue * GetEffectMultiplier(stat); //TODO: Do I multiply buffs and debuffs or do I make them additive instead?!?
    }

    public void OnTowerSelected(bool selected = true)
    {
        foreach (var effect in m_effectTriggers)
        {
            effect.DisplayRadius(selected);
        }
        foreach (var effect in m_projectileEffectTriggers)
        {
            effect.DisplayRadius(selected);
        }
    }
}
