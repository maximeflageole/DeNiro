using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    [SerializeField]
    protected Vector2Int m_dimensions;
    [SerializeField]
    protected GameObject m_tilePrefab;
    [SerializeField]
    protected float m_tileSize;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < m_dimensions.x; i++)
        {
            for (int j = 0; j < m_dimensions.y; j++)
            {
                var tilePosition = new Vector3(i * m_tileSize, 0, j * m_tileSize);
                Instantiate(m_tilePrefab, tilePosition, Quaternion.identity, transform);
            }
        }
    }
}
