using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    protected float Hp = 60f;
    protected float speed = 3f;

    protected abstract void Move();

    public virtual void OnAttack(Vector2 direction)
    {
        
    }

    protected abstract void CheckWall();


}
