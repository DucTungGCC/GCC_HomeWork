using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyAttack : MonoBehaviour
{
    [SerializeField] GameObject _target;
    [SerializeField] Transform _tf;
    Coroutine _cor;
    IEnumerator Attacking()
    {
        _target.transform.SetParent(_tf);
        yield return new WaitForSeconds(1f);
        _target.transform.SetParent(null);
        yield return new WaitForSeconds(5f);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _target = other.gameObject;
            if (_cor != null)
            {
                _cor = StartCoroutine(Attacking());
            }
        }
    }

    private void OnDisable()
    {
        StopAllCoroutines();
        if(_target != null)
            _target.transform.SetParent(null);
    }
}
