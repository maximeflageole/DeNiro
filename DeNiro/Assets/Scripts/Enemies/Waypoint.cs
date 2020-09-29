using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField]
    protected Waypoint m_nextWaypoint;

    private void OnTriggerEnter(Collider other)
    {
        var enemy = other.GetComponent<TdEnemy>();
        if (enemy != null)
        {
            enemy.AssignWaypoint(m_nextWaypoint);
        }
    }
}
