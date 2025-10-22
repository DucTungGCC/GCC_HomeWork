using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UnlockGame : MonoBehaviour
{
    [SerializeField] private GameObject Minigame;

    private void Start()
    {
        Minigame.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Minigame.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if(Minigame != null)
                Minigame.SetActive(false);
        }
    }
}

