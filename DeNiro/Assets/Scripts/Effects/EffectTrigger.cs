using UnityEngine;

public class EffectTrigger : MonoBehaviour
{
    [SerializeField]
    protected Effect m_onEnterEffect = null;

    public void Init(Effect onEnterEffect)
    {
        var radius = onEnterEffect.Radius/100.0f;
        transform.localScale = new Vector3(radius, 5.0f, radius);
        m_onEnterEffect = onEnterEffect;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (m_onEnterEffect.Target == ETarget.Enemies)
        {
            return;
        }
        if (m_onEnterEffect.Target == ETarget.Towers)
        {
            var tower = other.GetComponent<Tower>();
            if (tower != null)
            {
                Debug.Log("Should apply speed buff effect to tower");
                tower.AddEffect(m_onEnterEffect);
            }
            return;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (m_onEnterEffect.Target == ETarget.Enemies)
        {
            return;
        }
        if (m_onEnterEffect.Target == ETarget.Towers)
        {
            var tower = other.GetComponent<Tower>();
            if (tower != null)
            {
                tower.RemoveEffect(m_onEnterEffect);
            }
            return;
        }
    }
}
