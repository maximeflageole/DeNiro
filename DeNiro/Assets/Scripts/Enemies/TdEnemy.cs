using UnityEngine;

public class TdEnemy : MonoBehaviour
{
    [SerializeField]
    protected float m_speed = 10.0f;

    protected Waypoint m_nextWaypoint;
    protected Vector3 m_directionalVector;

    public void AssignWaypoint(Waypoint waypoint)
    {
        m_nextWaypoint = waypoint;
        m_directionalVector = (m_nextWaypoint.transform.position - transform.position).normalized;
    }

    private void Update()
    {
        GetComponent<Rigidbody>().velocity = m_directionalVector * m_speed * Time.deltaTime;
    }
}
