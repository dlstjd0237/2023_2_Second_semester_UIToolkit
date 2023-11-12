using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WallGenerator
{
    public static void CreateWalls(HashSet<Vector2Int> floorPosition, TilemapVisualizer tilemapVisualizer)
    {
        //���ڹ��� ������ �����غ���
        HashSet<Vector2Int> basicWallPosition = FindWallsInDirection(floorPosition, Direction2D.cardinalDirectionList);
        CreateBasicWall(tilemapVisualizer, basicWallPosition, floorPosition);

        //�ڳʹ��� �� ����
        HashSet<Vector2Int> cornerWallPosition = FindWallsInDirection(floorPosition, Direction2D.diagonalDirectionList);
        CreateCornerWall(tilemapVisualizer, cornerWallPosition, floorPosition);
    }

    private static void CreateCornerWall(TilemapVisualizer tilemapVisualizer, HashSet<Vector2Int> cornerWallPosition, HashSet<Vector2Int> floorPosition)
    {
        foreach (Vector2Int position in cornerWallPosition) //������ �밢�� ���� �����ǵ���
        {
            int binaryType = 0;
            foreach (Vector2Int direction in Direction2D.eightDirectionList)
            {
                Vector2Int neighborPosition = position + direction;
                if (floorPosition.Contains(neighborPosition))
                {
                    binaryType = (binaryType << 1) + 1;
                }
                else
                {
                    binaryType <<= 1;
                } //8��Ʈ
            }
            tilemapVisualizer.PaintSingleCornerWall(position, binaryType);

        }
    }

    private static HashSet<Vector2Int> FindWallsInDirection(HashSet<Vector2Int> floorPosition, List<Vector2Int> cardinalDirectionList)
    {
        HashSet<Vector2Int> wallPositions = new HashSet<Vector2Int>();

        foreach (var position in floorPosition) //��� ���ǿ� ���ؼ�
        {
            foreach (Vector2Int direction in cardinalDirectionList)
            {
                Vector2Int neighborPosition = position + direction;
                if (floorPosition.Contains(neighborPosition) == false)
                {
                    wallPositions.Add(neighborPosition);
                }
            }
        }

        return wallPositions;
    }


    private static void CreateBasicWall(TilemapVisualizer tilemapVisualizer, HashSet<Vector2Int> basicWallPosition, HashSet<Vector2Int> floorPosition)
    {
        foreach (Vector2Int position in basicWallPosition)
        {
            int binaryType = 0;
            foreach (Vector2Int direction in Direction2D.cardinalDirectionList)
            {
                Vector2Int neighborPosition = position + direction;
                if (floorPosition.Contains(neighborPosition))
                {
                    binaryType = (binaryType << 1) + 1;
                }
                else
                {
                    binaryType <<= 1;
                }
            }

            tilemapVisualizer.PaintSingleBasicWall(position, binaryType);
        }
    }

}