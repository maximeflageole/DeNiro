using UnityEngine;
using UnityEngine.UI;

public class TdEnemy : MonoBehaviour
{
    [SerializeField]
    protected float m_speed = 10.0f;
    [SerializeField]
    protected float m_maxHp = 100.0f;
    [SerializeField]
    protected Image m_hpImage;
    [SerializeField]
    protected Canvas m_healthCanvas;

    protected float m_currentHp;

    protected Waypoint m_nextWaypoint;
    protected Vector3 m_directionalVector;

    private void Start()
    {
        m_currentHp = m_maxHp;
    }
    public void AssignWaypoint(Waypoint waypoint)
    {
        m_nextWaypoint = waypoint;
        m_directionalVector = (m_nextWaypoint.transform.position - transform.position).normalized;
    }

    private void Update()
    {
        GetComponent<Rigidbody>().velocity = m_directionalVector * m_speed * Time.deltaTime;

        m_hpImage.fillAmount = m_currentHp / m_maxHp;
        UiRotationUpdate();

        if (Input.GetKeyDown(KeyCode.X))
        {
            Damage(m_maxHp / 2.0f);
        }
    }

    protected void UiRotationUpdate()
    {
        Quaternion camrot = Camera.main.transform.rotation;
        m_healthCanvas.transform.rotation = camrot;
    }

    public void Damage(float damageAmount)
    {
        m_currentHp = Mathf.Clamp(m_currentHp - damageAmount, 0.0f, m_maxHp);
        if (m_currentHp <= 0.0f)
        {
            Die();
        }
    }

    protected void Die()
    {
        Destroy(gameObject);
    }
}
