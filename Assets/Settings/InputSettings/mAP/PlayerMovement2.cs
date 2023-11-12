using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement2 : MonoBehaviour
{
    [SerializeField] private InputReader2 _inputReader;
    [SerializeField] private float _moveSpeed;

    private PlayerAnimator _animator;
    private Transform _visualTrm;
    private Rigidbody2D _rigidbody;
    private Vector2 _movementInput;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _visualTrm = transform.Find("Visual");
        _animator = _visualTrm.GetComponent<PlayerAnimator>();

        _inputReader.MovementEvent += HandleMovement;
        _inputReader.ChangeEvent += HandleChange;
    }

    private void HandleChange()
    {
        _animator.SetNextSprite();//체인지 버튼 누루면 다음 스프라이트로
    }

    private void OnDestroy()
    {
        _inputReader.MovementEvent -= HandleMovement;
        _inputReader.ChangeEvent -= HandleChange;

    }

    private void HandleMovement(Vector2 obj)
    {
        _movementInput = obj;
    }

    private void FixedUpdate()
    {
        Move();
        CheckFlip();
    }

    private void Move()
    {
        _rigidbody.velocity = _movementInput * _moveSpeed;
        _animator.SetMovement(_movementInput.sqrMagnitude > 0.01f);
        //애니메이션 알잘딱 적용
    }
    private void CheckFlip()
    {
        if (Mathf.Abs(_movementInput.x) > 0)
        {
            Vector3 localScale = _visualTrm.localScale;

            if ((_movementInput.x < 0 && localScale.x > 0) || (_movementInput.x > 0 && localScale.x < 0))
            {
                localScale.x *= -1;
                _visualTrm.localScale = localScale;
            }
        }
    }


}
