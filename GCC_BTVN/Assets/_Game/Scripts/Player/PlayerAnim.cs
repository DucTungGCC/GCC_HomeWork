using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
public enum AnimState {
    Idle,
    Run,
    Jump,
    Attack
}
public class PlayerAnim : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private PlayerAttack _playerAttack;
    [SerializeField] private Rigidbody2D _rb;

    const string Anim_IDLE = "idle";
    const string Anim_RUN = "run";
    const string Anim_JUMP = "jump";
    const string Anim_ATTACK = "attack";
    const string Anim_JUMPSTATE = "velocityY";
    string currentAnim = Anim_IDLE;
    private AnimState currState = AnimState.Idle;
    private float JumpState = 0f;
    
    private void LateUpdate()
    {
        CallAnim();
    }

    private void Update()
    {
        UpdateState();
        JumpState = _rb.velocity.y;
    }

    private void UpdateState()
    {
        if (_playerMovement._moveDirection != 0 || (_rb.velocity.y != 0 && _rb.velocity.x != 0))
        {
            currState = AnimState.Idle;
            //ChangeAnim(Anim_IDLE);
        }

        if (_rb.velocity.x != 0 && _playerMovement.isGrounded())
        {
            currState = AnimState.Run;
            //ChangeAnim(Anim_RUN);
        }
        else if (!_playerMovement.isGrounded())
        {
            currState = AnimState.Jump;
        }
        else
        {
            currState = AnimState.Idle;
        }
        if (_playerAttack.isAttacking)
        {
            currState = AnimState.Attack;
            //ChangeAnim(Anim_ATTACK);
        }
    }

    private void CallAnim()
    {
        switch (currState)
        {
            case AnimState.Attack:
                ChangeAnim(Anim_ATTACK);
                break;
            case AnimState.Run:
                ChangeAnim(Anim_RUN);
                break;
            case AnimState.Jump:
                ChangeAnim(Anim_JUMP);
                break;
            default:
                ChangeAnim(Anim_IDLE);
                break;
        }
        
    }
    
    private void ChangeAnim(string newAnim)
    {
        if (newAnim == Anim_JUMP)
        {
            _animator.SetFloat(Anim_JUMPSTATE, JumpState);
        }
        if (currentAnim != newAnim)
        {
            _animator.ResetTrigger(currentAnim);
            currentAnim = newAnim;
            _animator.SetTrigger(currentAnim);
        }
    }
}
