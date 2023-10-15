using System;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class CreateChar : EditorWindow
{
    [SerializeField]
    private VisualTreeAsset m_VisualTreeAsset = default;

    private TextField _txtName;
    private TextField _txtDesc;
    private ObjectField _objectSprite;
    [MenuItem("GGM/CreateChar")]
    public static void ShowExample()
    {
        CreateChar wnd = GetWindow<CreateChar>();
        wnd.titleContent = new GUIContent("정호야 내가 미안해 풀어줘 밍밍 ㅠㅠ");
    }
    [OnOpenAsset]
    public static bool OnOpenAsset(int instanceID,int line)
    {
        if(Selection.activeObject is CharacterSO)
        {
            ShowExample();
            
            return true;
        }
        return false;
    }

    public void CreateGUI()
    {
        VisualElement root = rootVisualElement;
        VisualElement container = m_VisualTreeAsset.Instantiate();
        container.style.flexGrow = 1;
        root.Add(container);

        _txtName = container.Q<TextField>("txt-name");
        _txtDesc = container.Q<TextField>("txt-desc");
        _objectSprite = container.Q<ObjectField>("object-sprite");
        container.Q<Button>("btn-create").RegisterCallback<ClickEvent>(CreateSO);
        OnSelectionChange();

    }

    private void CreateSO(ClickEvent evt)
    {
        string charname = _txtName.value;
        string filename = $"Assets/08_SO/CharacterSO/{charname}.asset";
        CharacterSO asset = AssetDatabase.LoadAssetAtPath<CharacterSO>(filename);

        if (asset != null)
        {

            asset.charname = _txtName.value;
            asset.description = _txtDesc.value;
            asset.sprite = _objectSprite.value as Sprite;
            EditorUtility.SetDirty(asset);//디스크에 저장
            AssetDatabase.SaveAssets();//유니티 메모리에 저장
        }
        else
        {
            asset = ScriptableObject.CreateInstance<CharacterSO>();

            asset.charname = _txtName.value;
            asset.description = _txtDesc.value;
            asset.sprite = _objectSprite.value as Sprite;

            string assetPath = AssetDatabase.GenerateUniqueAssetPath($"Assets/08_SO/CharacterSO/{asset.charname}.asset");
            AssetDatabase.CreateAsset(asset, filename);
            AssetDatabase.Refresh();
        }

    }
    private void OnSelectionChange()
    {
        var so =Selection.activeObject as CharacterSO;
        if (so != null)
        {
            _txtName.value = so.name;
            _txtDesc.value = so.description;
            _objectSprite.value = so.sprite;
        }
        else
        {

        }
    }


}
