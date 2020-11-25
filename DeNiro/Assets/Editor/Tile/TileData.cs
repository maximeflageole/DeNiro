using UnityEngine.UIElements;

public class TileData
{
    public Button m_uiButton;
    public TileType m_tileType;

    public void Instantiate(Button button)
    {
        m_uiButton = button;
        AssignTileType(TileType.Ground);
        button.clicked += OnClick;
    }

    private void OnClick()
    {
        m_tileType++;
        if (m_tileType == TileType.Count)
        {
            m_tileType = TileType.Spawn;
        }
        AssignTileType(m_tileType);
    }

    private void AssignTileType(TileType tileType)
    {
        m_tileType = tileType;
        m_uiButton.style.backgroundColor = MapEditor.TilesData.TilesList[(int)tileType].Color;
    }
}
