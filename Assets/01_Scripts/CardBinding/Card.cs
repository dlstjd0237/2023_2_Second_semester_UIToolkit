using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
public class Card 
{
    private Character _character;
    private VisualElement _cardRoot;

    public VisualElement Root => _cardRoot;

    private Label _nameLabel;
    private Label _descLabel;
    private VisualElement _profileImage;

    public Card(VisualElement cardRoot, Character character)
    {
        _character = character;
        _cardRoot = cardRoot;

        _nameLabel = _cardRoot.Q<Label>("name-label");
        _descLabel = _cardRoot.Q<Label>("info-label");
        _profileImage = _cardRoot.Q<VisualElement>("image");

        _character.OnChanged += UpdateInfo;
        UpdateInfo();
    }

    private void UpdateInfo()
    {
        _nameLabel.text = _character.Name;
        _descLabel.text = _character.Description;
        _profileImage.style.backgroundImage = new StyleBackground(_character.Sprite);
    }
}
