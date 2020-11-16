using UnityEngine;

public class WavesManager : Waypoint
{
    [SerializeField]
    private GameObject m_spawnee;
    private int m_unitsSpawnedInWave;
    private int m_currentWaveIndex = 0;

    private float m_currentCooldown;
    private float m_nextSpawnTimer;
    private uint m_unitsCount;

    [SerializeField]
    protected WavesData m_wavesData;

    // Start is called before the first frame update
    void Start()
    {
        m_nextSpawnTimer = m_wavesData.FirstWaveDelay;
        m_unitsSpawnedInWave = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.GAME_OVER)
        {
            m_currentCooldown += Time.deltaTime;
            if (m_currentCooldown > m_nextSpawnTimer && m_unitsSpawnedInWave < m_wavesData.Waves[m_currentWaveIndex].CreaturesData.Count)
            {
                Spawn();
                ResetUnitsCooldown();
                m_unitsCount++;
            }
            else if (m_unitsCount == 0 && AreUnitsAllSpawned())
            {
                EndWave();
            }
        }   
    }

    public void OnUnitDeath()
    {
        m_unitsCount--;
    }

    private void Spawn()
    {
        var enemy = Instantiate(m_spawnee, transform.position, Quaternion.identity).GetComponent<TdEnemy>();
        enemy.AssignWaypoint(m_nextWaypoint);
        enemy.AssignData(m_wavesData.Waves[m_currentWaveIndex].CreaturesData[m_unitsSpawnedInWave]);
        m_unitsSpawnedInWave ++;
        enemy.OnDeathCallback += OnUnitDeath;
    }

    private void ResetUnitsCooldown()
    {
        m_nextSpawnTimer = m_wavesData.UnitsCooldown;
        m_currentCooldown = 0;
    }

    private void EndWave()
    {
        m_currentWaveIndex++;
        m_nextSpawnTimer = m_wavesData.WavesCooldown;
        m_currentCooldown = 0;
        m_unitsSpawnedInWave = 0;
        EndGameCheck();
    }

    private void EndGameCheck()
    {
        if (m_currentWaveIndex == m_wavesData.Waves.Count)
        {
            GameManager.Instance.EndGame(true);
        }
    }

    private bool AreUnitsAllSpawned()
    {
        return m_wavesData.Waves[m_currentWaveIndex].CreaturesData.Count == m_unitsSpawnedInWave;
    }
}
