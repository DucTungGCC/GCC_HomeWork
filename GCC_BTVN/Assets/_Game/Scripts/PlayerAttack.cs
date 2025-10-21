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
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private UnityEvent OnAttack;
    public float attackRange = 2f;
    public float attackForce = 2f;
    public int attackDirection = 1;
    private Rigidbody2D targetRb = null;
    private bool canAttack = true;
    public bool isAttacking = false;
    void Start()
    {
        _inputAttack = InputSystem.actions.FindAction("Attack");
    }
    private void Update()
    {
        attackDirection = _spriteRenderer.flipX ? -1 : 1;
        if (_inputAttack.WasPressedThisFrame() && canAttack)
        {
            OnAttack.Invoke();
            isAttacking = true;
            Attack();
            canAttack = false;
            Debug.Log("dang tan cong");
            StartCoroutine(CooldownAttack());
        }
    }

    IEnumerator CooldownAttack()
    {
        yield return new WaitForSeconds(0.5f);
        canAttack = true;
        isAttacking = false;
    }
    void Attack()
    {
        
        Debug.DrawRay(_rigidbody2D.transform.position, new Vector2(attackDirection, 0) * attackRange, Color.red, 0.5f);
        RaycastHit2D hit = Physics2D.Raycast(_rigidbody2D.transform.position, new Vector2(attackDirection, 0), attackRange, _layerMask);
        if (hit.collider != null)
        {
            targetRb = hit.collider.attachedRigidbody;
            targetRb.AddForce((new Vector2(attackDirection, 0) + Vector2.up * 0.5f) * attackForce, ForceMode2D.Impulse);
        }
    }
    
}
