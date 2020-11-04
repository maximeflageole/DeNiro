using UnityEngine;

public class TdDummy : TdEnemy
{
    [SerializeField]
    protected CreatureData m_defaultCreatureData;

    protected void Start()
    {
        AssignData(m_defaultCreatureData);
    }
}