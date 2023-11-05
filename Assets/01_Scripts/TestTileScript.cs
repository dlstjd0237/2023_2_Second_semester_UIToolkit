using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class TestTileScript : MonoBehaviour
{
    [SerializeField] private Tilemap _tilemap;
    [SerializeField] private TileBase _floorTile; //타일맵에 찍히는 애들
    private void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mouseWorldPos.z = 0;

                Vector3Int tilePos = _tilemap.WorldToCell(mouseWorldPos);

                //이 위치에 바닥 타일을 깐다.
                _tilemap.SetTile(tilePos, _floorTile);
            }
    }
}
