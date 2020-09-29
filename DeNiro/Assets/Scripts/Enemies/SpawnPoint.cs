using UnityEngine;

public class SpawnPoint : Waypoint
{
    [SerializeField]
    private GameObject m_spawnee;
    [SerializeField]
    private float m_cooldown = 1.0f;
    [SerializeField]
    private float m_initialCooldown = 3.0f;
    [SerializeField]
    private uint m_amountToSpawn = 5;
    [SerializeField]
    private uint m_amountRemaining;

    private float m_currentCooldown;
    private float m_nextSpawnTimer;

    // Start is called before the first frame update
    void Start()
    {
        m_amountRemaining = m_amountToSpawn;
        m_nextSpawnTimer = m_initialCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        m_currentCooldown += Time.deltaTime;
        if (m_amountRemaining > 0 && m_currentCooldown > m_nextSpawnTimer)
        {
            Spawn();
            ResetCooldown();
        }
    }

    private void Spawn()
    {
        var enemy = Instantiate(m_spawnee).GetComponent<TdEnemy>();
        enemy.AssignWaypoint(m_nextWaypoint);
        m_amountRemaining--;
    }

    private void ResetCooldown()
    {
        m_nextSpawnTimer = m_cooldown;
        m_currentCooldown = 0;
    }
}
