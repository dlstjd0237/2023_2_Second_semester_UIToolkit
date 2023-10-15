using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerTest : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;

    private void Update()
    {
        if (Keyboard.current.kKey.wasPressedThisFrame)
        {
            var control = _inputReader.GetControls();
            control.Player.Disable(); //Ű���� �ϴ°� �غ����
            // Ű ����ÿ��� �ݵ�� �ش� ��ǲ���� disable����� ��

            Debug.Log("������ ���ϴ� Ű�� �Է��ϼ���.");
            control.Player.Jump.PerformInteractiveRebinding()
                .WithControlsExcluding("Mouse")
                .WithCancelingThrough("<Keyboard>/escape")//ESCŰ�� ���
                .OnComplete(op =>
                {
                    Debug.Log("����Ǿ����ϴ�.");
                    control.Player.Enable();
                }).OnCancel(op =>
                {
                    Debug.Log("��ҵǾ����ϴ�.");
                    op.Dispose();
                    control.Player.Enable();
                }).Start();
        }
        if (Keyboard.current.pKey.wasPressedThisFrame)
        {
           var json = _inputReader.GetControls().SaveBindingOverridesAsJson();
            Debug.Log(json);
        }
    }

}
