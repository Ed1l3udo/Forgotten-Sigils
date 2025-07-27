using UnityEngine;
using UnityEngine.Tilemaps;

public class BreakableTile : MonoBehaviour
{
    private Tilemap tilemap;

    void Start()
    {
        tilemap = GetComponent<Tilemap>();
    }

    public void DestroyTileAt(Vector3 worldPosition)
    {
        Vector3Int cellPos = tilemap.WorldToCell(worldPosition);
        if (tilemap.HasTile(cellPos))
        {
            tilemap.SetTile(cellPos, null); // Remove o tile
            Debug.Log("Tile destruído em " + cellPos);
        }
    }
}
