using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

/**
 * This component just keeps a list of allowed tiles.
 * Such a list is used both for pathfinding and for movement.
 */
public class AllowedTiles : MonoBehaviour  {
    [SerializeField] TileBase[] allowedTiles = null;
    [SerializeField] TileBase[] slowTiles = null;
    public bool ContainAllow(TileBase tile) {
        return allowedTiles.Contains(tile);
    }
    public bool ContainSpeed(TileBase tile)
    {
        return slowTiles.Contains(tile);
    }


    public TileBase[] GetAllow() { return allowedTiles;  }
    public TileBase[] GetSpeed() { return slowTiles; }
}
