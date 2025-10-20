using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    InputAction _inputAttack;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private LayerMask _layerMask;
    public float attackRange = 2f;
    public float attackForce = 2f;
    public int attackDirection = 1;
    private Rigidbody2D targetRb = null;
    private bool canAttack = true;
    void Start()
    {
        _inputAttack = InputSystem.actions.FindAction("Attack");
    }
    private void Update()
    {
        attackDirection = _spriteRenderer.flipX ? -1 : 1;
        if (_inputAttack.WasPressedThisFrame() && canAttack)
        {
            Attack();
            canAttack = false;
            Debug.Log("dang tan cong");
            StartCoroutine(CountdownAttack());
        }
    }

    IEnumerator CountdownAttack()
    {
        yield return new WaitForSeconds(0.5f);
        canAttack = true;
    }
    void Attack()
    {
        Debug.DrawRay(_rigidbody2D.transform.position, new Vector2(attackDirection, 0) * attackRange, Color.red, 0.5f);
        RaycastHit2D hit = Physics2D.Raycast(_rigidbody2D.transform.position, new Vector2(attackDirection, 0), attackRange, _layerMask);
        if (hit.collider != null)
        {
            targetRb = hit.collider.attachedRigidbody;
            targetRb.AddForce(new Vector2(attackDirection , 0) * attackForce, ForceMode2D.Impulse);
        }
    }
    
}
