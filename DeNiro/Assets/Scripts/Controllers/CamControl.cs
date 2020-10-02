using UnityEngine;

public class CamControl : MonoBehaviour
{
    [SerializeField]
    private float m_speed;
    [SerializeField]
    private float m_zoomSpeed = 1;
    [SerializeField]
    private float m_rotationSpeed = 1;

    // Update is called once per frame
    void Update()
    {
        ExecuteXYMovements();
        Zoom();
        ExecuteRotation();
    }

    private void ExecuteXYMovements()
    {
        var position = new Vector3();
        if (Input.GetKey(KeyCode.W))
        {
            position += Vector3.Normalize(Vector3.Cross(transform.right, Vector3.up));
        }
        if (Input.GetKey(KeyCode.S))
        {
            position += Vector3.Normalize(Vector3.Cross(-transform.right, Vector3.up));
        }
        if (Input.GetKey(KeyCode.A))
        {
            position += Vector3.Normalize(Vector3.Cross(new Vector3(0, Time.deltaTime, 0), -transform.up));
        }
        if (Input.GetKey(KeyCode.D))
        {
            position += Vector3.Normalize(Vector3.Cross(new Vector3(0, Time.deltaTime, 0), transform.up));
        }
        transform.position += Vector3.Normalize(position) * m_speed;
    }

    private void Zoom()
    {
        var zoom = Input.mouseScrollDelta.y;
        if (zoom != 0)
        {
            transform.position += transform.forward * m_zoomSpeed * zoom;
        }
    }

    private void ExecuteRotation()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(0, - m_rotationSpeed * Time.deltaTime, 0, Space.World);
        }
        else if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(0, m_rotationSpeed * Time.deltaTime, 0, Space.World);
        }
    }
}
