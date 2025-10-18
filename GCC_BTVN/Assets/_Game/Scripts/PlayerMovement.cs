using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    InputAction inputMovement;
    InputAction inputJump;
    [SerializeField] private float _speed = 8f;
    [SerializeField] private float _jumpForce = 12f;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private GameObject Grounded;
    [SerializeField] private SpriteRenderer _sr;
    [SerializeField] private LayerMask layerGrounded;
    [SerializeField] private float bufferJump = 0;
    [SerializeField] private float caiyoteJump = 0;
    private float _moveDirection;
    private void Start()
    {
        inputMovement = InputSystem.actions.FindAction("Horizontal");
        inputJump = InputSystem.actions.FindAction("Jump");
    }

    private void Update()
    {
        Movement();
        Jump();
    }

    void Movement()
    {
        _moveDirection = inputMovement.ReadValue<float>();
        _rb.velocity = new Vector2(_moveDirection * _speed, _rb.velocity.y);
        if (_moveDirection > 0)
        {
            _sr.flipX = false;
        }
        else if (_moveDirection < 0)
        {
            _sr.flipX = true;
        }
        
    }

    bool isGrounded()
    {
        return Physics2D.OverlapCapsule(Grounded.transform.position, new Vector2(0.4f, 0.12f),
            CapsuleDirection2D.Horizontal, 0f, layerGrounded);
    }

    void jumpStatus() // Ham cap nhat cac trang thai cua nhay
    {
        // Jump Buffer : Khi nguoi chs lo an nhay som thi khi cham dat van trong thoi gian delta time thi van nhay
        if (bufferJump > 0)
        {
            bufferJump -= Time.deltaTime;
        }
        if (inputJump.WasPressedThisFrame())
        {
            bufferJump = 0.1f;
        }
        
        // Jump Caiyote : Khi nguoi chs lo chay qua mep cua grounded 1 xiu van co the nhay
        if (isGrounded())
        {
            caiyoteJump = 0.1f;
        }

        if (!isGrounded())
        {
            caiyoteJump -= Time.deltaTime;
        }
    }
    
    void Jump()
    {
        jumpStatus();
        if (CanJump())
        {
            _rb.velocity = new Vector2(_rb.velocity.x, _jumpForce);
            bufferJump = 0;
            caiyoteJump = 0;
        }
        if (!isGrounded() && inputJump.WasReleasedThisFrame() && _rb.velocity.y > 0)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, 0.4f * _rb.velocity.y);
        }
        JumpCurve();
    }

    bool CanJump()
    {
        return ((isGrounded() && bufferJump > 0) || (!isGrounded() && caiyoteJump > 0 && inputJump.WasPressedThisFrame()));
    }

    void JumpCurve()
    {
        if (_rb.velocity.y < 0)
        {
            _rb.gravityScale = 4f;
        }
        else
        {
            _rb.gravityScale = 3f;
        }
    }
}
