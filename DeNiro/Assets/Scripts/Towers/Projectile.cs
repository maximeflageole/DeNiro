using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    protected float m_damage = 10.0f;
    [SerializeField]
    protected float m_speed = 500.0f;

    protected TdEnemy m_target;

    protected void OnTriggerEnter(Collider other)
    {
        var enemy = other.GetComponent<TdEnemy>();
        if (enemy == m_target)
        {
            enemy.Damage(m_damage);
            Destroy(gameObject);
        }
    }

    public void Launch(TdEnemy target)
    {
        m_target = target;
    }

    private void Update()
    {
        if (m_target == null)
        {
            Destroy(gameObject);
            return;
        }

        var directionalVector = (m_target.transform.position - transform.position).normalized;
        GetComponent<Rigidbody>().velocity = directionalVector * Time.deltaTime * m_speed;
    }
}
