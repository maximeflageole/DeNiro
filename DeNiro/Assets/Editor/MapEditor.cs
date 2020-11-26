using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class MapEditor : EditorWindow
{
    private BaseField<int> m_height;
    private BaseField<int> m_width;
    private VisualElement m_map;
    private TextField m_mapName;
    private List<List<TileData>> m_tilesData = new List<List<TileData>>();

    public static Dictionary<EDirection, Sprite> DirectionArrows = new Dictionary<EDirection, Sprite>();
    public static TilesData TilesData { get; set; }

    [MenuItem("Custom Tools/MapEditor")]
    public static void ShowExample()
    {
        MapEditor wnd = GetWindow<MapEditor>();
        wnd.titleContent = new GUIContent("MapEditor");
    }

    public void OnEnable()
    {
        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;

        TilesData = AssetDatabase.LoadAssetAtPath<TilesData>("Assets/Resources/Data/Map/TilesData.asset");

        // Import UXML
        var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Editor/MapEditor.uxml");
        VisualElement labelFromUXML = visualTree.CloneTree();
        var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Editor/MapEditor.uss");
        labelFromUXML.styleSheets.Add(styleSheet);
        root.Add(labelFromUXML);

        m_height = root.Q<BaseField<int>>("Height");
        m_width = root.Q<BaseField<int>>("Width");

        var applyChangesBtn = root.Q<Button>("ApplyChangesBtn");
        applyChangesBtn.clicked += ApplyChangesClicked;

        m_map = root.Q<VisualElement>("Map");
        m_mapName = root.Q<TextField>("MapName");

        var saveBtn = root.Q<Button>("SaveBtn");
        saveBtn.clicked += SaveBtnClicked;

        DirectionArrows.Clear();
        DirectionArrows.Add(EDirection.Up, AssetDatabase.LoadAssetAtPath<Sprite>("Assets/Editor/Sprites/ArrowUp.png"));
        DirectionArrows.Add(EDirection.Right, AssetDatabase.LoadAssetAtPath<Sprite>("Assets/Editor/Sprites/ArrowRight.png"));
        DirectionArrows.Add(EDirection.Down, AssetDatabase.LoadAssetAtPath<Sprite>("Assets/Editor/Sprites/ArrowDown.png"));
        DirectionArrows.Add(EDirection.Left, AssetDatabase.LoadAssetAtPath<Sprite>("Assets/Editor/Sprites/ArrowLeft.png"));


    }

    public void ApplyChangesClicked()
    {
        GenerateMap(m_height.value, m_width.value);
    }

    public void SaveBtnClicked()
    {
        var tileTypes = new List<TileDataTuple>();
        foreach (var row in m_tilesData)
        {
            foreach (var tile in row)
            {
                tileTypes.Add(new TileDataTuple() { Direction = tile.m_direction, TileType = tile.m_tileType });
            }
        }

        MapData.CreateOrOverrideFile(m_mapName.value, tileTypes, m_tilesData.Count, m_tilesData[0].Count);
    }

    private void GenerateMap(int x, int y)
    {
        m_tilesData.Clear();
        m_map.Clear();

        var row = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Editor/Row.uxml");
        var rowSS = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Editor/Row.uss");

        var tile = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Editor/Tile.uxml");
        var tileSS = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Editor/Tile.uss");

        for (var i = 0; i < x; i++)
        {
            var RowUxml = row.CloneTree();
            RowUxml.styleSheets.Add(rowSS);
            m_map.Add(RowUxml);
            var rowInstance = RowUxml.Q<VisualElement>("Row");

            var rowData = new List<TileData>();
            m_tilesData.Add(rowData);
            for (var j = 0; j < y; j++)
            {
                var tileUxml = tile.CloneTree();
                tileUxml.styleSheets.Add(tileSS);
                rowInstance.Add(tileUxml);
                var tileData = new TileData();
                tileData.Instantiate(tileUxml.Q<Button>("Tile"));
                rowData.Add(tileData);
            }
        }
    }
}