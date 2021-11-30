using UnityEngine;
using UnityEngine.Tilemaps;

/**
 * This component allows the player to move by clicking the arrow keys,
 * but only if the new position is on an allowed tile.
 */
public class KeyboardMoverByTile: KeyboardMover {
    [SerializeField] public Tilemap tilemap = null;
    [SerializeField] AllowedTiles allowedTiles = null;
    public Vector3 newPosition;


    private TileBase TileOnPosition(Vector3 worldPosition) {
        Vector3Int cellPosition = tilemap.WorldToCell(worldPosition);
        return tilemap.GetTile(cellPosition);
    }

    void Update()  {
        if (Time.time > nextStep)
        {
             newPosition = NewPosition();
            TileBase tileOnNewPosition = TileOnPosition(newPosition);
            if (allowedTiles.ContainAllow(tileOnNewPosition))
            {
                transform.position = newPosition;
            }
            else
            {
                //Debug.Log("You cannot walk on " + tileOnNewPosition + "!");
            }
        }
    }
}
