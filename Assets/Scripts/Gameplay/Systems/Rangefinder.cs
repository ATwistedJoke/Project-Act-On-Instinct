using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Rangefinder : MonoBehaviour
{
    /*struct used to store instances of tiles and how much mv it took to reach it.*/
    public struct Pair
    {
        public Tile tile {get;}
        public int mv {get; }
        public Pair(Tile t, int c)
        {
            tile = t; 
            mv = c; 
        }
    }

    /*find the maximum number of tiles able to be reached given a range*/
    int maxTiles(int range)
    {
        if(range == 0){ return 1; }
        return (4*range) + maxTiles(range-1); 
    }

    public List<Tile> findSearchableTiles(Tile center, int rng)
    {
        /*Iniitalize List to search through*/ 
        List<Pair> searchList = new List<Pair> {new Pair(center,0)}; 
        /*Dictionary stores found Tiles & the lowest cost to reach that given Tile (honestly may be redundant data)*/
        Dictionary<Tile, int> inRangeTiles = new Dictionary<Tile, int>(maxTiles(rng)); 

        /*Loop while there are searchable Tiles*/
        while(searchList.Count != 0)
        {
            /*Grab and add tile of pair to inRangeTiles while removing the tile from the searchList*/
            Pair currPair = searchList.OrderBy(x => x.mv).First(); 
            searchList.Remove(currPair);
            /*If tile has already been added, skip entire process*/
            if(inRangeTiles.ContainsKey(currPair.tile)){ continue; } 
            /*Add tile to dictionary if tile is not yet found*/
            inRangeTiles.Add(currPair.tile, currPair.mv);

            /*Add Valid Neighbors to searchList*/
            List<Tile> neighbors = currPair.tile.GetNeighbors(); 
            foreach(Tile nb in neighbors)
            {
                /*Calculate cost to reach tile from curr*/
                int cost = currPair.mv + nb.mvCost; 
                /*Skip if tile is unavailable or cost >= movement range*/
                if(cost >= rng || !nb.CanInteract){continue;}
                Pair nbPair = new Pair(nb, cost);
                searchList.Add(nbPair);
            }
        }
        return inRangeTiles.Keys.Distinct().ToList(); 
    }
}