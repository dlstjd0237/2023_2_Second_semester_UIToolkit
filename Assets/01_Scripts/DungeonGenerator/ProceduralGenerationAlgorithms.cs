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

        //여기서 위에서 만든 Direction2D의 랜덤디렉션을 이용해서 뻗어나간 길들을 저장하면 됨.

        return path;
    }

    public static List<BoundsInt> BinarySpacePartitioning(BoundsInt spaceTosplit, int minRoomWidth, int minRoomHeight)
    {
        Queue<BoundsInt> roomsQueue = new Queue<BoundsInt>();
        List<BoundsInt> boundsInts = new List<BoundsInt>();
        roomsQueue.Enqueue(spaceTosplit);

        while (roomsQueue.Count > 0)
        {
            var room = roomsQueue.Dequeue(); //하나를 빼서

            if (room.size.y >= minRoomHeight && room.size.x >= minRoomWidth)
            {
                if (Random.value < 0.5f)//가로로 자른다
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
                else //세로로 자른다
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
        //큐에서 하나씩 뽑아서 쪼개서 다시 큐에 넣는다.
        //근데 쪼개진 녀석이 더이상 쪼갤 수 없는 크기라면 큐에 넣지말고 리시트에 넣어준다.
        // 이걸 언제까지 반복하냐면 큐가 다 비어있을때까지.
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
        //y값을 기준으로 오름차순  정렬이 된다.
        HashSet<Vector2Int> filledPosition = new HashSet<Vector2Int>();

        int currentY = list[0].y;
        int minX = 0, maxX = 0;
        minX = maxX = list[0].x;

        int beforeMinX = 0, beforeMaxX = 0;
        bool firstLine = true;

        foreach (Vector2Int pos in list)
        {
            if (pos.y != currentY) //줄이 변경되었다면 
            {
                if (smoothLine)
                {
                    if (firstLine)
                    {
                        firstLine = false;
                    }
                    else
                    {
                        //첫번째 줄이 아니면 이전줄과 보정을 해줘야 한다.
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
        //랜덤한 디렉션을 뽑아다가 
        //현재 위치로부터 corridorLength 만큼 전진하면서 리스트에 넣어주면 된다.

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
