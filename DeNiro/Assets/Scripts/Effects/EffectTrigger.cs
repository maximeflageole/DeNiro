using System.Collections.Generic;
using UnityEngine;

public abstract class EffectTrigger : MonoBehaviour
{
    protected static float ALPHA_DISPLAY_VALUE = 100.0f/255.0f;
    protected Effect m_effect = null;

    protected List<TdEnemy> m_enemiesInCollider = new List<TdEnemy>();
    protected List<TdUnit> m_towersInCollider = new List<TdUnit>();
    [SerializeField]
    protected SpriteRenderer m_radiusRenderer;

    public virtual void Init(Effect effect)
    {
        m_effect = effect;
        var radius = effect.Radius / 100.0f;
        transform.localScale = new Vector3(radius, 5.0f, radius);
        DisplayRadius(false);
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (m_effect.Target == ETarget.Enemies)
        {
            var enemy = other.GetComponent<TdEnemy>();
            if (enemy != null)
            {
                m_enemiesInCollider.Add(enemy);
            }
            return;
        }
        if (m_effect.Target == ETarget.Towers)
        {
            var tower = other.GetComponent<TdUnit>();
            if (tower != null)
            {
                m_towersInCollider.Add(tower);
            }
            return;
        }
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        if (m_effect.Target == ETarget.Enemies)
        {
            var enemy = other.GetComponent<TdEnemy>();
            if (enemy != null)
            {
                m_enemiesInCollider.Remove(enemy);
            }
            return;
        }
        if (m_effect.Target == ETarget.Towers)
        {
            var tower = other.GetComponent<TdUnit>();
            if (tower != null)
            {
                m_towersInCollider.Remove(tower);
            }
            return;
        }
    }

    public void DisplayRadius(bool display)
    {
        var color = m_radiusRenderer.color;

        if (display)
        {
            color.a = ALPHA_DISPLAY_VALUE;
        }
        else
        {
            color.a = 0;
        }

        m_radiusRenderer.color = color;
    }
}
