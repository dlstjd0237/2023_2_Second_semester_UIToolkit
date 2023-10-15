using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;

    private void Awake()
    {
        _inputReader.AttackEvent += OnAttack;
    }

    private void OnAttack(bool obj)
    {
        Debug.Log(obj);
    }
}
