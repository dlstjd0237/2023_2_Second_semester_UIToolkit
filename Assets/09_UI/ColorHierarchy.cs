using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class ColorHierarchy : MonoBehaviour
{
#if UNITY_EDITOR
    private static Dictionary<Object, ColorHierarchy> coloredObject = new();

    static ColorHierarchy()
    {
        EditorApplication.hierarchyWindowItemOnGUI += HandleDraw;
    }

    private static void HandleDraw(int instanceID, Rect selectionRect)
    {
        Object obj = EditorUtility.InstanceIDToObject(instanceID);

        //지금 에디터에서 그리려고 하는 녀석이 색칠리스트에 들어가 있는 거
        if (obj != null && coloredObject.ContainsKey(obj))
        {
            GameObject gameObj = obj as GameObject;
            if (gameObj.TryGetComponent<ColorHierarchy>(out ColorHierarchy ch))
            {
                PaintObject(obj, selectionRect, ch);
            }
            else
            {
                coloredObject.Remove(obj);
            }
        }
    }

    private static void PaintObject(Object obj, Rect selectionRext, ColorHierarchy ch)
    {
        Rect bgRect = new Rect(selectionRext.x, selectionRext.y, selectionRext.width + 50, selectionRext.height);

        if (Selection.activeObject != obj)
        {
            EditorGUI.DrawRect(bgRect, ch.backColor);
            string name = $"{ch.prefix} {obj.name}";

            EditorGUI.LabelField(bgRect, name, new GUIStyle()
            {
                normal = new GUIStyleState { textColor = ch.fontcolor },
                fontStyle = FontStyle.Bold
            });
        }
    }

    public string prefix;
    public Color backColor;
    public Color fontcolor;

    private void Reset()
    {
        OnValidate();
    }

    private void OnValidate()
    {
        if (coloredObject.ContainsKey(this.gameObject) == false)
        {
            coloredObject.Add(this.gameObject, this);
        }
    }

#endif
}
