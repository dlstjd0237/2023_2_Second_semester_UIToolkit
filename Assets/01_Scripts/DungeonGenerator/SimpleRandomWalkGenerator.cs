using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;
public class SimpleRandomWalkGenerator : AbstractDungeonGenerator
{
    [SerializeField] protected RandomWalkSO _walkData;
    protected override void RunProceduralGeneration()
    {
        _tilemapVisualizer.Clear();
        //�ڵ����� �ߺ��� �������ش�.
        HashSet<Vector2Int> floorPosition = RunRandomWalk(_startPosition);
        _tilemapVisualizer.PaintFloorTiles(floorPosition);

        //����� ���� �׷��ִ� ���� ������ .. �̰Ŵ� ���߿�
    }

    protected HashSet<Vector2Int> RunRandomWalk(Vector2Int startPosition)
    {
        Vector2Int currentPosition = startPosition;

        HashSet<Vector2Int> floorPosition = new HashSet<Vector2Int>();

        for (int i = 0; i < _walkData.iteration; ++i)
        {
            var path = Direction2D.ProceduralGenerationAlgorithms.SimpleRandomWalk(currentPosition, _walkData.walkLength);
            floorPosition.UnionWith(path);//�ߺ� �����ϰ� �����Ѵ�.

            //�̶� ���� startRandomlyEachIteration�� true��
            if (_walkData.startRandomlyEachIteration)
            {
                //���������� startPosition 
                currentPosition = floorPosition.ElementAt(Random.Range(0, floorPosition.Count));
            }
        }
        if (_walkData.fillRoom)
        {
            floorPosition = Direction2D.ProceduralGenerationAlgorithms.FillRoomTile(floorPosition,_walkData.smoothLine);
        }




        return floorPosition;
    }
}
