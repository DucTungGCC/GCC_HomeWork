using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class FlyEnemy : Enemy
{
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private LayerMask _playerMask;
    [SerializeField] private GameObject _wallCheck;
    [SerializeField] private SpriteRenderer _sr;
    [SerializeField] private Transform _tf;
    public float attackForce = 2f;
    public float direction = 1;

    private void Start()
    {
        _rb.gravityScale = 0;
    }

    void Update()
    {
        if (PlayerIsHere() && Hp > 0)
        {
            Move();
        }
        else if(Hp <= 0)
        {
            Death();
        }
    }
    
    protected override void Move()
    {
        if (Mathf.Abs(_rb.velocity.x) < speed)
        {
            _rb.AddForce(Vector2.right * (direction * speed), ForceMode2D.Force);
        }

        CheckWall();
    }

    public override void OnAttack(Vector2 dir)
    {
        _rb.velocity = Vector2.zero;
        dir.y = _tf.position.y;
        _rb.AddForce(((- dir + (Vector2)(_tf.position)).normalized) * attackForce, ForceMode2D.Impulse);
        Hp -= 20;
        if(Hp > 0)Debug.Log("Con lai " + Hp + " Hp");
    }

    protected override void CheckWall()
    {
        if (Physics2D.OverlapBox(_wallCheck.transform.position, new Vector2(2f, 2f),
                 0f, _groundMask))
        {
            _sr.flipX = !_sr.flipX;
            direction = _sr.flipX ? -1f : 1f;
            _wallCheck.transform.position = new Vector3(direction + _tf.position.x , _wallCheck.transform.position.y, _wallCheck.transform.position.z);
        }
    }

    private void Death()
    {
        _sr.color = Color.red;
        _rb.gravityScale = 1;
    }

    private bool PlayerIsHere()
    {
        return Physics2D.OverlapCircle(_tf.position, 10f, _playerMask);
    }
}