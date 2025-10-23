using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDance : MonoBehaviour
{
    [SerializeField] private AnimationCurve curve;
    //[SerializeField] private float duration;
    [SerializeField] private Transform _tf;
    //private float minScale = 1, maxScale = 2;
    public float ShrinkDuration; // Thoi gian hoat canh
    public float currentShrinkTime; // Thoi gian hoat canh hien tai
    private Coroutine _coroutine;

    private void Update()
    {
        if (_coroutine == null)
            _coroutine = StartCoroutine(Expand());
    }

    IEnumerator Expand()
    {
        currentShrinkTime = 0;
        while (currentShrinkTime <= ShrinkDuration)
        {
            currentShrinkTime += Time.deltaTime;
            float currentScale = Mathf.Lerp(1, 2, curve.Evaluate(currentShrinkTime / ShrinkDuration));
            _tf.localScale = Vector3.one * currentScale;
            yield return null;
        }
        _coroutine = null;
    }
}
