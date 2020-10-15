using System.Collections.Generic;

[System.Serializable]
public class WavesData
{
    public float FirstWaveDelay = 5.0f;
    public float WavesCooldown = 3.0f; //Cooldown between last unit killed from the previous wave and next wave starting
    public float UnitsCooldown = 1.0f; //How long you wait between units of a same wave
    public List<Wave> Waves;
}

[System.Serializable]
public struct Wave
{
    public List<CreatureData> CreaturesData;
}