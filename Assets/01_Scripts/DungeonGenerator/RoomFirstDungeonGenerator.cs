using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomFirstDungeonGenerator : SimpleRandomWalkGenerator
{
    [SerializeField]
    private int _minRoomWidth = 8, _minRoomHeight = 8;//최소 방 크기

    [SerializeField]
    private int _dungeonWidth = 100, _dungeonHeight = 100;

    [SerializeField]
    [Range(0, 10)]
    private int _offset = 2; //방간 거리

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

        //방들을 잇는 복도를 만들기 위해서는 각 방의 중심점을 구해야 하는데
        List<Vector2Int> roomCenters = new List<Vector2Int>();
        //이건 다음주에 한다.

        //그리고 각 룸 중심점을 기준으로 신장트리를 만들어서 연결통로를 만들어주고
        //방을 합쳐준다

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
            //x축과 y축으로 사각형을 꽉 채워주면 되는데 offSet은 뛰어서 넣는다.
            //이중 for문으로 작성하면 ㅆㄱㄴ
        }
        return floor;
    }
}
