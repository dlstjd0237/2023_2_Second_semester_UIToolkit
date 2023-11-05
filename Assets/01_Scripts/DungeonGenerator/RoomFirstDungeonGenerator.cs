using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomFirstDungeonGenerator : SimpleRandomWalkGenerator
{
    [SerializeField]
    private int _minRoomWidth = 8, _minRoomHeight = 8;//�ּ� �� ũ��

    [SerializeField]
    private int _dungeonWidth = 100, _dungeonHeight = 100;

    [SerializeField]
    [Range(0, 10)]
    private int _offset = 2; //�氣 �Ÿ�

    [SerializeField]
    private bool _randomWalkRoom = false;

    [SerializeField]
    private bool _path3X3 = true;

    protected override void RunProceduralGeneration()
    {
        CreateRooms();
    }

    private void CreateRooms()
    {
        var rootBound = new BoundsInt((Vector3Int)_startPosition, new Vector3Int(_dungeonWidth, _dungeonHeight, 0));
        List<BoundsInt> roomBoundingList = ProceduralGenerationAlgorithms.BinarySpacePartitioning(
            rootBound,_minRoomWidth,_minRoomHeight
            );

        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();

        if (_randomWalkRoom)
        {

        }
        else
        {
            floor = CreateSimpleRoom(roomBoundingList);
        }

        //����� �մ� ������ ����� ���ؼ��� �� ���� �߽����� ���ؾ� �ϴµ�
        List<Vector2Int> roomCenters = new List<Vector2Int>();
        //�̰� �����ֿ� �Ѵ�.

        //�׸��� �� �� �߽����� �������� ����Ʈ���� ���� ������θ� ������ְ�
        //���� �����ش�

        _tilemapVisualizer.PaintFloorTiles(floor);
    }

    private HashSet<Vector2Int> CreateSimpleRoom(List<BoundsInt> roomBoundingList)
    {
        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();

        foreach (BoundsInt roomBound in roomBoundingList)
        {
            for (int col = _offset; col < roomBound.size.x; col++)
            {
                for (int row = _offset; row < roomBound.size.y; row++)
                {
                    Vector2Int position = (Vector2Int)roomBound.min + new Vector2Int(col, row);
                    floor.Add(position);
                }
            }
            //x��� y������ �簢���� �� ä���ָ� �Ǵµ� offSet�� �پ �ִ´�.
            //���� for������ �ۼ��ϸ� ������
        }
        return floor;
    }
}
