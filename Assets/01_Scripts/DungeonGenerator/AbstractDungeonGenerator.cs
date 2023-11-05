using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractDungeonGenerator : MonoBehaviour
{
    //Ÿ�ϸ��� �׷��ִ� ������Ʈ�� �ϳ� ������ �־���ϰ�
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
