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
    private bool isRunning = false;
    private void Start()
    {
        for(int i = 0; i < Block.Count; i++)
            Block[i].SetSecondPos(_targets[i].position);
            
    }

    IEnumerator Expand()
    {
        for (int i = 0; i < Block.Count; i++)
        {
            Block[i].Move();
        }

        yield return new WaitForSeconds(6f);
        WaitForSeconds wait = new WaitForSeconds(0.5f);
        for (int i = 0; i < Block.Count; i++)
        {
            Block[i].ExpandBlock();
            yield return wait;
        }
        yield return new WaitForSeconds(0.6f);
        for (int i = 0; i < Block.Count; i++)
        {
            Block[i].Move();
        }
        yield return new WaitForSeconds(3.1f);
        isRunning = false;
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {

        if (!isRunning)
        {
            StartCoroutine(Expand());
            isRunning = true;
        }
    }
}
