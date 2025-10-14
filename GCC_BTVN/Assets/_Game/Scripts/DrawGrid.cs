using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class DrawGrid : MonoBehaviour
{
    [SerializeField] private Vector2Int gridSize;
    [SerializeField] private Vector2 cellSize;
    [SerializeField] private Camera cam;
    [SerializeField] private GameObject Block;
    private Vector3 _mousePosition;

    void Start()
    {
        Block.transform.localScale = new Vector3(cellSize.x, cellSize.y, cellSize.x);
    }
    void Update()
    {
        cam.gameObject.transform.position = new Vector3((float)gridSize.x * cellSize.x / 2, (float)gridSize.y * cellSize.y / 2, -10);
        if (Input.GetMouseButtonDown(0))
        {
            MakeChoose();
        }
    }

    private void MakeChoose()
    {
        // Lay toa do the gioi
        _mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
        
        // Lấy tọa độ của x và y
        int x = Mathf.FloorToInt(_mousePosition.x / cellSize.x);
        int y = Mathf.FloorToInt(_mousePosition.y / cellSize.y);
        Vector2Int blockPos = new Vector2Int(x, y);
        
        // Debug blockPos
        Debug.Log(blockPos.ToString());
        
        // Kiem tra va Spawn/Destroy block neu co/chua co block tai vi tri bam chuot
        Collider2D hit = Physics2D.OverlapPoint(new Vector3(blockPos.x * cellSize.x + cellSize.x / 2, blockPos.y * cellSize.y+ cellSize.y / 2, 0));
        if(hit == null)
        {
            if (blockPos.x >= 0 && blockPos.y >= 0 && blockPos.x < gridSize.x && blockPos.y < gridSize.y)
            {
                Instantiate(Block, new Vector3(blockPos.x * cellSize.x + cellSize.x / 2, blockPos.y * cellSize.y + cellSize.y / 2), Quaternion.identity, this.transform);
            }
        }
        else
        {
            Destroy(hit.gameObject);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        for (int i = 0; i < gridSize.y; i++)
        {
            for (int j = 0; j < gridSize.x; j++)
            {
                Gizmos.DrawWireCube(new Vector2(j * cellSize.x + cellSize.x/2, i * cellSize.y + cellSize.y /2), cellSize);
            }
        }
    }


}
