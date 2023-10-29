using System;
using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UIElements;
using Unity.EditorCoroutines.Editor;
using System.IO;

public class SpreadSheetLoader : EditorWindow
{
    [SerializeField]
    private VisualTreeAsset m_VisualTreeAsset = default;

    private string _documentID = "12gnUB7ONT4BGoH0Hvj4Z9mDRlNj8UDb7AXcvqGWCnxg";
    private Label _statusLabel;
    private VisualElement _loadingIcon;

    [MenuItem("Window/Utility/SpreadSheetLoader")]
    public static void OpenWindow()
    {
        SpreadSheetLoader wnd = GetWindow<SpreadSheetLoader>();
        wnd.titleContent = new GUIContent("SpreadSheetLoader");
    }

    public void CreateGUI()
    {
        VisualElement root = rootVisualElement;
        VisualElement templateContainer = m_VisualTreeAsset.Instantiate();
        templateContainer.style.flexGrow = 1;
        root.Add(templateContainer);
        _statusLabel = root.Q<Label>("status-label");
        _loadingIcon = root.Q<VisualElement>("loading-icon");


        Setup();
    }

    private void Setup()
    {
        TextField txtUrl = rootVisualElement.Q<TextField>("txt-url");
        txtUrl.RegisterCallback<ChangeEvent<string>>(HandleUrlChange);
        txtUrl.SetValueWithoutNotify(_documentID);

        Button loadBtn = rootVisualElement.Q<Button>("btn-load");
        loadBtn.RegisterCallback<ClickEvent>(HandleLoadBtn);
    }

    private void HandleLoadBtn(ClickEvent evt)
    {
        EditorCoroutineUtility.StartCoroutine(GetDataFromSheet("0", (dataArr) =>
        {
            CreateSO(name: dataArr[0], dex: int.Parse(dataArr[1]), str: int.Parse(dataArr[2]), hp: int.Parse(dataArr[3]), wis: int.Parse(dataArr[4]));
        }), this);
        EditorCoroutineUtility.StartCoroutine(GetDataFromSheet("413988979", (dataArr) =>
        {
            CreateSourceCode(dataArr[0], dataArr[1], dataArr[2], dataArr[3]);
        }), this);
    }

    private IEnumerator GetDataFromSheet(string sheetID, Action<string[]> Preocess)
    {
        UnityWebRequest req = UnityWebRequest.Get($"https://docs.google.com/spreadsheets/d/{_documentID}/export?format=tsv&gid={sheetID}");

        _statusLabel.text = "������ �ε���";
        _loadingIcon.RemoveFromClassList("off");


        yield return req.SendWebRequest();
        if (req.result == UnityWebRequest.Result.ConnectionError || req.responseCode != 200)
        {
            Debug.LogError("Error : " + req.responseCode);
            yield break;
        }

        _loadingIcon.AddToClassList("off");

        string resText = req.downloadHandler.text;
        string[] lines = resText.Split("\n");
        int lineNumber = 1;
        try
        {
            for (lineNumber = 1; lineNumber < lines.Length; ++lineNumber)
            {
                string[] dataArr = lines[lineNumber].Split("\t");
                Preocess?.Invoke(dataArr);
            }

        }
        catch (Exception e)
        {
            _statusLabel.text += $"\n {_documentID} �ε� �� ���� �߻�";
            _statusLabel.text += $"\n {lineNumber: �� ���� �߻�}";
            _statusLabel.text += $"\n {e.Message}";
        }

        _statusLabel.text = $"\n �ε�Ϸ�! {lineNumber - 1} ���� ������ ���������� �ۼ���.";


        AssetDatabase.SaveAssets();//����Ƽ �޸𸮿� ����
        AssetDatabase.Refresh();
    }

    private void HandleUrlChange(ChangeEvent<string> evt)
    {
        _documentID = evt.newValue;
    }

    private void CreateSO(string name, int dex, int str, int hp, int wis)
    {
        string filePath = $"Assets/08_SO/StatSO/{name}.asset";
        StatSO asset = AssetDatabase.LoadAssetAtPath<StatSO>($"Assets/08_SO/StatSO/{name}.asset");

        if (asset == null)
        {
            asset = ScriptableObject.CreateInstance<StatSO>();
            asset.charName = name;
            asset.dex = dex;
            asset.str = str;
            asset.hp = hp;
            asset.wis = wis;
            string filename = AssetDatabase.GenerateUniqueAssetPath(filePath);
            AssetDatabase.CreateAsset(asset, filename);
        }
        else
        {
            asset.charName = name;
            asset.dex = dex;
            asset.str = str;
            asset.hp = hp;
            asset.wis = wis;
            EditorUtility.SetDirty(asset);
        }


    }

    private void CreateSourceCode(string name, string className, string speed, string type)
    {
        string code = string.Format(MovementCodeFormat.MovementFormat, name, className, speed, type);
        string path = $"{Application.dataPath}/01_Scripts/";
        File.WriteAllText($"{path}/{className}.cs", code);
    }
}


