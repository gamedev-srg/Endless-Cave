using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/**
 * This component moves its object towards a given target position.
 */
public class TargetMover: MonoBehaviour {
    [SerializeField] Tilemap tilemap = null;
    [SerializeField] AllowedTiles allowedTiles = null;

    [Tooltip("The speed by which the object moves towards the target, in meters (=grid units) per second")]
    [SerializeField] float speed = 2f;

    [Tooltip("Maximum number of iterations before BFS algorithm gives up on finding a path")]
    [SerializeField] int maxIterations = 1000;

    [Tooltip("The target position in world coordinates")]
    [SerializeField] Vector3 targetInWorld;

    [Tooltip("The target position in grid coordinates")]
    [SerializeField] Vector3Int targetInGrid;
    [SerializeField] float maxSpeed = 4;
    [SerializeField] float minSpeed = 2;

    protected bool atTarget;  // This property is set to "true" whenever the object has already found the target.

    public void SetTarget(Vector3 newTarget) {
        if (targetInWorld != newTarget) 
        {
            targetInWorld = newTarget;
            targetInGrid = tilemap.WorldToCell(targetInWorld);
            atTarget = false;
            
        }
    }
    public void setTilemap(Tilemap tilemap)
    {
        this.tilemap = tilemap;
    }
    public void setAllowedTiles(AllowedTiles allowedTiles)
    {
        this.allowedTiles = allowedTiles;
    }

    public Vector3 GetTarget() {
        return targetInWorld;
    }

    //set speed for the object.
    public void setSpeed(int level, bool enemy = false)
    {
        
        if (enemy)
        {
            //set enemy speed to be half the entered amount (usually current game level)
            float speed = level / 2;
            //ensure speed isn't below or above minmax values
            if (speed < minSpeed)
            {  
                speed = minSpeed;
            }
            if (speed > maxSpeed)
            {
                speed = maxSpeed;
            }
            this.speed = speed;
        }
        else
        { //if not an enemy just set the speed to what was eneterd
            speed = level;
        }
    }

    private TilemapGraph tilemapGraph = null;
    private float timeBetweenSteps;

    protected virtual void Start() {
        tilemapGraph = new TilemapGraph(tilemap, allowedTiles.GetAllow(),allowedTiles.GetSpeed());
        timeBetweenSteps = 1 / speed;
        StartCoroutine(MoveTowardsTheTarget());
    }

    IEnumerator MoveTowardsTheTarget() {
        for(;;) {

            if (allowedTiles.ContainSpeed(tilemap.GetTile(tilemap.WorldToCell(transform.position))))
            {
                yield return new WaitForSeconds(timeBetweenSteps*2);
            }
            else
            {
                yield return new WaitForSeconds(timeBetweenSteps);
            }
            if (enabled && !atTarget)
                MakeOneStepTowardsTheTarget();
        }
    }

    private void MakeOneStepTowardsTheTarget() {
        Vector3Int startNode = tilemap.WorldToCell(transform.position);
        Vector3Int endNode = targetInGrid;
        List<Vector3Int> shortestPath = BFS.GetPath(tilemapGraph, startNode, endNode, maxIterations);
        if (shortestPath.Count >= 2) {
            Vector3Int nextNode = shortestPath[1];
            transform.position = tilemap.GetCellCenterWorld(nextNode);
        } else {
            atTarget = true;
        }
    }
}
