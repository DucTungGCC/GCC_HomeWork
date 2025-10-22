using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    //[SerializeField] private Rigidbody2D _rb;
    [SerializeField] private Transform _tf;
    private Vector3 firstPos, secondPos;
    private bool isRunning = false;
    Coroutine coroutine;
    void Start()
    {
        firstPos = _tf.position;
    }
    
    
    
    IEnumerator Movement(Vector3 target)
    {
        isRunning = true;
        Vector3 startPos = _tf.position;
        Debug.Log("Start");
        float T = 0;
        while (true)
        {
            T +=  Time.deltaTime;
            //Debug.Log(T);
            _tf.position = Vector3.Lerp(startPos, target, T/3);
            yield return null;
            if (Vector3.Distance(_tf.position, target) <= 0.1f)
            {
                _tf.position = target;   
                break;
            }
        }
        yield return new WaitForSeconds(5f);
        coroutine = null;
        T = 0;
        while (true)
        {
            T +=  Time.deltaTime;
            //Debug.Log(T);
            _tf.position = Vector3.Lerp(target, startPos, T/3);
            yield return null;
            if (Vector3.Distance(_tf.position, startPos) <= 0.1f)
            {
                _tf.position = startPos;   
                break;
            }
        }
        isRunning = false;
        Debug.Log("End");
    }
    
    public void Move()
    {
        if (!isRunning)
        {
            if (coroutine == null)
            {
                coroutine = StartCoroutine(Movement(secondPos)); 
            }
        }
        // else
        // {
        //     if (coroutine == null)
        //     {
        //         coroutine = StartCoroutine(Movement(firstPos)); 
        //     }
        // }
    }

    public void SetSecondPos(Vector3 set)
    {
        secondPos = set;
    }
}
