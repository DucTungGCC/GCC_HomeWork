using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class WalkEnemy : Enemy
{
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private LayerMask _playerMask;
    [SerializeField] private GameObject _wallCheck;
    [SerializeField] private SpriteRenderer _sr;
    [SerializeField] private Transform _tf;
    public float attackForce = 2f;
    public float direction = 1;

    void Update()
    {
        if (PlayerIsHere())
        {
            Move();
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
        _rb.AddForce(((- dir + (Vector2)(_tf.position)).normalized + Vector2.up * 0.5f) * attackForce, ForceMode2D.Impulse);
    }

    void CheckWall()
    {
        if (Physics2D.OverlapCapsule(_wallCheck.transform.position, new Vector2(0.5f, 0.75f),
                CapsuleDirection2D.Vertical, 0f, _groundMask))
        {
            _sr.flipX = !_sr.flipX;
            direction = _sr.flipX ? -1f : 1f;
            _wallCheck.transform.position = new Vector3(direction + _tf.position.x , _wallCheck.transform.position.y, _wallCheck.transform.position.z);
        }
    }
    
    // private void OnCollisionEnter2D(Collision2D collision)
    // {
    //     if (collision.gameObject.CompareTag("Ground"))
    //     {
    //         _sr.flipX = !_sr.flipX;
    //         direction *= -1;
    //         _wallCheck.transform.position = new Vector3(direction, _wallCheck.transform.position.y, _wallCheck.transform.position.z);
    //     }
    // }

    private bool PlayerIsHere()
    {
        return Physics2D.OverlapCircle(_tf.position, 10f, _playerMask);
    }
}
