public class MapButton
{
    public string m_mapDataName;

    public void OnClicked()
    {
        MapEditor.Instance.OnMapSelected(m_mapDataName);
    }
}
