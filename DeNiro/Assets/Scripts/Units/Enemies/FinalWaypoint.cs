using UnityEngine;

public class FinalWaypoint : Waypoint
{
    private void OnTriggerEnter(Collider other)
    {
        var enemy = other.GetComponent<TdEnemy>();
        if (enemy != null)
        {
            enemy.Die();
        }
    }
}
