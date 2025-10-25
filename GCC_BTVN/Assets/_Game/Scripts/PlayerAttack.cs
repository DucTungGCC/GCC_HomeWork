using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    InputAction _inputAttack;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Collider2D attackRange;
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private Transform _tf;
    //[SerializeField] private LayerMask _layerMask;
    [SerializeField] private UnityEvent OnAttack;
    //[SerializeField] private Bot bot;
    //public float attackRange = 2f;
    //public float attackForce = 2f;
    //private Bot bot;
    public float attackDirection = 1;
    //private Rigidbody2D targetRb = null;
    private bool canAttack = true;
    public bool isAttacking = false;
    void Start()
    {
        _inputAttack = InputSystem.actions.FindAction("Attack");
        attackRange.enabled = false;
    }
    private void Update()
    {
        attackDirection = _spriteRenderer.flipX ? -1f : 1f;
        if (_inputAttack.WasPressedThisFrame() && canAttack)
        {
            OnAttack.Invoke();
            isAttacking = true;
            Attack();
            canAttack = false;
            //Debug.Log("dang tan cong");
            StartCoroutine(CooldownAttack());
        }
    }

    IEnumerator CooldownAttack()
    {
        yield return new WaitForSeconds(0.5f);
        canAttack = true;
        isAttacking = false;
        attackRange.enabled = false;
    }
    public void Attack()
    {
        attackRange.enabled = true;
        ((BoxCollider2D)attackRange).offset = new Vector2(attackDirection, 0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Bot bot = other.gameObject.GetComponent<Bot>();
        if (bot != null)
        {
            bot.OnAttack(new Vector2(_tf.position.x, _tf.position.y));
        }
    }
}
