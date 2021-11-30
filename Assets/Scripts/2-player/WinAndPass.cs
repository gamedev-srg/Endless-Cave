using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

public class WinAndPass : MonoBehaviour
{
    // script placed on the end objective, creates the next level.
    
    [SerializeField] Tilemap tilemap;
    //used to genereate the cave for the next levels
    TilemapCaveGenerator caveGenerator;
    //used to spawn the enemies for the next levels
    EnemyGenerator enemyGenerator;

    //since tiles centers are of the format x.5
    float tileOffset = 0.5f;
    //used to transfrom from 1 dimension (X or Y only) to XY
    float doubleXY = 2;

    //max enemies in a level;
    int maxEnemies=7;

    //position of the end objective
    Vector3 topRightPos;

    //will always spawn the player at this point, can change. 
    float startingPosConstant = 2.5f;

    private int currentLevel = 0;
    //increase of the next levels size (1.1 -> 10%)
    [SerializeField] float increase = 1.1f;
    //signifies you can take the end objective, used so you won't accidently trigger mulitple levels if the next one takes time to load.
    [SerializeField] bool canTake ;
    static public Dictionary<string,Vector3> positions = new Dictionary<string, Vector3>();
    //used for placement calculations on the grid. mapBounds * 2 - 0.5 is final X or Y of the map.
    float mapBounds;
   

    //these will always be the important positions in the tilemap
    private string[] positionIndex= { "bottom left","bottom middle","bottom right", "middle right", "top right", "top middle", "top left", "middle left"};

    //Each time a new map is created we need to recalcualte the positions according to the new map bounds
    //said poistions are then assigned according to their name.
    //the Dictionary is done mainly assign meaningful names to the positions(instead of using unrealted indexes)
    //AND be able to iterate over the distances sequentially using the index(name) in the positionIndex.
    private void GeneratePositions(Dictionary<string, Vector3> positions)
    {
        mapBounds = (tilemap.localBounds.extents.x) * doubleXY - tileOffset;
        
        float halfMap = Mathf.Ceil(mapBounds / 2) + tileOffset;
        positions[positionIndex[0]] = new Vector3(startingPosConstant, startingPosConstant, 0);
        positions[positionIndex[1]] = new Vector3(halfMap, startingPosConstant, 0);
        positions[positionIndex[2]] = new Vector3(mapBounds - doubleXY, startingPosConstant, 0);
        positions[positionIndex[3]] = new Vector3(mapBounds - doubleXY, halfMap, 0);
        positions[positionIndex[4]] = new Vector3(mapBounds - doubleXY, mapBounds-doubleXY, 0);
        positions[positionIndex[5]] = new Vector3(halfMap,mapBounds-doubleXY);
        positions[positionIndex[6]] = new Vector3(startingPosConstant,mapBounds-doubleXY);
        positions[positionIndex[7]] = new Vector3(startingPosConstant,halfMap,0);

        foreach(var position in positions)
        {
            Debug.Log(position.Key+":"+position.Value);
        }

    }

    //once a player gets to the objective, generate a new map.
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (canTake)
            {
                canTake = false;
                //level counter to increase difficulty
                currentLevel += 1;
                //destory all current enemies so they wont carry over
                enemyGenerator.destroyEnemies();
                //generate a new map with the increase precentage
                caveGenerator.GenerateNewMap(increase);
                
                //calculate new map bounds and generate positions 
                GeneratePositions(positions);
                //place the player at the bottom left of the screen.
                other.transform.position = positions["bottom left"];
                var healthSystem = other.GetComponent<HealthSystem>();
                healthSystem.addHealth(max: true);
                //Place the chest at a constant location at the upper right of the screen.             
                transform.position = positions["top right"];
                //generate enemies,enter how many, and which position dictionary and position index to use (assuming you have more than 1)
                enemyGenerator.spawnEnemies(enemyGenerator.enemyToSpawn, enemyCount(currentLevel), positionIndex, positions, currentLevel);
                //"unlock" the objective
                canTake = true;
            }
        }
    }
    //method to easily restart the level
    public void Gameover()
    {
        //again destory enemies and later spawn them
        enemyGenerator.destroyEnemies();
        //set the optional resetart flag to true in the method, to simply regenerate the level without grid increase, etc..
        caveGenerator.GenerateNewMap(restart: true);
        enemyGenerator.spawnEnemies(enemyGenerator.enemyToSpawn, enemyCount(currentLevel), positionIndex, positions, currentLevel);


    }
    //how many enemies will spawn.
    // currently set to half the level number
    //this increases difficulty.
    private int enemyCount(int level) 
    {
        int enemies = Mathf.FloorToInt(level / 2);
        //to make sure there is atleast 1 enemy;
        if (enemies < 1) { enemies = 1; }
        //limit enemy number;
        else if (enemies > maxEnemies) { enemies = maxEnemies; }
        return enemies;
    }
    private void Start()
    {     
        canTake = true;
        caveGenerator = tilemap.GetComponent<TilemapCaveGenerator>();
        enemyGenerator = tilemap.GetComponent<EnemyGenerator>();     
    }

}
