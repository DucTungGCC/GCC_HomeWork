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

    private void Start()
    {
        for (int i = 0; i < gridSize.y; i++)
        {
            for (int j = 0; j < gridSize.x; j++)
            {
                Instantiate(Block,new Vector3(i * cellSize.x + cellSize.x/2, j * cellSize.y + cellSize.y /2, 0), Quaternion.Inverse(cam.transform.rotation), this.transform);
            }
        }
    }

    void Update()
    {
        cam.gameObject.transform.position = new Vector3((float)gridSize.x * cellSize.x / 2, (float)gridSize.y * cellSize.y / 2, cam.transform.position.z);
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
