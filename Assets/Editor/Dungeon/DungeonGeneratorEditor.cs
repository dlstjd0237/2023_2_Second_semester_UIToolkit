using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AbstractDungeonGenerator), true)]
public class DungeonGeneratorEditor : Editor
{
    private AbstractDungeonGenerator _generator;

    private void Awake()
    {
        _generator = target as AbstractDungeonGenerator;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Generate"))//if 문 안에 있는게 콜백 함수임
        {
            _generator.GenerateDungeon();
        }
        if (GUILayout.Button("Clear"))//if 문 안에 있는게 콜백 함수임
        {
            _generator.Clear();
        }
    }
}
