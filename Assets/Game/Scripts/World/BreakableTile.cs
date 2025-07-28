using UnityEngine;
using UnityEngine.Tilemaps;

public class BreakableTile : MonoBehaviour
{
    private Tilemap tilemap;

    void Start()
    {
        tilemap = GetComponent<Tilemap>();
        if (GameManager.Instance.caveWallHasBeenBroken)
        {
            DestroyAllTiles();
        }
    }

    public void DestroyAllTiles()
    {
        tilemap.ClearAllTiles();
    }

    // public void DestroyTileAt(Vector3 worldPosition)
    // {
    //     Vector3Int cellPos = tilemap.WorldToCell(worldPosition);
    //     if (tilemap.HasTile(cellPos))
    //     {
    //         tilemap.SetTile(cellPos, null); 
    //         Debug.Log("Tile destruído em " + cellPos);
    //     }
    // }
}
