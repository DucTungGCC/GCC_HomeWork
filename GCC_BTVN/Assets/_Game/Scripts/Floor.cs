using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    //[SerializeField] private Rigidbody2D _rb;
    [SerializeField] private Transform _tf;
    [SerializeField] private AnimationCurve _curve;
    private Vector3 firstPos, secondPos;
    private bool isFirst = true;
    Coroutine coroutine;
    public float shrinkDuration, currentShrinkDuration;
    public float minScale = 1, maxScale = 2;
    void Start()
    {
        firstPos = _tf.position;
    }
    
    
    
    IEnumerator Movement(Vector3 target)
    {
        isFirst = !isFirst;
        Vector3 startPos = _tf.position;
        Debug.Log("Start");
        float T = 0;
        while (_tf.position != target)
        {
            T +=  Time.deltaTime;
            //Debug.Log(T);
            _tf.position = Vector3.Lerp(startPos, target, T/3);
            yield return null;
            // if (Vector3.Distance(_tf.position, target) <= 0.1f)
            // {
            //     _tf.position = target;   
            //     break;
            // }
        }
        coroutine = null;
        if(!isFirst)
            yield return new WaitForSeconds(3f);
        
        
        // T = 0;
        // while (true)
        // {
        //     T +=  Time.deltaTime;
        //     //Debug.Log(T);
        //     _tf.position = Vector3.Lerp(target, startPos, T/3);
        //     yield return null;
        //     if (Vector3.Distance(_tf.position, startPos) <= 0.1f)
        //     {
        //         _tf.position = startPos;   
        //         break;
        //     }
        // }
        // isFirst = false;
        Debug.Log("End");
    }
    
    public void Move()
    {
        if (isFirst)
        {
            if (coroutine == null)
            {
                coroutine = StartCoroutine(Movement(secondPos)); 
            }
        }
        else
        {
            if (coroutine == null)
            {
                coroutine = StartCoroutine(Movement(firstPos)); 
            }
        }
    }

    public void ExpandBlock()
    {
        //Debug.Log("Expend");
        if (coroutine == null)
        {
            coroutine = StartCoroutine(Expand());
        }
    }
    
    private IEnumerator Expand()
    {
        Debug.Log("Expend");
        currentShrinkDuration = 0;
        while (currentShrinkDuration <= shrinkDuration)
        {
            currentShrinkDuration += Time.deltaTime;
            float currentScale = Mathf.Lerp(minScale, maxScale, _curve.Evaluate(currentShrinkDuration / shrinkDuration));
            _tf.localScale = Vector3.one * currentScale;
            yield return null;
        }
        coroutine = null;
        //yield return new WaitForSeconds(3f);
    }

    public void SetSecondPos(Vector3 set)
    {
        secondPos = set;
    }
}
