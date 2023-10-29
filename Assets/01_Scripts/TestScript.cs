using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ToolkitMessageSystem;
using UnityEngine.InputSystem;

public class TestScript : MonoBehaviour
{
    private void Update()
    {
        if (Keyboard.current.aKey.wasPressedThisFrame)
        {
            MessageHub.OnMessage?.Invoke("테스트 메세지",MessageColor.Red);
        }
    }
}
