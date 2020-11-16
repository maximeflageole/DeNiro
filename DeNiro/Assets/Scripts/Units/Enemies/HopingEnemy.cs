using UnityEngine;

public class HopingEnemy : TdEnemy
{
    protected bool m_isHoping;

    protected override void Move()
    {
        if (m_isHoping)
        {
            var directionalSpeed = m_directionalVector * m_speed * Time.deltaTime * GetSpeedMultiplier();
            GetComponent<Rigidbody>().velocity = directionalSpeed;
            m_waypointDistance -= (directionalSpeed * Time.deltaTime).magnitude;

            WaypointCheck();

            Quaternion OriginalRot = transform.rotation;
            transform.LookAt(m_nextWaypoint.transform);
            Quaternion NewRot = transform.rotation;
            transform.rotation = OriginalRot;
            transform.rotation = Quaternion.Lerp(transform.rotation, NewRot, ROTATION_SPEED * Time.deltaTime);
            m_animator.SetFloat("Speed", directionalSpeed.magnitude);
        }
        else
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }

    public void Bounce()
    {
        m_isHoping = true;
    }

    public void Land()
    {
        m_isHoping = false;
    }
}
