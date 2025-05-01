using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

// Manages block placement/removal in the build phase.
public class BuildManager : MonoBehaviour
{
    public static BuildManager Instance { get; private set; }

    [System.Serializable]
    public class BlockTypeData
    {
        public string name;
        public GameObject prefab;
        public Sprite icon;
        public int maxCount;

        [HideInInspector] public int currentCount;
        [HideInInspector] public BlockButton buttonUI;
    }

    [Header("Level Setup")]
    public Tilemap initialTilemap;

    [Header("Block Types")]
    public List<BlockTypeData> blockTypes;

    [Header("UI")]
    public Transform blockPanel;
    public GameObject blockButtonPrefab;

    [Header("Previews")]
    public GameObject removalPreviewPrefab;

    [Header("Parenting")]
    public Transform placedBlocksParent;

    Grid _grid;
    Dictionary<Vector3Int, GameObject> _placedBlocks = new Dictionary<Vector3Int, GameObject>();

    int _selectedType = -1;
    bool _buildingEnabled = true;

    GameObject _placementPreview;
    SpriteRenderer _placementPreviewRend;
    Vector3Int _lastPlacementCell = new Vector3Int(int.MinValue, int.MinValue, int.MinValue);

    GameObject _removalPreview;
    SpriteRenderer _removalPreviewRend;
    Vector3Int _lastRemovalCell = new Vector3Int(int.MinValue, int.MinValue, int.MinValue);

    void Awake()
    {
        if (Instance == null) Instance = this;
        else { Destroy(gameObject); return; }

        _grid = initialTilemap.layoutGrid;
        InitializeUI();
        CreateRemovalPreview();
    }

    void Update()
    {
        if (!_buildingEnabled) return;

        Vector3 mw = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int cell = _grid.WorldToCell(mw);

        UpdatePlacementPreview(cell);
        UpdateRemovalPreview(cell);

        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
            return;

        if (Input.GetMouseButtonDown(0))
        {
            if (Input.GetKey(KeyCode.LeftShift))
                TryRemove(cell);
            else
                TryPlace(cell);
        }
    }

    void InitializeUI()
    {
        for (int i = 0; i < blockTypes.Count; i++)
        {
            var bt = blockTypes[i];
            bt.currentCount = bt.maxCount;

            var go = Instantiate(blockButtonPrefab, blockPanel);
            go.SetActive(true);

            var btn = go.GetComponent<BlockButton>();
            btn.Init(this, i, bt.icon, bt.currentCount);
            bt.buttonUI = btn;
        }
    }

    void CreateRemovalPreview()
    {
        _removalPreview = Instantiate(removalPreviewPrefab, transform);
        _removalPreviewRend = _removalPreview.GetComponent<SpriteRenderer>();
        _removalPreview.SetActive(false);
    }

    void CreatePlacementPreview()
    {
        DestroyPlacementPreview();
        var prefab = blockTypes[_selectedType].prefab;
        _placementPreview = Instantiate(prefab, transform);
        _placementPreviewRend = _placementPreview.GetComponent<SpriteRenderer>();
        foreach (var c in _placementPreview.GetComponents<Collider2D>()) Destroy(c);
        foreach (var mb in _placementPreview.GetComponents<MonoBehaviour>())
            if (!(mb is BlockInstance)) Destroy(mb);
        _placementPreviewRend.color = new Color(1, 1, 1, 0.5f);
    }

    void DestroyPlacementPreview()
    {
        if (_placementPreview != null) Destroy(_placementPreview);
        _lastPlacementCell = new Vector3Int(int.MinValue, int.MinValue, int.MinValue);
    }

    public void SelectBlockType(int typeIndex)
    {
        _selectedType = typeIndex;
        DestroyPlacementPreview();
        CreatePlacementPreview();
    }

    void UpdatePlacementPreview(Vector3Int cell)
    {
        if (_selectedType < 0 || _placementPreview == null)
        {
            if (_placementPreview != null) _placementPreview.SetActive(false);
            return;
        }
        if (blockTypes[_selectedType].currentCount <= 0)
        {
            DestroyPlacementPreview();
            return;
        }
        if (cell != _lastPlacementCell)
        {
            _lastPlacementCell = cell;
            _placementPreview.transform.position = _grid.GetCellCenterWorld(cell);
            bool valid = !initialTilemap.HasTile(cell) && !_placedBlocks.ContainsKey(cell);
            _placementPreviewRend.color = valid ? new Color(1, 1, 1, 0.5f) : new Color(1, 0, 0, 0.5f);
        }
        _placementPreview.SetActive(true);
    }

    void UpdateRemovalPreview(Vector3Int cell)
    {
        bool removalActive = Input.GetKey(KeyCode.LeftShift);
        if (!removalActive)
        {
            _removalPreview.SetActive(false);
            _lastRemovalCell = new Vector3Int(int.MinValue, int.MinValue, int.MinValue);
            return;
        }
        if (cell != _lastRemovalCell)
        {
            _lastRemovalCell = cell;
            _removalPreview.transform.position = _grid.GetCellCenterWorld(cell);
            bool validRemove = _placedBlocks.ContainsKey(cell);
            _removalPreviewRend.color = validRemove ? new Color(1, 1, 1, 0.5f) : new Color(1, 0, 0, 0.5f);
        }
        _removalPreview.SetActive(true);
    }

    void TryPlace(Vector3Int cell)
    {
        if (_selectedType < 0) return;
        var bt = blockTypes[_selectedType];
        if (bt.currentCount <= 0) return;
        if (initialTilemap.HasTile(cell)) return;
        if (_placedBlocks.ContainsKey(cell)) return;

        Vector3 spawnPos = _grid.GetCellCenterWorld(cell);
        var go = Instantiate(bt.prefab, spawnPos, Quaternion.identity, placedBlocksParent);
        var inst = go.AddComponent<BlockInstance>(); inst.typeIndex = _selectedType;
        _placedBlocks[cell] = go;

        bt.currentCount--;
        bt.buttonUI.UpdateCount(bt.currentCount);
        if (bt.currentCount <= 0)
        {
            DestroyPlacementPreview();
            _selectedType = -1;
        }
    }

    void TryRemove(Vector3Int cell)
    {
        if (!_placedBlocks.TryGetValue(cell, out var go)) return;
        var inst = go.GetComponent<BlockInstance>();
        int idx = inst.typeIndex;

        Destroy(go);
        _placedBlocks.Remove(cell);

        var bt = blockTypes[idx];
        bt.currentCount++;
        bt.buttonUI.UpdateCount(bt.currentCount);
    }

    // Called to disable all building interactions
    public void DisableBuilding()
    {
        _buildingEnabled = false;
        _selectedType = -1;
        DestroyPlacementPreview();
        _removalPreview.SetActive(false);
    }

    // Called to re-enable building interactions
    public void EnableBuilding()
    {
        _buildingEnabled = true;
    }
}
