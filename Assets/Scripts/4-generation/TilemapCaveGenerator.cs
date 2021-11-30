using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;


/**
 * This class demonstrates the CaveGenerator on a Tilemap.
 * 
 * By: Erel Segal-Halevi
 * Since: 2020-12
 */

public class TilemapCaveGenerator: MonoBehaviour {
    [SerializeField] Tilemap tilemap = null;

    [Tooltip("The tile that represents a wall (an impassable block)")]
    [SerializeField] TileBase wallTile = null;

    [Tooltip("The tile that represents a floor (a passable block)")]
    [SerializeField] TileBase floorTile = null;

    [Tooltip("The percent of walls in the initial random map")]
    [Range(0, 1)]
    [SerializeField] private float randomFillPercent = 0.5f;

    [Tooltip("Length and height of the grid")]
    [SerializeField] private int gridSize = 100;

    //what I found to work nicly for the fill increase 
    [SerializeField] float increaseFillModifier = 0.05f;
    [SerializeField] float maxMapFill = 0.45f;

    private CaveGenerator caveGenerator;

   
    void Start()  {
        //To get the same random numbers each time we run the script
        Random.InitState(100);
    }

    //generate a new map, with an optional increase and restart flags for level restart upon player death
    public void GenerateNewMap(float increase = 1, bool restart = false)
    {
        //clear all tiles before new ones are created;
        tilemap.ClearAllTiles();
        //if not restarted
        if (!restart)
        {
            //increase the fill perecentages of the map using the grid size increase and an additional modifier
            //(So they will scale together (fill and size) but not at the same rate)
            randomFillPercent += increase * increaseFillModifier;
            //limit fill %
            if (randomFillPercent > maxMapFill)
                randomFillPercent = maxMapFill;
            //increase grid size
            gridSize = Mathf.CeilToInt(gridSize * increase);
            //generate new map using the new values;
            caveGenerator = new CaveGenerator(randomFillPercent, gridSize);
            caveGenerator.RandomizeMap();
            SimulateCavePattern();
        }
        //if restarting a level
        else
        {   //don't increase values, just generate a new map
            caveGenerator = new CaveGenerator(randomFillPercent, gridSize);
            caveGenerator.RandomizeMap();
            SimulateCavePattern();
        }
    }
    //Do the simulation in a coroutine so we can pause and see what's going on
    public void SimulateCavePattern()  {
       

            //Calculate the new values
            caveGenerator.SmoothMap();

            //Generate texture and display it on the plane
            GenerateAndDisplayTexture(caveGenerator.GetMap());
        Debug.Log("Simulation completed!");
        return ;
    }



    //Generate a black or white texture depending on if the pixel is cave or wall
    //Display the texture on a plane
    private void GenerateAndDisplayTexture(int[,] data) {
        for (int y = 0; y < gridSize; y++) {
            for (int x = 0; x < gridSize; x++) {
                var position = new Vector3Int(x, y, 0);
                var tile = data[x, y] == 1 ? wallTile: floorTile;
                tilemap.SetTile(position, tile);
            }
        }
    }
}
