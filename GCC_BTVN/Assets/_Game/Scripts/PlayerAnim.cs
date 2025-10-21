using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerAnim : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private PlayerAttack _playerAttack;
    [SerializeField] private Rigidbody2D _rb;
    
    public bool isRunning = false;
    public bool isJumping = false;
    public float JumpState = 0;
    public bool isAttacking = false;
    private void Update()
    {
        UpdateState();
        Movement();
    }

    private void UpdateState()
    {
        isRunning = _playerMovement._moveDirection != 0;
        isJumping = !_playerMovement.isGrounded();
        JumpState = _rb.velocity.y;
        isAttacking = _playerAttack.isAttacking;
    }

    private void Movement()
    {
        _animator.SetBool("isRunning", isRunning);
        _animator.SetFloat("velocityY", JumpState);
        _animator.SetBool("isJumping", isJumping);
    }
    

    public void Attack()
    {
        _animator.SetTrigger("isAttacking");
    }
}
