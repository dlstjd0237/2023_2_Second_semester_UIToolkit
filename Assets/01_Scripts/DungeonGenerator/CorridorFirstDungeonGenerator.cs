using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CorridorFirstDungeonGenerator : SimpleRandomWalkGenerator
{
    [SerializeField] private int _corridorLength = 14, _corridorCount = 5;
    [SerializeField] [Range(0.1f, 1f)] private float _roomPercent = 0.6f; //꼬다리중 60%가 방으로
    [SerializeField] private bool _brush3X3 = false;
    protected override void RunProceduralGeneration()
    {
        CorridorFirstGeneration();
    }
    private void CorridorFirstGeneration()
    {
        //바닥의 위치들이고
        HashSet<Vector2Int> floorPosition = new HashSet<Vector2Int>();
        //이건 방이 될수도 있는 위치들이다.
        HashSet<Vector2Int> potentialRoomPosition = new HashSet<Vector2Int>();

        //복도 생성하고, 나중에 이걸 이용해서 복도 크기를 3x3크기로
        List<List<Vector2Int>> corridors = CreateCorridors(floorPosition, potentialRoomPosition);

        HashSet<Vector2Int> roomFloors = CreateRooms(potentialRoomPosition);

        List<Vector2Int> deadEnds = FindAllDeadEnds(floorPosition);

        CreateRoomAtDeadEnd(deadEnds, roomFloors);

        floorPosition.UnionWith(roomFloors);

        if (_brush3X3)
        {
            //복도들을 for문을 돌면서 확장하면 됨
            for (int i = 0; i < corridors.Count; ++i)
            {
                corridors[i] = IncreaseCorridorBrush3X3(corridors[i]);
                floorPosition.UnionWith(corridors[i]);
            }

        }


        _tilemapVisualizer.PaintFloorTiles(floorPosition);
    }

    private List<Vector2Int> IncreaseCorridorBrush3X3(List<Vector2Int> corridor)
    {
        List<Vector2Int> newCorridors = new List<Vector2Int>();
        //corridor안에 있는 모든 점에 대해서 8방향 확장을 한 포인트를 넣어서 리턴하면 된다.

        for (int i = 1; i < corridor.Count; ++i)
        {
            for (int x = -1; x <= -1; ++x)
            {
                for (int y = -1; y <= 1; ++y)
                {
                    newCorridors.Add(corridor[i - 1] + new Vector2Int(x, y));
                }
            }
        }
        return newCorridors;
    }


    private void CreateRoomAtDeadEnd(List<Vector2Int> deadEnds, HashSet<Vector2Int> roomFloors)
    {
        foreach (Vector2Int position in deadEnds)
        {
            //이미 그곳에 방이 생성되어있다면 생성할 필요가 없거든
            if (roomFloors.Contains(position) == false)
            {
                HashSet<Vector2Int> deadEndFloor = RunRandomWalk(position);
                roomFloors.UnionWith(deadEndFloor);
                //생성해서 합쳐주면된다.
            }
        }
    }

    private List<Vector2Int> FindAllDeadEnds(HashSet<Vector2Int> floorPosition)
    {
        List<Vector2Int> deadEnds = new List<Vector2Int>();

        foreach (Vector2Int pos in floorPosition)
        {
            int neighborCount = 0;//이웃 갯수
            foreach (Vector2Int direction in Direction2D.diagonalDirectionList)
            {
                if (floorPosition.Contains(pos + direction))
                {
                    ++neighborCount;
                }
                if (neighborCount >= 2) break;

            }
            if (neighborCount <= 1)
            {
                deadEnds.Add(pos);
            }
        }

        //floorPosition엥 있는 모든 애들을 다 돌면서
        //걔의 이웃이 1개밖에 없는지를 체크해야디
        // 이웃은 CardirnalDirection을 체크해서
        //1개밖에 없으먄 deadEnds에 추가하게 된다.

        return deadEnds;
    }

    private HashSet<Vector2Int> CreateRooms(HashSet<Vector2Int> potentialRoomPosition)
    {
        HashSet<Vector2Int> roomPositions = new HashSet<Vector2Int>();

        int roomToCreateCount = Mathf.RoundToInt(potentialRoomPosition.Count * _roomPercent);
        //GUID
        List<Vector2Int> roomToCreate = potentialRoomPosition.OrderBy(x => Guid.NewGuid()).Take(roomToCreateCount).ToList();



        foreach (Vector2Int pos in roomToCreate)
        {
            HashSet<Vector2Int> roomFloor = RunRandomWalk(pos);
            roomPositions.UnionWith(roomFloor);
        }
        return roomPositions;
    }

    private List<List<Vector2Int>> CreateCorridors(HashSet<Vector2Int> floorPosition, HashSet<Vector2Int> potentialRoomPosition)
    {
        Vector2Int currentPosition = _startPosition;

        potentialRoomPosition.Add(currentPosition);

        List<List<Vector2Int>> corridors = new List<List<Vector2Int>>();

        for (int i = 0; i < _corridorLength; ++i)
        {
            List<Vector2Int> corridor = Direction2D.ProceduralGenerationAlgorithms.RandomWalkCorridor(
                                                                currentPosition, _corridorLength);
            corridors.Add(corridor);
            currentPosition = corridor.Last();
            //������ ��ġ�� ���� ��ġ�� ����
            potentialRoomPosition.Add(currentPosition);
            //���� �� ���ɼ��� �ִ� ���� ��ġ�� �־���
            floorPosition.UnionWith(corridor);
        }

        return corridors;
    }
}
