using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class TestTileScript : MonoBehaviour
{
    [SerializeField] private Tilemap _tilemap;
    [SerializeField] private TileBase _floorTile; //Ÿ�ϸʿ� ������ �ֵ�
    private void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mouseWorldPos.z = 0;

                Vector3Int tilePos = _tilemap.WorldToCell(mouseWorldPos);

                //�� ��ġ�� �ٴ� Ÿ���� ���.
                _tilemap.SetTile(tilePos, _floorTile);
            }
    }
}
