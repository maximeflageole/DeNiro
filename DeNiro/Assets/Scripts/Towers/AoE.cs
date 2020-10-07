using UnityEngine;

public class AoE : MonoBehaviour
{
    private AoEData m_data;

    public void Init(AoEData data)
    {
        m_data = data;
    }


    private void OnTriggerEnter(Collider other)
    {
        var enemy = other.gameObject.GetComponent<TdEnemy>();
        if (enemy != null)
        {
            enemy.Damage(m_data.Damage);
        }
    }
}
