using UnityEngine;

public class TdDummy : TdEnemy
{
    [SerializeField]
    protected CreatureData m_defaultCreatureData;

    private void Start()
    {
        AssignData(m_defaultCreatureData);
    }
}