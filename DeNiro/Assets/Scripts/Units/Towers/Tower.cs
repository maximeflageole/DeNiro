﻿using System.Collections.Generic;
using UnityEngine;

public class Tower: TdUnit
{
    [SerializeField]
    protected GameObject m_homingProjectile;
    [SerializeField]
    protected GameObject m_artilleryProjectile;
    [SerializeField]
    protected TowerRange m_radiusTransform;
    [SerializeField]
    protected Transform m_canon;
    [SerializeField]
    protected GameObject m_effectTriggerPrefab;
    [SerializeField]
    protected MeshRenderer m_towerMeshRenderer;
    [SerializeField]
    protected MeshFilter m_towerMeshFilter;

    [SerializeField]
    protected List<EffectTrigger> m_effectTriggers = new List<EffectTrigger>();

    protected TdEnemy m_target;
    protected float m_currentFireTimer;
    protected TowerData m_data;
    public TowerData GetData() { return m_data; }

    public void Init(CreatureData creatureData)
    {
        m_creatureData = creatureData;
        m_data = m_creatureData.TowerData;
        m_towerMeshFilter.mesh = m_data.TowerMesh;
        m_towerMeshRenderer.materials = m_data.Materials.ToArray();

        foreach (var effect in m_data.Effects)
        {
            var effectTrigger = Instantiate(m_effectTriggerPrefab, transform).GetComponent<EffectTrigger>();
            effectTrigger.Init(effect);
            m_effectTriggers.Add(effectTrigger);
        }
        m_data.Stats.Init();
    }

    void Start()
    {
        m_radiusTransform.transform.localScale = Vector3.one * m_data.Radius / 100.0f;
    }

    protected void Update()
    {
        if (m_data.RateOfFire <= 0.0f)
        {
            return;
        }

        m_currentFireTimer += Time.deltaTime * GetFinalStat(EStat.Haste);

        if (m_currentFireTimer > m_data.RateOfFire)
        {
            if (m_target != null)
            {
                Shoot();
                ResetTimer();
            }
        }
    }

    protected void Shoot()
    {
        if (m_data.ProjectileData.GetType() == typeof(ArtilleryData))
        {
            var artilleryProjectile = Instantiate(m_artilleryProjectile, m_canon.transform.position, Quaternion.identity, m_canon).GetComponent<ArtilleryProjectile>();
            if (artilleryProjectile != null)
            {
                artilleryProjectile.Init(m_data.ProjectileData, m_target.transform.position, GetFinalStat(EStat.Attack));
            }
            return;
        }

        else if (m_data.ProjectileData.GetType() == typeof(HomingProjectileData))
        {
            var homingProjectile = Instantiate(m_homingProjectile, m_canon.transform.position, Quaternion.identity, m_canon).GetComponent<HomingProjectile>();
            if (homingProjectile != null)
            {
                homingProjectile.Init(m_data.ProjectileData, m_target, GetFinalStat(EStat.Attack));
            }
        }
    }

    protected void ResetTimer()
    {
        m_currentFireTimer = 0.0f;
    }

    public void AssignTarget(TdEnemy target)
    {
        m_target = target;
    }

    public void GiveXP(uint xpAmount)
    {
        m_data.Stats.CurrentXp += xpAmount;
        while (GameManager.Instance.LevelUpCheck(m_data.Stats.CurrentXp, m_data.Stats.CurrentLevel))
        {
            LevelUp();
        }
        PlayerControls.Instance.RefreshUnitUI();
    }

    protected void LevelUp()
    {
        m_data.Stats.CurrentLevel++;
        m_data.Stats.LevelUp();
    }

    public float GetFinalStat(EStat stat)
    {
        var statValue = 1 + m_data.Stats.GetStat(stat, 100.0f); //Default to 1.0f if stat does not have a base value
        return statValue * GetEffectMultiplier(stat); //TODO: Do I multiply buffs and debuffs or do I make them additive instead?!?
    }
}
