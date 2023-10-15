using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
public class CardBinding : MonoBehaviour
{
    private UIDocument _doc;

    private VisualElement _contentBox;

    private TextField _txtName;
    private TextField _txtDesc;


    [SerializeField] private VisualTreeAsset _cardTemplate;

    private void Awake()
    {
        _doc = GetComponent<UIDocument>();

    }


    private void OnEnable()
    {

        var root = _doc.rootVisualElement;

        _txtName = root.Q<TextField>("txt-name");
        _txtDesc = root.Q<TextField>("txt-desc");

        _contentBox = root.Q<VisualElement>("content");
        root.Q<Button>("add-btn").RegisterCallback<ClickEvent>(AddCard);
    }

    private void AddCard(ClickEvent evt)
    {
        var template = _cardTemplate.Instantiate();

        string name =_txtName.value;
        string desc = _txtDesc.value;

        template.Q<Label>("name-label").text = name;
        template.Q<Label>("info-label").text = desc;

        _contentBox.Add(template);
    }


}


