using System.Collections.Generic;

[System.Serializable]
public class SaveData
{
    public static string TOWER_PATH = "Assets/Resources/Data/TowersData/";
    List<TowerSaveData> m_towersData = new List<TowerSaveData>();

    public void Save(IEnumerable<TowerSaveData> towersData)
    {
        m_towersData.Clear();
        m_towersData.AddRange(towersData);
    }

    public SaveData Load()
    {
        return null;
    }
}

[System.Serializable]
public class TowerSaveData
{
    public EUnit id;
    public uint level = 0;
    public uint xp = 0;
}
