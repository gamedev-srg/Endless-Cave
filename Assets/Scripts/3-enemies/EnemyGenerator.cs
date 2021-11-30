using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField] public GameObject enemyToSpawn;
    [SerializeField] GameObject target;
    Tilemap tilemap;
    [SerializeField] AllowedTiles allowedTiles;
    //keep track of our enemies in game.
    List<GameObject> currentEnemies = new List<GameObject>();
   
    public void destroyEnemies() //we don't want enemies to carry over.
    {
        foreach(GameObject enemy in currentEnemies)
        {
            Destroy(enemy);
        }
    }

    //spawn count enemies, in positions according to the positionIndex and postion vector.
    public void spawnEnemies(GameObject toSpawn, int count,string[] positionIndex,Dictionary<string,Vector3> positions,int level)
    {
        for(int i = 0; i<count; i++)
        {   
            //j is used to indicate the positionIndex, to be translated into the position dictionary.
           int j = i;
            //positionIndex.Length-1 is because the length is 1 larger then the greatest index.
            //another -1 is added (or subtracted) because I later add another +1 (explained), which will cause us to step outside bounds.
            if (j>positionIndex.Length-2)
                j = positionIndex.Length-1 - i; //used to reset j to 0 once i reaches 6, creating a round robin for the indexes.
                                                                //here I add +1 to the index because I never want the enemy to spawn where the player spawns (index 0)
           GameObject newObject = Instantiate(toSpawn, positions[positionIndex[j+1]], Quaternion.identity);
           currentEnemies.Add(newObject);

            //if the prefab has chaser set these targets and values(will crash otherwise)
           var chaser = newObject.GetComponent<Chaser>();
            if (chaser)
            {
                chaser.setTargetObject(target);
                chaser.setTilemap(tilemap);
                chaser.setAllowedTiles(allowedTiles);
                //to increase difficulty, make enemies speed scale with level
                chaser.setSpeed(level,enemy:true);
            }

            var radiusWatcher = newObject.GetComponent<RadiusWatcher>();
            if(radiusWatcher) //set the radius for the radius watcher to be half the map's size.
                radiusWatcher.setRadius(tilemap.localBounds.center.x);
        }
    }
    void Start()
    {
        tilemap = GetComponent<Tilemap>();
    }

}
