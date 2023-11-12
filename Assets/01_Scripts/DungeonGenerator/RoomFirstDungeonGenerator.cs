using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        List<BoundsInt> roomBoundingList = Direction2D.ProceduralGenerationAlgorithms.BinarySpacePartitioning(
            rootBound,_minRoomWidth,_minRoomHeight
            );

        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();

        if (_randomWalkRoom)
        {
            //집에서 해보장
        }
        else
        {
            floor = CreateSimpleRoom(roomBoundingList);
        }

        //방들을 잇는 복도를 만들기 위해서는 각 방의 중심점을 구해야 하는데
        List<Vector2Int> roomCenters = new List<Vector2Int>();
        //이건 다음주에 한다.
        foreach (BoundsInt room in roomBoundingList)
        {
            //BoundsInt에 있는 center라는 프로퍼티를 사용해서 해당 바운드의 중심점을 구하고 난뒤에.
            //그걸 Vector3Int값을 반올림해서 가져온거.
            roomCenters.Add((Vector2Int)Vector3Int.RoundToInt(room.center));
        }
        //그리고 각 룸 중심점을 기준으로 신장트리를 만들어서 연결통로를 만들어주고
        HashSet<Vector2Int> corriodors = ConnectRoom(roomCenters);

        //방을 합쳐준다
        floor.UnionWith(corriodors);
        
        _tilemapVisualizer.PaintFloorTiles(floor);
        WallGenerator.CreateWalls(floor, _tilemapVisualizer);
    }

    private HashSet<Vector2Int> ConnectRoom(List<Vector2Int> roomCenters)
    {
        HashSet<Vector2Int> corridors = new HashSet<Vector2Int>();
        int targetIndex = UnityEngine.Random.Range(0, roomCenters.Count);//랜덤한 방을 하나 골라
        Vector2Int targetCenter = roomCenters[targetIndex];
        roomCenters.RemoveAt(targetIndex);

        while (roomCenters.Count > 0)
        {
            //모든 룸의 중심점을 지금 혅- target중심정과의 거리비교를 통해서 오름차순 정렬하고, 거기에 첫번째를 뽑아온다.
            Vector2Int closest = roomCenters.OrderBy(x => Vector2Int.Distance(x, targetCenter)).First();
            roomCenters.Remove(closest);

            HashSet<Vector2Int> newCorridors = CreateCorridors(targetCenter, closest);
            targetCenter = closest;

            corridors.UnionWith(newCorridors);
        }
        return corridors;
    }

    private HashSet<Vector2Int> CreateCorridors(Vector2Int targetCenter, Vector2Int destination)
    {
        HashSet<Vector2Int> corridor = new HashSet<Vector2Int>();
        Vector2Int position = targetCenter;

        corridor.Add(position); //시작지점을 더하고
        while(position.y != destination.y)
        {
            int delta = destination.y - position.y > 0 ? 1 : -1; //위로 이을지 아래로 이을지
            position += new Vector2Int(0, delta);
            corridor.Add(position);
            //여기서 만약 3칸짜리 통로로 만든다면 해줘야 할일이 있다.
            if (_path3X3)
            {
                corridor.Add(position + new Vector2Int(-1,0));
                corridor.Add(position + new Vector2Int(1, 0));
            }
        }

        //여기가 꺽는 지점

        while (position.x != destination.x)
        {
            int delta = destination.x - position.x > 0 ? 1 : -1; // 위로 이을지 아래로 이을지
            position += new Vector2Int(delta, 0);
            corridor.Add(position);
            if (_path3X3)
            {
                corridor.Add(position + new Vector2Int(-1, 0));
                corridor.Add(position + new Vector2Int(1, 0));
            }
        }

       

        return corridor;
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
