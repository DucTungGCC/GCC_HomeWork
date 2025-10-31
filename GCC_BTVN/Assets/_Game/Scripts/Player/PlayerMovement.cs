using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    InputAction _inputMovement;
    InputAction _inputJump;
    [SerializeField] private float _speed = 8f;
    [SerializeField] private float _jumpForce = 12f;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private GameObject Grounded;
    [SerializeField] private SpriteRenderer _sr;
    [SerializeField] private LayerMask layerGrounded;
    [SerializeField] private float bufferJump = 0;
    [SerializeField] private float caiyoteJump = 0;
    [SerializeField] private Animator _animator;
    public float _moveDirection;

    private void Awake()
    {
        if(_animator == null)
            _animator = GetComponent<Animator>();
        _inputMovement = InputSystem.actions.FindAction("Horizontal");
        _inputJump = InputSystem.actions.FindAction("Jump");
    }
    

    private void Update()
    {
        if (GameManager.isPaused) return;
        Movement();
        Jump();
    }
    
    #region MovementLogic
    void Movement()
    {
        _moveDirection = _inputMovement.ReadValue<float>();
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
    #endregion
    #region  JumpLogic
    public bool isGrounded()
    {
        return (Physics2D.OverlapCapsule(Grounded.transform.position, new Vector2(0.4f, 0.12f),
            CapsuleDirection2D.Horizontal, 0f, layerGrounded));

    }

    void jumpStatus() // Ham cap nhat cac trang thai cua nhay
    {
        // Jump Buffer : Khi nguoi chs lo an nhay som thi khi cham dat van trong thoi gian delta time thi van nhay
        if (bufferJump > 0)
        {
            bufferJump -= Time.deltaTime;
        }
        if (_inputJump.WasPressedThisFrame())
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
        jumpStatus(); // Cap nhat cac trang thai cua Jump (bufferJump hay caiyoteJump)
        
        // Thuc hien Jump
        if (CanJump())
        {
            _rb.velocity = new Vector2(_rb.velocity.x, _jumpForce);
            bufferJump = 0;
            caiyoteJump = 0;
        }
        if (!isGrounded() && _inputJump.WasReleasedThisFrame() && _rb.velocity.y > 0)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, 0.4f * _rb.velocity.y);
        }
        JumpCurve();
    }

    bool CanJump()
    {
        // Neu bufferJump van con thi van nhay duoc || neu an nhay ma caiyoteJump > 0 van nhay duoc
        return ((isGrounded() && bufferJump > 0) || (!isGrounded() && caiyoteJump > 0 && _inputJump.WasPressedThisFrame()));
    }
    // bufferJump : Nhay som
    // caiyoteJump : nhay hut doc tuong

    void JumpCurve() // Roi se nhanh hon khi nhay
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
    #endregion
    

}
