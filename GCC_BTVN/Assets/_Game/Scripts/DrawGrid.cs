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
    private Vector3 mousePosition;
    
    void Update()
    {
        cam.gameObject.transform.position = new Vector3((float)gridSize.x * cellSize.x / 2, (float)gridSize.y * cellSize.y / 2, cam.transform.position.z);
        if (Input.GetMouseButtonDown(0))
        {
            mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
            Vector2Int blockPos = new Vector2Int((int)(mousePosition.x / cellSize.x), (int)(mousePosition.y / cellSize.y));
            Collider2D hit = Physics2D.OverlapPoint(new Vector3(blockPos.x * cellSize.x, blockPos.y * cellSize.y, 0));
            if(hit == null)
            {
                if (blockPos.x >= 0 && blockPos.y >= 0 && blockPos.x < gridSize.x && blockPos.y < gridSize.y)
                {
                    Instantiate(Block,
                        new Vector3(blockPos.x * cellSize.x + cellSize.x / 2, blockPos.y * cellSize.y + cellSize.y / 2),
                        Quaternion.identity);
                }
            }
            else
            {
                hit.gameObject.SetActive(false);
            }
        }
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        for (int i = 0; i < gridSize.y; i++)
        {
            for (int j = 0; j < gridSize.x; j++)
            {
                Gizmos.DrawWireCube(new Vector2(i * cellSize.x + cellSize.x/2, j * cellSize.y + cellSize.y /2), cellSize);
            }
        }
    }


}
