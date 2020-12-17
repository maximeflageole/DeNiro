using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class Tower: TdUnit
{
    protected static string ATTACK_TRIGGER = "Attack";

    public Tile CurrentTile { get; set; }

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
    protected Color m_xpGainTxtColor;
    [SerializeField]
    protected Color m_levelUpTxtColor;
    [SerializeField]
    protected Animator m_animator;
    [SerializeField]
    protected VfxHandler m_attackVfxHandler;
    [SerializeField]
    protected VfxHandler m_victimHitHandler;
    [SerializeField]
    protected SkinnedMeshRenderer m_renderer;
    [SerializeField]
    protected Material m_inPlacementMaterial;
    [SerializeField]
    protected Material m_towerMaterial;
    protected AttackData m_nextAttackData;
    protected InstantEffectTrigger m_nextEffectTrigger;

    [SerializeField]
    protected List<AoeEffectTrigger> m_effectTriggers = new List<AoeEffectTrigger>();
    [SerializeField]
    protected List<InstantEffectTrigger> m_instantEffectTriggers = new List<InstantEffectTrigger>();

    protected TowerData m_data;
    protected TowerSaveData m_saveData;
    public TowerData GetData() { return m_data; }
    public TowerSaveData GetSaveData() { return m_saveData; }

    public void BeginPlacement(CreatureData creatureData)
    {
        m_renderer.material = m_inPlacementMaterial;
        m_creatureData = creatureData;

        gameObject.SetActive(true);
        m_animator.SetFloat("Speed", 0.0f);

        //TODO: Replug radius display
    }

    public void PlaceTower(Tile tile, TowerSaveData saveData = null)
    {
        CurrentTile = tile;
        m_renderer.material = m_towerMaterial;
        m_data = m_creatureData.TowerData;

        foreach (var effect in m_data.StatEffects)
        {
            var effectTrigger = Instantiate(m_aoeTriggerPrefab, transform).GetComponent<AoeEffectTrigger>();
            effectTrigger.Init(effect);
            m_effectTriggers.Add(effectTrigger);
        }
        foreach (var projectileEffect in m_data.ProjectileEffects)
        {
            var effectTrigger = Instantiate(m_projectileTriggerPrefab, transform).GetComponent<InstantEffectTrigger>();
            effectTrigger.Init(projectileEffect);
            m_instantEffectTriggers.Add(effectTrigger);
            effectTrigger.AttackInvoke += Attack;
        }
        foreach (var instantEffect in m_data.InstantEffects)
        {
            var effectTrigger = Instantiate(m_projectileTriggerPrefab, transform).GetComponent<InstantEffectTrigger>();
            effectTrigger.Init(instantEffect);
            m_instantEffectTriggers.Add(effectTrigger);
            effectTrigger.AttackInvoke += Attack;
        }

        if (saveData != null)
        {
            m_saveData = saveData;
        }
        else
        {
            m_saveData = new TowerSaveData() { id = m_creatureData.Id };
        }
        m_data.Stats.Init(m_saveData.level);
    }
    
    public void OnTowerReturnToInventory()
    {
        CurrentTile = null;
        MermanLib.UnityManipulator.DestroyAndClearList(ref m_instantEffectTriggers);
        MermanLib.UnityManipulator.DestroyAndClearList(ref m_effectTriggers);
        gameObject.SetActive(false);
    }

    protected void Attack(AttackData data, InstantEffectTrigger trigger)
    {
        m_nextAttackData = data;
        m_nextEffectTrigger = trigger;
        m_animator.SetTrigger(ATTACK_TRIGGER);
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
        foreach (var effect in m_instantEffectTriggers)
        {
            effect.DisplayRadius(selected);
        }
    }

    public void DoAttack()
    {
        TdUnit target;

        if (m_nextEffectTrigger == null || !m_nextEffectTrigger.TryGetTarget(out target))
        {
            m_nextEffectTrigger = null;
            m_nextAttackData = null;
            return;
        }

        if (m_nextAttackData.GetType() == typeof(ArtilleryData))
        {
            var artilleryProjectile = Instantiate(m_artilleryProjectile, m_canon.transform.position, Quaternion.identity, m_canon).GetComponent<ArtilleryProjectile>();
            if (artilleryProjectile != null)
            {
                artilleryProjectile.Init((ProjectileData)m_nextAttackData, target.transform.position, GetFinalStat(EStat.AttackBuff));
            }
        }
        else if (m_nextAttackData.GetType() == typeof(HomingProjectileData))
        {
            var homingProjectile = Instantiate(m_homingProjectile, m_canon.transform.position, Quaternion.identity, m_canon).GetComponent<HomingProjectile>();
            if (homingProjectile != null)
            {
                homingProjectile.Init((ProjectileData)m_nextAttackData, target, GetFinalStat(EStat.AttackBuff));
            }
        }
        else if (m_nextAttackData.GetType() == typeof(InstantAttackData))
        {
            var instantAttackData = (InstantAttackData)m_nextAttackData;
            if (instantAttackData.m_onHitVfx != null)
            {
                Instantiate(instantAttackData.m_onHitVfx, target.transform.position, Quaternion.identity, target.transform);
            }
            target.Damage(Projectile.GetFinalDamage(m_nextAttackData, target, GetFinalStat(EStat.AttackBuff)));
        }

        m_nextEffectTrigger = null;
        m_nextAttackData = null;
    }

    public void OnAttackBegin()
    {
        m_attackVfxHandler?.LaunchParticles();
    }
}
