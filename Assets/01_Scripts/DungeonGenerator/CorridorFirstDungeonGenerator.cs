using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CorridorFirstDungeonGenerator : SimpleRandomWalkGenerator
{
    [SerializeField] private int _corridorLength = 14, _corridorCount = 5;
    [SerializeField] [Range(0.1f, 1f)] private float _roomPercent = 0.6f; //���ٸ��� 60%�� ������
    [SerializeField] private bool _brush3X3 = false;
    protected override void RunProceduralGeneration()
    {
        CorridorFirstGeneration();
    }
    private void CorridorFirstGeneration()
    {
        //�ٴ��� ��ġ���̰�
        HashSet<Vector2Int> floorPosition = new HashSet<Vector2Int>();
        //�̰� ���� �ɼ��� �ִ� ��ġ���̴�.
        HashSet<Vector2Int> potentialRoomPosition = new HashSet<Vector2Int>();

        //���� �����ϰ�, ���߿� �̰� �̿��ؼ� ���� ũ�⸦ 3x3ũ���
        List<List<Vector2Int>> corridors = CreateCorridors(floorPosition, potentialRoomPosition);

        HashSet<Vector2Int> roomFloors = CreateRooms(potentialRoomPosition);

        List<Vector2Int> deadEnds = FindAllDeadEnds(floorPosition);

        CreateRoomAtDeadEnd(deadEnds, roomFloors);

        floorPosition.UnionWith(roomFloors);

        if (_brush3X3)
        {
            //�������� for���� ���鼭 Ȯ���ϸ� ��
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
        //corridor�ȿ� �ִ� ��� ���� ���ؼ� 8���� Ȯ���� �� ����Ʈ�� �־ �����ϸ� �ȴ�.

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
            //�̹� �װ��� ���� �����Ǿ��ִٸ� ������ �ʿ䰡 ���ŵ�
            if (roomFloors.Contains(position) == false)
            {
                HashSet<Vector2Int> deadEndFloor = RunRandomWalk(position);
                roomFloors.UnionWith(deadEndFloor);
                //�����ؼ� �����ָ�ȴ�.
            }
        }
    }

    private List<Vector2Int> FindAllDeadEnds(HashSet<Vector2Int> floorPosition)
    {
        List<Vector2Int> deadEnds = new List<Vector2Int>();

        foreach (Vector2Int pos in floorPosition)
        {
            int neighborCount = 0;//�̿� ����
            foreach (Vector2Int direction in Direction2D.cardianlDirectionList)
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

        //floorPosition�� �ִ� ��� �ֵ��� �� ���鼭
        //���� �̿��� 1���ۿ� �������� üũ�ؾߵ�
        // �̿��� CardirnalDirection�� üũ�ؼ�
        //1���ۿ� ������ deadEnds�� �߰��ϰ� �ȴ�.

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
        Vector2Int currentPosition = _startPosition; //���������� ����

        potentialRoomPosition.Add(currentPosition);//������ġ�� ������ ������� ���ɼ��� �ִ� ��

        List<List<Vector2Int>> corridors = new List<List<Vector2Int>>();
        for (int i = 0; i < _corridorCount; ++i)
        {
            List<Vector2Int> corridor = ProceduralGenerationAlgorithms.RandomWalkCorridor(currentPosition, _corridorLength);

            corridors.Add(corridor);
            currentPosition = corridor.Last();
            //������ ��ġ�� ������ġ�� �����ϰ�
            potentialRoomPosition.Add(currentPosition);
            //���� �� ���ɼ��� �ִ� ���� ��ġ�� �־��ְ�
            floorPosition.UnionWith(corridor);
        }
        return corridors;
    }
}
