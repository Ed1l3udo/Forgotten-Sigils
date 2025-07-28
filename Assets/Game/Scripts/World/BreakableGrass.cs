using UnityEngine;
using UnityEngine.Tilemaps;

public class BreakableGrass : MonoBehaviour
{
    private Tilemap tilemap;

    void Start()
    {
        tilemap = GetComponent<Tilemap>();
        if (GameManager.Instance.grassHasBeenBroken)
        {
            DestroyAllTiles();
        }
    }

    public void DestroyAllTiles()
    {
        tilemap.ClearAllTiles();
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
