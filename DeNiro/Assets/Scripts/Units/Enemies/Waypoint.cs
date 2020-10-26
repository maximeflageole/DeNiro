using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField]
    protected Waypoint m_nextWaypoint;

    public Waypoint GetNextWaypoint() { return m_nextWaypoint; }
}
