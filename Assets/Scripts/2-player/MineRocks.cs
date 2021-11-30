using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;
using System.Linq;

public class MineRocks : MonoBehaviour
{
    [SerializeField] TileBase[] mineAble;
    [SerializeField] Tile tileToChange;
    [SerializeField] float slowAmount = 2f;
    float slowSpeed;
    float regularSpeed;
    float speedToUse;
    KeyboardMoverByTile keyboardMover;
    Tilemap tilemap;

    // Start is called before the first frame update

    private void Start()
    {
        keyboardMover = GetComponent < KeyboardMoverByTile>();
        tilemap = keyboardMover.tilemap;
        regularSpeed = keyboardMover.timeBetweenSteps;
        slowSpeed = regularSpeed * slowAmount;
    }
    // Update is called once per frame
    void Update()
    {
        //if the user presses X start the mining process
        if (Input.GetKey(KeyCode.X))
        { // set the speed to use to the slowSpeed
            speedToUse = slowSpeed;
            keyboardMover.timeBetweenSteps = speedToUse;
            //check if the next tile would've been a mountain (or other minable tiles)
            TileBase tileOnNewPosition = tilemap.GetTile(tilemap.WorldToCell(keyboardMover.newPosition));
            if (mineAble.Contains(tileOnNewPosition))
            {   //if yes, simply change the current tile to something else.
                tilemap.SetTile(tilemap.WorldToCell(keyboardMover.newPosition), tileToChange);
            }
        }    
        if (Input.GetKeyUp(KeyCode.X))
        { //once X is not pressed return to regular speed;
            speedToUse = regularSpeed;
            keyboardMover.timeBetweenSteps = speedToUse;
        }
    }
}
