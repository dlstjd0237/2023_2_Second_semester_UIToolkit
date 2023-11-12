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
        //자동으로 중복을 제거해준다.
        HashSet<Vector2Int> floorPosition = RunRandomWalk(_startPosition);
        _tilemapVisualizer.PaintFloorTiles(floorPosition);

        //여기다 벽을 그려주는 들어가야 하지만 .. 이거는 나중에
    }

    protected HashSet<Vector2Int> RunRandomWalk(Vector2Int startPosition)
    {
        Vector2Int currentPosition = startPosition;

        HashSet<Vector2Int> floorPosition = new HashSet<Vector2Int>();

        for (int i = 0; i < _walkData.iteration; ++i)
        {
            var path = Direction2D.ProceduralGenerationAlgorithms.SimpleRandomWalk(currentPosition, _walkData.walkLength);
            floorPosition.UnionWith(path);//중복 제거하고 병합한다.

            //이때 만약 startRandomlyEachIteration이 true면
            if (_walkData.startRandomlyEachIteration)
            {
                //시작지점을 startPosition 
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
