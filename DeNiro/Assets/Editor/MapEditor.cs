using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class MapEditor : EditorWindow
{
    public static MapEditor Instance;
    private BaseField<int> m_height;
    private BaseField<int> m_width;
    private VisualElement m_mapEditorView;
    private TextField m_mapName;
    private List<List<TileData>> m_tilesData = new List<List<TileData>>();
    private List<MapButton> m_mapButtons = new List<MapButton>();
    private ListView m_mapButtonsList;
    private VisualTreeAsset m_mapBtn;

    public static Dictionary<EDirection, Sprite> DirectionArrows = new Dictionary<EDirection, Sprite>();
    public static TilesData TilesData { get; set; }
    private static string MAP_PATH = "Data/Map/Maps";

    [MenuItem("Custom Tools/MapEditor")]
    public static void ShowExample()
    {
        MapEditor wnd = GetWindow<MapEditor>();
        wnd.titleContent = new GUIContent("MapEditor");
    }

    public void OnEnable()
    {
        Instance = this;
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

        m_mapEditorView = root.Q<VisualElement>("Map");
        m_mapName = root.Q<TextField>("MapName");

        var saveBtn = root.Q<Button>("SaveBtn");
        saveBtn.clicked += SaveBtnClicked;

        DirectionArrows.Clear();
        DirectionArrows.Add(EDirection.Up, AssetDatabase.LoadAssetAtPath<Sprite>("Assets/Editor/Sprites/ArrowUp.png"));
        DirectionArrows.Add(EDirection.Right, AssetDatabase.LoadAssetAtPath<Sprite>("Assets/Editor/Sprites/ArrowRight.png"));
        DirectionArrows.Add(EDirection.Down, AssetDatabase.LoadAssetAtPath<Sprite>("Assets/Editor/Sprites/ArrowDown.png"));
        DirectionArrows.Add(EDirection.Left, AssetDatabase.LoadAssetAtPath<Sprite>("Assets/Editor/Sprites/ArrowLeft.png"));

        m_mapButtonsList = root.Q<ListView>("MapList");
        m_mapBtn = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Editor/MapBtn.uxml");
        LoadMaps();
    }

    protected void LoadMaps()
    {
        var assets = Resources.LoadAll(path: MAP_PATH);
        foreach (var asset in assets)
        {
            if (asset.GetType() == typeof(MapData))
            {
                var mapData = (MapData)asset;
                CreateLoadMapBtn(mapData.MapName);
            }
            else
            {
                Debug.LogError("The object " + asset.name + " in the folder " + MAP_PATH + " is not of type MapData. Please put it in the appropriate folder");
            }
        }
    }

    protected void LoadMap(string mapName)
    {
        var asset = Resources.Load(path: MAP_PATH + "/" + mapName);

        if (asset.GetType() == typeof(MapData))
        {
            LoadMap((MapData)asset);
        }
        else
        {
            Debug.LogError("The object " + asset.name + " in the folder " + MAP_PATH + " is not existing.");
        }
    }

    private void CreateLoadMapBtn(string mapName)
    {
        MapButton mapButton = new MapButton();
        mapButton.m_mapDataName = mapName;
        m_mapButtons.Add(mapButton);

        var btn = m_mapBtn.CloneTree().Q<Button>();
        btn.clicked += mapButton.OnClicked;
        m_mapButtonsList.Add(btn);
        btn.Q<TextElement>().text = mapName;
    }

    public void ApplyChangesClicked()
    {
        GenerateMap(m_height.value, m_width.value);
    }

    public void OnMapSelected(string mapName)
    {
        LoadMap(mapName);
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

        if (MapData.CreateOrOverrideFile(m_mapName.value, tileTypes, m_tilesData.Count, m_tilesData[0].Count))
        {
            CreateLoadMapBtn(m_mapName.value);
        }
    }

    private void GenerateMap(int x, int y)
    {
        m_tilesData.Clear();
        m_mapEditorView.Clear();

        var row = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Editor/Row.uxml");
        var rowSS = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Editor/Row.uss");

        var tile = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Editor/Tile.uxml");
        var tileSS = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Editor/Tile.uss");

        for (var i = 0; i < x; i++)
        {
            var RowUxml = row.CloneTree();
            RowUxml.styleSheets.Add(rowSS);
            m_mapEditorView.Add(RowUxml);
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

    private void LoadMap(MapData mapData)
    {
        m_mapName.value = mapData.MapName;

        m_tilesData.Clear();
        m_mapEditorView.Clear();

        var row = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Editor/Row.uxml");
        var rowSS = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Editor/Row.uss");

        var tile = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Editor/Tile.uxml");
        var tileSS = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Editor/Tile.uss");

        for (var i = 0; i < mapData.XSize; i++)
        {
            var RowUxml = row.CloneTree();
            RowUxml.styleSheets.Add(rowSS);
            m_mapEditorView.Add(RowUxml);
            var rowInstance = RowUxml.Q<VisualElement>("Row");

            var rowData = new List<TileData>();
            m_tilesData.Add(rowData);
            for (var j = 0; j < mapData.YSize; j++)
            {
                var tileUxml = tile.CloneTree();
                tileUxml.styleSheets.Add(tileSS);
                rowInstance.Add(tileUxml);
                var tileData = new TileData();
                tileData.Instantiate(tileUxml.Q<Button>("Tile"));
                rowData.Add(tileData);
                var tileTuple = mapData.GetTileDataAtPos(i * mapData.YSize + j);
                tileData.AssignTileFromTileTuple(tileTuple);
            }
        }
    }
}