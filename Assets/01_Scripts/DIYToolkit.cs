using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
public class DIYToolkit : MonoBehaviour
{
    [SerializeField]
    private WebcamDisplay _cam;
    [SerializeField] private Texture2D _god;


    private UIDocument _doc;
    private VisualElement _root;
    private VisualElement _slider;
    private VisualElement _option;

    private Button _homeBtn;
    private Button _findBtn;
    private Button _addBtn;
    private Button _greatBtn;
    private Button _myProfileBtn;

    private Button _heart1;
    private Button _heart2;
    private Button _heart3;
    private Button _heart4;
    private Button _heart5;

    private Button Qhasorl;

    private Button _option1;
    private Button _option2;
    private Button _option3;
    private Button _option4;
    private Button _option5;


    private void Awake()
    {




        _doc = GetComponent<UIDocument>();
        _root = _doc.rootVisualElement;

        _option = _root.Q<VisualElement>("option");

        _slider = _root.Q<VisualElement>("visual-slider");

        _homeBtn = _root.Q<Button>("btn-home");
        _homeBtn.RegisterCallback<ClickEvent>(e =>
        {
            _cam.gameObject.SetActive(false);
            _cam.CamStop();
            _slider.style.left = 0;
        });
        _findBtn = _root.Q<Button>("btn-find");
        _findBtn.RegisterCallback<ClickEvent>(e =>
        {
            _cam.gameObject.SetActive(false);
            _cam.CamStop();
            _slider.style.left = -1100;


        });
        _addBtn = _root.Q<Button>("btn-add");
        _addBtn.clicked += addBtn;


        _greatBtn = _root.Q<Button>("btn-great");
        _greatBtn.RegisterCallback<ClickEvent>(e =>
        {
            _cam.gameObject.SetActive(false);
            _cam.CamStop();
            _slider.style.left = -3300;
        });
        _myProfileBtn = _root.Q<Button>("btn-profile");
        _myProfileBtn.RegisterCallback<ClickEvent>(e =>
        {
            _cam.gameObject.SetActive(false);
            _cam.CamStop();
            _slider.style.left = -4400;
        });





        _heart1 = _root.Q<Button>("btn-heart-1");
        _heart1.RegisterCallback<ClickEvent>(e =>
        {
            _heart1.style.backgroundImage = _god;
        });
        _heart2 = _root.Q<Button>("btn-heart-2");
        _heart2.RegisterCallback<ClickEvent>(e =>
        {
            _heart2.style.backgroundImage = _god;
        });
        _heart3 = _root.Q<Button>("btn-heart-3");
        _heart3.RegisterCallback<ClickEvent>(e =>
        {
            _heart3.style.backgroundImage = _god;
        });
        _heart4 = _root.Q<Button>("btn-heart-4");
        _heart4.RegisterCallback<ClickEvent>(e =>
        {
            _heart4.style.backgroundImage = _god;
        });
        _heart5 = _root.Q<Button>("btn-heart-5");
        _heart5.RegisterCallback<ClickEvent>(e =>
        {
            _heart5.style.backgroundImage = _god;
        });

        Qhasorl = _root.Q<Button>("Qnasorl");
        Qhasorl.clicked += qwer;


        _option1 = _root.Q<Button>("btn-option1");
        _option1.RegisterCallback<ClickEvent>(e => {
            Debug.Log("±â¸ð¶ì");
            _option.AddToClassList("on"); }
        );


    }

    private void addBtn()
    {
        _slider.style.left = -2200;



    }
    private void qwer()
    {
        _cam.gameObject.SetActive(true);
        _cam.CamStart();
    }


}
