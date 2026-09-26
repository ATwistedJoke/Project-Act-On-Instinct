using System; 
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager : MonoBehaviour
{
    public static MapManager _instance; 
    public static MapManager Instance {get {return _instance; } }
    public Tile TilePrefab; 
    public GameObject tileContainer; 
    [NonSerialized] public Dictionary<Vector2Int, Tile> map = new Dictionary<Vector2Int, Tile>(); 
    public List<Sprite> sprites; 
    public Tilemap tilemap; 

    public void Awake()
    {
        if(_instance != null && _instance != this){ Destroy(gameObject); }
        else{ _instance = this; }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InitializeMap(); 
    }

    public void InitializeMap()
    {
        var tileMap = gameObject.GetComponentInChildren<Grid>().GetComponentInChildren<Tilemap>(); 
        BoundsInt bounds = tileMap.cellBounds; 
        for(int z = bounds.max.z; z >= bounds.min.z; z--){
            for(int x = bounds.max.x; x >= bounds.min.x; x--){
                for(int y = bounds.max.y; y >= bounds.min.y; y--){
                    var tileLoc = new Vector3Int(x,y,z); 
                    var tileKey = new Vector2Int(x, y);
                    if (tileMap.HasTile(tileLoc) && !map.ContainsKey(tileKey))
                    {
                        var tile = Instantiate(TilePrefab, tileContainer.transform);
                        var CellWorldPos = tileMap.GetCellCenterWorld(tileLoc);
                        tile.transform.position = new Vector3(CellWorldPos.x, CellWorldPos.y, CellWorldPos.z);
                        tile.GetComponent<SpriteRenderer>().sortingOrder = tileMap.GetComponent<TilemapRenderer>().sortingOrder;
                        tile.gridLoc = tileLoc;
                        Sprite tileSprite = tileMap.GetSprite(new Vector3Int(x, y, z)); 
                        setMvCost(tile, tileSprite);
                        map.Add(tileKey, tile);
                    }
                }
            }
        }
        foreach (var t in map){ t.Value.FindNeighbors(); }
    }

    public void setMvCost(Tile tile, Sprite sprite)
    {
        if (sprite == sprites[0]){ tile.mvCost = 1; }
        else if (sprite == sprites[1] || sprite == sprites[2]){ tile.mvCost = 2; }
        else{ tile.mvCost = 3; }
    }

    public Tile GetTile(int x, int y){ return map[new Vector2Int(x, y)]; }
    
    /*Get Distance between Tile 1 and Tile 2*/ 
    public int GetDistance(Tile t1, Tile t2){ return Math.Abs(t2.GridLoc2D.x - t1.GridLoc2D.x) + Math.Abs(t2.GridLoc2D.y-t2.GridLoc2D.y); }

}