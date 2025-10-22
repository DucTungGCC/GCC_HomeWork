using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanGat : MonoBehaviour
{
    //[SerializeField] private GameObject[] Block = new GameObject[4];
    //[SerializeField] private Rigidbody2D[] _rb;
    [SerializeField] private List<Transform> _targets = new List<Transform>();
    [SerializeField] private List<Floor> Block = new List<Floor>();

    private void Start()
    {
        for(int i = 0; i < Block.Count; i++)
            Block[i].SetSecondPos(_targets[i].position);
            
    }
    
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        for (int i = 0; i < Block.Count; i++)
        {
            Block[i].Move();
        }
    }
}
