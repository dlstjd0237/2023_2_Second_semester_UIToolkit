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
        if (GUILayout.Button("Generate"))//if �� �ȿ� �ִ°� �ݹ� �Լ���
        {
            _generator.GenerateDungeon();
        }
        if (GUILayout.Button("Clear"))//if �� �ȿ� �ִ°� �ݹ� �Լ���
        {
            _generator.Clear();
        }
    }
}
