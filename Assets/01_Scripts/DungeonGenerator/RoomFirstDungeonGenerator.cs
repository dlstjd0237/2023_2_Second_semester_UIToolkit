using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        List<BoundsInt> roomBoundingList = Direction2D.ProceduralGenerationAlgorithms.BinarySpacePartitioning(
            rootBound,_minRoomWidth,_minRoomHeight
            );

        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();

        if (_randomWalkRoom)
        {
            //������ �غ���
        }
        else
        {
            floor = CreateSimpleRoom(roomBoundingList);
        }

        //����� �մ� ������ ����� ���ؼ��� �� ���� �߽����� ���ؾ� �ϴµ�
        List<Vector2Int> roomCenters = new List<Vector2Int>();
        //�̰� �����ֿ� �Ѵ�.
        foreach (BoundsInt room in roomBoundingList)
        {
            //BoundsInt�� �ִ� center��� ������Ƽ�� ����ؼ� �ش� �ٿ���� �߽����� ���ϰ� ���ڿ�.
            //�װ� Vector3Int���� �ݿø��ؼ� �����°�.
            roomCenters.Add((Vector2Int)Vector3Int.RoundToInt(room.center));
        }
        //�׸��� �� �� �߽����� �������� ����Ʈ���� ���� ������θ� ������ְ�
        HashSet<Vector2Int> corriodors = ConnectRoom(roomCenters);

        //���� �����ش�
        floor.UnionWith(corriodors);
        
        _tilemapVisualizer.PaintFloorTiles(floor);
        WallGenerator.CreateWalls(floor, _tilemapVisualizer);
    }

    private HashSet<Vector2Int> ConnectRoom(List<Vector2Int> roomCenters)
    {
        HashSet<Vector2Int> corridors = new HashSet<Vector2Int>();
        int targetIndex = UnityEngine.Random.Range(0, roomCenters.Count);//������ ���� �ϳ� ���
        Vector2Int targetCenter = roomCenters[targetIndex];
        roomCenters.RemoveAt(targetIndex);

        while (roomCenters.Count > 0)
        {
            //��� ���� �߽����� ���� �p- target�߽������� �Ÿ��񱳸� ���ؼ� �������� �����ϰ�, �ű⿡ ù��°�� �̾ƿ´�.
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

        corridor.Add(position); //���������� ���ϰ�
        while(position.y != destination.y)
        {
            int delta = destination.y - position.y > 0 ? 1 : -1; //���� ������ �Ʒ��� ������
            position += new Vector2Int(0, delta);
            corridor.Add(position);
            //���⼭ ���� 3ĭ¥�� ��η� ����ٸ� ����� ������ �ִ�.
            if (_path3X3)
            {
                corridor.Add(position + new Vector2Int(-1,0));
                corridor.Add(position + new Vector2Int(1, 0));
            }
        }

        //���Ⱑ ���� ����

        while (position.x != destination.x)
        {
            int delta = destination.x - position.x > 0 ? 1 : -1; // ���� ������ �Ʒ��� ������
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
            //x��� y������ �簢���� �� ä���ָ� �Ǵµ� offSet�� �پ �ִ´�.
            //���� for������ �ۼ��ϸ� ������
        }
        return floor;
    }
}
