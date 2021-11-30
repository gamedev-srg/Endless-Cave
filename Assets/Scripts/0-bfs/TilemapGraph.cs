using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/**
 * A graph that represents a tilemap, using only the allowed tiles.
 */
public class TilemapGraph: IGraph<Vector3Int> {
    private Tilemap tilemap;
    private TileBase[] allowedTiles;
    private TileBase[] slowTiles;
    private Dictionary<KeyValuePair<Vector3Int, Vector3Int>, float> distance;


    public TilemapGraph(Tilemap tilemap, TileBase[] allowedTiles,TileBase[]slowTiles) {
        this.tilemap = tilemap;
        this.allowedTiles = allowedTiles;
        this.slowTiles = slowTiles;
        distance = new Dictionary<KeyValuePair<Vector3Int, Vector3Int>,float>();
    }

    static Vector3Int[] directions = {
            new Vector3Int(-1, 0, 0),
            new Vector3Int(1, 0, 0),
            new Vector3Int(0, -1, 0),
            new Vector3Int(0, 1, 0),
    };
    public float GetDistance(Vector3Int src, Vector3Int nei)
    {
        return distance[new KeyValuePair<Vector3Int, Vector3Int>(src, nei)];
    }

    public IEnumerable<Vector3Int> Neighbors(Vector3Int node) {
        foreach (var direction in directions) {
            Vector3Int neighborPos = node + direction;
            TileBase neighborTile = tilemap.GetTile(neighborPos);

            if (allowedTiles.Contains(neighborTile))
            {
                KeyValuePair<Vector3Int, Vector3Int> pair = new KeyValuePair<Vector3Int, Vector3Int>(node, neighborPos);
                if(!distance.ContainsKey(pair))
                {
                    if (slowTiles.Contains(neighborTile))
                    {
                        distance.Add(pair,2f);
                        Debug.Log(neighborPos + "is slow");
                    }
                    else
                    {
                        distance.Add(pair, 1f);
                    }
                }
                yield return neighborPos;
            }
        }
    }
}
