using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot : MonoBehaviour
{
    [SerializeField] private MessNPC controller; // Tat bat chat
    [SerializeField] private Rigidbody2D _rb;

    private void Awake()
    {
        if(_rb == null)
            _rb = GetComponent<Rigidbody2D>();
    }

    
    IEnumerator Die()
    {
        yield return new WaitForSeconds(5f);
        _rb.gameObject.SetActive(false);
    }
    private void Update()
    {
        if (_rb.velocity.magnitude > 0.3f)
        {
            controller.TakeDamage();
            StartCoroutine(Die());
        }
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
}
