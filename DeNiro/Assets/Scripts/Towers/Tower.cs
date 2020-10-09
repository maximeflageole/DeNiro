using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
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

    protected Dictionary<EEffect, List<Effect>> m_effectsDictionary = new Dictionary<EEffect, List<Effect>>();
    [SerializeField]
    protected List<EffectTrigger> m_effectTriggers = new List<EffectTrigger>();

    protected TdEnemy m_target;
    protected float m_currentFireTimer;
    protected TowerData m_data;

    public void Init(TowerData towerData)
    {
        m_towerMeshFilter.mesh = towerData.TowerMesh;
        m_towerMeshRenderer.materials = towerData.Materials.ToArray();
        m_data = towerData;
        foreach (var effect in m_data.Effects)
        {
            var effectTrigger = Instantiate(m_effectTriggerPrefab, transform).GetComponent<EffectTrigger>();
            effectTrigger.Init(effect);
            m_effectTriggers.Add(effectTrigger);
        }
    }

    void Start()
    {
        m_radiusTransform.transform.localScale = Vector3.one * m_data.Radius / 100.0f;
    }

    void Update()
    {
        if (m_data.RateOfFire <= 0.0f)
        {
            return;
        }

        m_currentFireTimer += Time.deltaTime * GetAttackSpeedMultiplier();

        if (m_currentFireTimer > m_data.RateOfFire)
        {
            if (m_target != null)
            {
                Shoot();
                ResetTimer();
            }
        }
    }

    protected float GetAttackSpeedMultiplier()
    {
        float attackSpeedMultiplier = 1.0f;
        if (m_effectsDictionary.ContainsKey(EEffect.AttackSpeedBuff))
        {
            foreach (var speedBuff in m_effectsDictionary[EEffect.AttackSpeedBuff])
            {
                attackSpeedMultiplier += speedBuff.Magnitude;
            }
        }
        if (m_effectsDictionary.ContainsKey(EEffect.AttackSpeedDebuff))
        {
            foreach (var speedBuff in m_effectsDictionary[EEffect.AttackSpeedDebuff])
            {
                attackSpeedMultiplier -= speedBuff.Magnitude;
            }
        }
        attackSpeedMultiplier = Mathf.Clamp(attackSpeedMultiplier, 0.5f, 10.0f);
        return attackSpeedMultiplier;
    }

    protected void Shoot()
    {
        if (m_data.ProjectileData.GetType() == typeof(ArtilleryData))
        {
            var artilleryProjectile = Instantiate(m_artilleryProjectile, m_canon.transform.position, Quaternion.identity, m_canon).GetComponent<ArtilleryProjectile>();
            if (artilleryProjectile != null)
            {
                artilleryProjectile.Init(m_data.ProjectileData, m_target.transform.position);
            }
            return;
        }

        else if (m_data.ProjectileData.GetType() == typeof(HomingProjectileData))
        {
            var homingProjectile = Instantiate(m_homingProjectile, m_canon.transform.position, Quaternion.identity, m_canon).GetComponent<HomingProjectile>();
            if (homingProjectile != null)
            {
                homingProjectile.Init(m_data.ProjectileData, m_target);
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

    public void AddEffect(Effect effect)
    {
        if (!m_effectsDictionary.ContainsKey(effect.EffectType))
        {
            Debug.Log("Correctly adding effect to this tower");
            m_effectsDictionary.Add(effect.EffectType, new List<Effect>());
        }
        m_effectsDictionary[effect.EffectType].Add(effect);
    }

    public void RemoveEffect(Effect effect)
    {
        if (!m_effectsDictionary.ContainsKey(effect.EffectType))
        {
            return;
        }
        foreach (var effectInstance in m_effectsDictionary[effect.EffectType])
        {
            if (effectInstance == effect)
            {
                m_effectsDictionary[effect.EffectType].Remove(effectInstance);
            }
        }
        if (m_effectsDictionary[effect.EffectType].Count == 0)
        {
            m_effectsDictionary.Remove(effect.EffectType);
        }
    }
}
