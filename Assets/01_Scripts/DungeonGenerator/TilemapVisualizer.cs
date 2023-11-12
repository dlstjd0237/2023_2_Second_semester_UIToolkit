using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapVisualizer : MonoBehaviour
{
    //���� ���߿� �׸��� �ٴڸ� �׸��°ɷ�
    [SerializeField] private Tilemap _floorTilemap, _wallTilemap;
    [SerializeField] private TileBase _floorTile;
    [SerializeField] private TileBase _wallTopTile, _wallSideRight, _wallBottom, _wallSideLeft, _wallFull;
    [SerializeField]
    private TileBase _wallInnerCornerDownLeft, _wallInnerCornerDownRight, _wallDiagonalCornerDownLeft,
        _wallDiagonalCornerDownRight, _wallDiagonalCornerUpLeft, _wallDiagonalCornerUpRight;

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
        _floorTilemap.ClearAllTiles();//��� Ÿ�� �����
        _wallTilemap.ClearAllTiles();
    }

    public void PaintSingleBasicWall(Vector2Int position, int tileFlag)
    {
        TileBase tile = null;

        if (WallTypesHelper.wallTop.Contains(tileFlag))
        {
            tile = _wallTopTile;
        }
        else if (WallTypesHelper.wallSideRight.Contains(tileFlag))
        {
            tile = _wallSideRight;
        }
        else if (WallTypesHelper.wallBottm.Contains(tileFlag))
        {
            tile = _wallBottom;
        }
        else if (WallTypesHelper.wallSideLeft.Contains(tileFlag))
        {
            tile = _wallSideLeft;
        }
        else if (WallTypesHelper.wallFull.Contains(tileFlag))
        {
            tile = _wallFull;
        }



        if (tile != null)
        {
            PaintSingleTile(position, _wallTilemap, tile);
        }
    }

    //���� : ������(HTMLS), NetCode, ����, Cocos2d, Godot, Win32API, DirectX  �̰��� �ϳ��� �Ἥ ����� ���°� ���� �� �̴�.

    public void PaintSingleCornerWall(Vector2Int position, int tileFlag)
    {
        //Debug.Log($"{position}, type: {tileFlag.ToString()}");
        TileBase tile = null;

        if (WallTypesHelper.wallInnerCornerDownLeft.Contains(tileFlag))
        {
            tile = _wallInnerCornerDownLeft;
        }
        else if (WallTypesHelper.wallInnerCornerDownRight.Contains(tileFlag))
        {
            tile = _wallInnerCornerDownRight;
        }
        else if (WallTypesHelper.wallDiagonalCornerDownLeft.Contains(tileFlag))
        {
            tile = _wallDiagonalCornerDownLeft;
        }
        else if (WallTypesHelper.wallDiagonalCornerDownRight.Contains(tileFlag))
        {
            tile = _wallDiagonalCornerDownRight;
        }
        else if (WallTypesHelper.wallDiagonalCornerUpLeft.Contains(tileFlag))
        {
            tile = _wallDiagonalCornerUpLeft;
        }
        else if (WallTypesHelper.wallDiagonalCornerUpRight.Contains(tileFlag))
        {
            tile = _wallDiagonalCornerUpRight;
        }
        else if (WallTypesHelper.wallFullEightDirections.Contains(tileFlag))
        {
            tile = _wallFull;
        }
        else if (WallTypesHelper.wallBottmEightDirections.Contains(tileFlag))
        {
            tile = _wallBottom;
        }

        if (tile != null)
            PaintSingleTile(position, _wallTilemap, tile);
    }
}
