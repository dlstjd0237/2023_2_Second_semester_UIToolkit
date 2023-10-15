using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
public class CardBinding : MonoBehaviour
{
    private UIDocument _doc;

    private VisualElement _contentBox;

    private TextField _txtName;
    private TextField _txtDesc;

    public List<CharacterSO> _charSOList = new();

    private List<Card> _cardList = new();
    private Character _currentCharacter = null;
    [SerializeField] private Sprite _defaultSprit;

    [SerializeField] private VisualTreeAsset _cardTemplate;

    private void Awake()
    {
        _doc = GetComponent<UIDocument>();

    }


    private  void OnEnable()
    {

        var root = _doc.rootVisualElement;

        _txtName = root.Q<TextField>("txt-name");
        _txtDesc = root.Q<TextField>("txt-desc");

        _contentBox = root.Q<VisualElement>("content");
        root.Q<Button>("btn-add-card").RegisterCallback<ClickEvent>(AddCard);
        root.Q<Button>("btn-show-all").RegisterCallback<ClickEvent>(e=> {

            qwer();
            
        }
        );
      

        _txtName.RegisterCallback<ChangeEvent<string>>(OnNameChanged);
        _txtDesc.RegisterCallback<ChangeEvent<string>>(OnDescChanged);

      
    }
    private async void qwer()
    {
        for (int i = 0; i < 5; i++)
        {
            AddCard(_charSOList[i]);
            await Task.Delay(100);
        }
    }

    private void OnNameChanged(ChangeEvent<string> evt)
    {
        if (_currentCharacter is null) return;
        _currentCharacter.Name = evt.newValue;
    }

    private void OnDescChanged(ChangeEvent<string> evt)
    {
        if (_currentCharacter is null) return;
        _currentCharacter.Description = evt.newValue;
    }


    private async void AddCard(ClickEvent evt)
    {
        var template = _cardTemplate.Instantiate().Q<VisualElement>("card-border");

        string name = _txtName.value;
        string desc = _txtDesc.value;

        template.Q<Label>("name-label").text = name;
        template.Q<Label>("info-label").text = desc;

        Character character = new Character(name, desc, _defaultSprit);
        Card card = new Card(template, character);

        _cardList.Add(card);


        template.RegisterCallback<ClickEvent>(e =>
        {
            _currentCharacter = character;
            _txtName.SetValueWithoutNotify(character.Name);
            _txtDesc.SetValueWithoutNotify(character.Description);
        });

        _contentBox.Add(template);
        await Task.Delay(100);
        template.AddToClassList("on");
    }
    private async void AddCard( CharacterSO character)
    {
        var template = _cardTemplate.Instantiate().Q<VisualElement>("card-border");

        string name = character.name;
        string desc = character.description;

        template.Q<Label>("name-label").text = name;
        template.Q<Label>("info-label").text = desc;



        template.RegisterCallback<ClickEvent>(e =>
        {
            _txtName.SetValueWithoutNotify(character.name);
            _txtDesc.SetValueWithoutNotify(character.description);
        });

        _contentBox.Add(template);
        await Task.Delay(100);
        template.AddToClassList("on");
    }


}


