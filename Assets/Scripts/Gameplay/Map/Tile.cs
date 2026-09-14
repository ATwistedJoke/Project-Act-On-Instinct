using UnityEngine;
using System.Collections.Generic; 

public class Tile : MonoBehaviour
{
    /*Handles the cost of the Given Tile*/ 
    public int G; //Distance of Node from start point
    public int H; //Distance of Node from end point
    public int F { get {return G + H;} } //Total Cost to reach a point. 
    public int mvCost; 
    /*Stores Information of a Tiles relation to other Tiles*/
    public Tile prev; 
    public List<Tile> neighbors = new List<Tile>(); //Listed As Up, Right, Down, Left

    /*Location & Status Handler of given Tile*/
    public Vector3Int gridLoc; 
    public Vector2Int GridLoc2D { get { return new Vector2Int(gridLoc.x, gridLoc.y); } }
    public bool occupied; //If Unit occupies Tile
    public bool traversable; //If Unit can occupy the Tile
    public bool CanInteract { get {return occupied & traversable; } }

    /*Sprite Handlers*/ 
    SpriteRenderer sprite {get {return gameObject.GetComponent<SpriteRenderer>(); } }
    public List<Sprite> arrows;

    public void ShowTile()
    {
        sprite.color = new Color(1,1,1,1); 
        sprite.sortingOrder = 1; 
    }

    public void HideTile()
    {
        sprite.color = new Color(1,1,1,0); 
        sprite.sortingOrder = 1;
    }

    /*Handle Neighbors*/
    public void FindNeighbors()
    {
        Dictionary<Vector2Int, Tile> m = MapManager.Instance.map; 
        Vector2Int locCheck = new Vector2Int(gridLoc.x, gridLoc.y + 1);
        if (m.ContainsKey(locCheck)){ neighbors.Add(m[locCheck]); }
        locCheck = new Vector2Int(gridLoc.x + 1, gridLoc.y);
        if (m.ContainsKey(locCheck)){ neighbors.Add(m[locCheck]); }
        locCheck = new Vector2Int(gridLoc.x, gridLoc.y - 1);
        if (m.ContainsKey(locCheck)){ neighbors.Add(m[locCheck]); }
        locCheck = new Vector2Int(gridLoc.x - 1, gridLoc.y);
        if (m.ContainsKey(locCheck)){ neighbors.Add(m[locCheck]); }
    }

    public List<Tile> GetNeighbors(){ return neighbors; }
}
