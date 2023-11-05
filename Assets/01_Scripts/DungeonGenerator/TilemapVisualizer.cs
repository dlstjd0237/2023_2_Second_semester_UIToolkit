using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapVisualizer : MonoBehaviour
{
    //벽은 나중에 그리고 바닥만 그리는걸로
    [SerializeField] private Tilemap _floorTilemap;
    [SerializeField] private TileBase _floorTile;

    public void PaintFloorTiles(IEnumerable<Vector2Int> floorPositions)
    {
        PaintTiles(floorPositions, _floorTilemap, _floorTile);
    }

    private void PaintTiles(IEnumerable<Vector2Int> positions, Tilemap tilemap, TileBase tile)
    {
        foreach (Vector2Int p in positions)
        {
            PaintSingleTile(p, tilemap, tile);
        }
    }

    private void PaintSingleTile(Vector2Int position, Tilemap tilemap, TileBase tile)
    {
        tilemap.SetTile((Vector3Int)position, tile);
    }

    public void Clear()
    {
        _floorTilemap.ClearAllTiles();//모든 타일 지우기
    }
}
