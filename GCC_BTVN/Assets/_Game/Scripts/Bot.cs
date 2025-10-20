using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot : MonoBehaviour
{
    [SerializeField] private ActionNPC controller; // Tat bat chat
    [SerializeField] private Rigidbody2D rigidbody2D;

    IEnumerator Die()
    {
        yield return new WaitForSeconds(5f);
        rigidbody2D.gameObject.SetActive(false);
    }
    private void Update()
    {
        if (rigidbody2D.velocity.magnitude > 0.3f)
        {
            controller.TakeDamage();
            StartCoroutine(Die());
        }
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
}
