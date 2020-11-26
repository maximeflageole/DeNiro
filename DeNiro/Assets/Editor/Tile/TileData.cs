using UnityEngine.UIElements;

public class TileData
{
    public Button m_uiButton;
    public IMGUIContainer m_image;
    public TileType m_tileType;
    public EDirection m_direction;

    public void Instantiate(Button button)
    {
        m_uiButton = button;
        m_image = m_uiButton.Q<IMGUIContainer>("Arrow");
        AssignTileType(TileType.Ground);
        AssignDirection(EDirection.Up);
        button.clicked += OnClick;
        m_uiButton.RegisterCallback<MouseUpEvent>(HandleRightClick);
    }

    private void HandleRightClick(MouseUpEvent evt)
    {
        if (evt.button != (int)MouseButton.RightMouse)
            return;

        var targetElement = evt.target as VisualElement;
        if (targetElement == null)
            return;

        OnRightClick();
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
        switch (tileType)
        {
            case TileType.Road:
            case TileType.Spawn:
                m_image.visible = true;
                break;
            default:
                m_image.visible = false;
                break;
        }
    }

    private void OnRightClick()
    {
        m_direction++;
        if (m_direction == EDirection.Count)
        {
            m_direction = EDirection.Up;
        }
        AssignDirection(m_direction);
    }

    private void AssignDirection(EDirection direction)
    {
        m_direction = direction;
        m_image.style.backgroundImage = MapEditor.DirectionArrows[direction].texture;
    }
}
