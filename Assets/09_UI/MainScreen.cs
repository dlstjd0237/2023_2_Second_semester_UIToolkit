using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
public class MainScreen : MonoBehaviour
{
    private UIDocument _doc;
    private VisualElement _root;

    private void Awake()
    {
        _doc = GetComponent<UIDocument>();
        _root = _doc.rootVisualElement;

        var slideBox = _root.Q("slide-box");


        _root.Q<Button>("home-btn").RegisterCallback<ClickEvent>(e =>
        {
            Debug.Log("Btn1");
            slideBox.style.left = 0;
        });
        _root.Q<Button>("inventory-btn").RegisterCallback<ClickEvent>(e =>
        {
            Debug.Log("Btn2");
            slideBox.style.left = -1920;
        });
        _root.Q<Button>("equip-btn").RegisterCallback<ClickEvent>(e =>
        {
            Debug.Log("Btn3");
            slideBox.style.left = -3840;
        });
        _root.Q<Button>("friend-btn").RegisterCallback<ClickEvent>(e =>
        {
            Debug.Log("Btn4");
            slideBox.style.left = -5760;
        });
        //_testBtn.clicked += Go;


    }

    private void Go(ClickEvent evt)
    {
        Debug.Log("±â¸ð¶ì");
    }

    private void Go()
    {
        Debug.Log("±â¸ð¶ì");
    }
}
