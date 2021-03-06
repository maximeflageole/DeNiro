﻿using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "EnemyData", menuName = "Creatures/Enemy Data")]
public class EnemyData : ScriptableObject
{
    public GameObject Prefab;
    public float Speed = 200.0f;
    public uint MaxHealth = 100;
    public uint BaseXpValue = 10;
}
