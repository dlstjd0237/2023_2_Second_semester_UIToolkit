using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class Direction2D
{
    public static List<Vector2Int> cardianlDirectionList = new List<Vector2Int>()
    {
        new Vector2Int(0,1), //up
        new Vector2Int(1,0), //Up
        new Vector2Int(0,-1), //Up
        new Vector2Int(-1,0), //Up
    };

    public static Vector2Int GetRandomCardinalDirection()
    {
        return cardianlDirectionList[Random.Range(0, cardianlDirectionList.Count)];
    }
}


public static class ProceduralGenerationAlgorithms
{
    public static HashSet<Vector2Int> SimpleRandomWalk(Vector2Int startPosition, int walkLength)
    {
        HashSet<Vector2Int> path = new HashSet<Vector2Int>();

        path.Add(startPosition);
        Vector2Int prevPos = startPosition;
        for (int i = 0; i < walkLength; ++i)
        {
            Vector2Int newPos = prevPos + Direction2D.GetRandomCardinalDirection();
            path.Add(newPos);
            prevPos = newPos;
        }

        //���⼭ ������ ���� Direction2D�� �����𷺼��� �̿��ؼ� ����� ����� �����ϸ� ��.

        return path;
    }

    public static List<BoundsInt> BinarySpacePartitioning(BoundsInt spaceTosplit, int minRoomWidth, int minRoomHeight)
    {
        Queue<BoundsInt> roomsQueue = new Queue<BoundsInt>();
        List<BoundsInt> boundsInts = new List<BoundsInt>();
        roomsQueue.Enqueue(spaceTosplit);

        while (roomsQueue.Count > 0)
        {
            var room = roomsQueue.Dequeue(); //�ϳ��� ����

            if (room.size.y >= minRoomHeight && room.size.x >= minRoomWidth)
            {
                if (Random.value < 0.5f)//���η� �ڸ���
                {
                    if (room.size.y > minRoomHeight * 2)
                    {

                        SplitHorizontal(minRoomHeight, roomsQueue, room);
                    }
                    else if (room.size.x > minRoomWidth * 2)
                    {
                        SplitVertical(minRoomWidth, roomsQueue, room);
                    }
                    else
                    {
                        roomsList.Add(room);
                    }
                }
                else //���η� �ڸ���
                {
                    if (room.size.x > minRoomWidth * 2)
                    {

                        SplitHorizontal(minRoomWidth, roomsQueue, room);
                    }
                    else if (room.size.y > minRoomHeight * 2)
                    {
                        SplitVertical(minRoomHeight, roomsQueue, room);
                    }
                    else
                    {
                        roomsList.Add(room);
                    }
                }
            }

        }
        //ť���� �ϳ��� �̾Ƽ� �ɰ��� �ٽ� ť�� �ִ´�.
        //�ٵ� �ɰ��� �༮�� ���̻� �ɰ� �� ���� ũ���� ť�� �������� ����Ʈ�� �־��ش�.
        // �̰� �������� �ݺ��ϳĸ� ť�� �� �������������.
        return roomsList;
    }

    private static void SplitVertical(int minRoomWidth, Queue<BoundsInt> roomsQueue, BoundsInt room)
    {
        int xSplit = Random.Range(1, room.size.x);
        //int xSplit = Random.Range(minRoomWidth, room.size.x - minRoomWidth);

        BoundsInt room1 = new BoundsInt(room.min, new Vector3Int(xSplit, room.size.y));
        BoundsInt room2 = new BoundsInt(
            new Vector3Int(room.min.x + xSplit, room.min.y),
            new Vector3Int(room.size.x - xSplit, room.size.y));

        roomsQueue.Enqueue(room1);
        roomsQueue.Enqueue(room2);
    }

    private static void SplitHorizontal(int minRoomHeight, Queue<BoundsInt> roomsQueue, BoundsInt room)
    {
        int ySplit = Random.Range(1, room.size.y);
        //int xSplit = Random.Range(minRoomWidth, room.size.x - minRoomWidth);

        BoundsInt room1 = new BoundsInt(room.min, new Vector3Int(room.size.x, ySplit));
        BoundsInt room2 = new BoundsInt(
            new Vector3Int(room.size.x, room.min.y+ySplit),
            new Vector3Int(room.size.x , room.size.y-ySplit));

        roomsQueue.Enqueue(room1);
        roomsQueue.Enqueue(room2);
    }

    public static HashSet<Vector2Int> FillRoomTile(HashSet<Vector2Int> floorPosition, bool smoothLine)
    {
        List<Vector2Int> list = floorPosition.OrderBy(x => x.y).ToList();
        //y���� �������� ��������  ������ �ȴ�.
        HashSet<Vector2Int> filledPosition = new HashSet<Vector2Int>();

        int currentY = list[0].y;
        int minX = 0, maxX = 0;
        minX = maxX = list[0].x;

        int beforeMinX = 0, beforeMaxX = 0;
        bool firstLine = true;

        foreach (Vector2Int pos in list)
        {
            if (pos.y != currentY) //���� ����Ǿ��ٸ� 
            {
                if (smoothLine)
                {
                    if (firstLine)
                    {
                        firstLine = false;
                    }
                    else
                    {
                        //ù��° ���� �ƴϸ� �����ٰ� ������ ����� �Ѵ�.
                        minX = Mathf.Clamp(minX, beforeMinX - 1, beforeMinX + 1);
                        maxX = Mathf.Clamp(maxX, beforeMaxX - 1, beforeMaxX + 1);
                    }
                    beforeMinX = minX;
                    beforeMaxX = maxX;
                }

                FillLine(filledPosition, minX, maxX, currentY);
                currentY = pos.y;
                minX = maxX = pos.x;
            }
            else
            {
                minX = Mathf.Min(pos.x, minX);
                maxX = Mathf.Max(pos.x, maxX);
            }
        }

        if (smoothLine)
        {
            minX = Mathf.Clamp(minX, beforeMinX - 1, beforeMinX + 1);
            maxX = Mathf.Clamp(maxX, beforeMaxX - 1, beforeMaxX + 1);
        }

        FillLine(filledPosition, minX, maxX, currentY);

        floorPosition.UnionWith(filledPosition);
        return floorPosition;
    }

    public static List<Vector2Int> RandomWalkCorridor(Vector2Int currentPosition, int corridorLength)
    {
        List<Vector2Int> corridor = new List<Vector2Int>();
        var dir = Direction2D.GetRandomCardinalDirection();

        corridor.Add(currentPosition);
        for (int i = 0; i < corridorLength; i++)
        {
            currentPosition += dir;
            corridor.Add(currentPosition);
        }
        //������ �𷺼��� �̾ƴٰ� 
        //���� ��ġ�κ��� corridorLength ��ŭ �����ϸ鼭 ����Ʈ�� �־��ָ� �ȴ�.

        return corridor;
    }

    private static void FillLine(HashSet<Vector2Int> filledPosition, int start, int end, int currentY)
    {
        for (int i = start; i <= end; ++i)
        {
            filledPosition.Add(new Vector2Int(i, currentY));
        }
    }
}
