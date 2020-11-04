using UnityEngine;

public class FinalWaypoint : Waypoint
{
    [SerializeField]
    protected Pyre m_pyre;

    public override void GetReached()
    {
        m_pyre.Damage(50.0f);
    }
}