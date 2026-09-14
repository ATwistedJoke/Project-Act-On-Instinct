using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    public List<Tile> findPath(Tile start, Tile end, Dictionary<Vector2Int,Tile> searchableTiles)
    {
        /*Initialize unexplored & explored lists of Tiles*/
        List<Tile> unexplored = new List<Tile> { start }; 
        List<Tile> explored = new List<Tile>(); 
        /*Add Start Node to Unexplored List*/
        while(unexplored.Count != 0)
        {
            /*Get current tile by ordering the unexplored tiles by their F value.*/
            Tile curr = unexplored.OrderBy(x => x.F).First();
            unexplored.Remove(curr); 
            explored.Add(curr); 

            if(curr == end)
            {
                List<Tile> path = ConstructPath(curr, start); 
                return path; 
            }

            //Add Valid Neighbors
            List<Tile> neighhbors = curr.GetNeighbors(); 
            foreach(Tile nb in neighhbors)
            {
                /*Ignore if nb is not valid.*/
                if(!searchableTiles.ContainsValue(nb) || explored.Contains(nb) || nb.occupied || !nb.traversable){ continue; }
                /*Set G and H cost of nb relative to G an H, set the previous tile of nb to curr Tile. */
                nb.G = MapManager.Instance.GetDistance(start, nb); 
                nb.H = MapManager.Instance.GetDistance(end, nb);
                nb.prev = curr; 

                if (!unexplored.Contains(nb))
                {
                    unexplored.Add(nb); 
                }
            } 
        }
        /*Return Empty List*/
        return new List<Tile>(); 
    }

    /*Create path from start to end*/
    List<Tile> ConstructPath(Tile tile, Tile start)
    {
        List<Tile> path = new List<Tile>(); 
        while(tile != start)
        {
            path.Add(tile); 
            tile = tile.prev; 
        }
        path.Reverse();
        return path; 
    }
}