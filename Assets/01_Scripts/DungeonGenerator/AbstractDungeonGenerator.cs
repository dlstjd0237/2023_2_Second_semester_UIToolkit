using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractDungeonGenerator : MonoBehaviour
{
    //타일맵을 그려주는 컴포넌트를 하나 가지고 있어야하고
    [SerializeField] protected TilemapVisualizer _tilemapVisualizer;
    [SerializeField] protected Vector2Int _startPosition = Vector2Int.zero;

    protected abstract void RunProceduralGeneration();

    public void GenerateDungeon()
    {
        Clear();
        RunProceduralGeneration();
    }

    public void Clear()
    {
        _tilemapVisualizer.Clear();
    }
}
