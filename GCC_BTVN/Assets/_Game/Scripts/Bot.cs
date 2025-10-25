using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot : MonoBehaviour
{
    [SerializeField] private MessNPC controller; // Tat bat chat
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private Transform _tf=> transform;
    [SerializeField] private float attackForce = 2f;
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

    public void OnAttack(Vector2 OtherAttacker)
    {
        Debug.Log("Toi bi ngu");
        _rb.AddForce(((- OtherAttacker + (Vector2)(_tf.position)).normalized + Vector2.up * 0.5f) * attackForce, ForceMode2D.Impulse);
    }
}
