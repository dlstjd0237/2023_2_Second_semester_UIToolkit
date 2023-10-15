using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    private Rigidbody2D _rig2D;
    [SerializeField] private float _moveSpeed = 5;
    private void Awake()
    {
        _rig2D = GetComponent<Rigidbody2D>();

        _inputReader.JumpEvent += JumpHandle;
        _inputReader.MovementEvent += MovementHandle;
    }
    private void OnDestroy()
    {
        _inputReader.JumpEvent -= JumpHandle;
        _inputReader.MovementEvent -= MovementHandle;
    }
    public void JumpHandle(bool value)
    {
        Debug.Log($"점프 핸들 {value}");
    }
    public void MovementHandle(Vector2 movement)
    {

        _rig2D.velocity = movement.normalized * _moveSpeed;
    }
}
