using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessNPC : MonoBehaviour
{
    [SerializeField] private Image MessengeBox;
    public bool wasVisited = false;

    private void Start()
    {
        MessengeBox.gameObject.SetActive(false);
    }

    IEnumerator CloseMessBox()
    {
        yield return new WaitForSeconds(5f);
        MessengeBox.gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (!wasVisited)
            {
                MessengeBox.gameObject.SetActive(true);
                StartCoroutine(CloseMessBox());
                wasVisited = true;
            }
                
        }
    }

    public void TakeDamage()
    {
        StopAllCoroutines();
        MessengeBox.gameObject.SetActive(false);
    }
    
    private void OnDisable()
    {
        StopAllCoroutines();
    }
}

