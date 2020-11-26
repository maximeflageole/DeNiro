public class Waypoint : Tile
{
    protected Waypoint m_nextWaypoint;

    public Waypoint GetNextWaypoint() { return GridManager.GetNextWaypoint(this); }

    public virtual void GetReached()
    {

    }
}
