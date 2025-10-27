using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour, IColorable
{
    [SerializeField] private Color color;
     private GameObject thisBlock => this.gameObject;
    private SpriteRenderer otherSr;

    private void Awake()
    {
        if (color == null)
        {
            color = GetComponent<SpriteRenderer>().color;
        }

        color.a = 1;
    }

    public void changeColor()
    {
        if (otherSr != null)
        {
            otherSr.color = color;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        ShowMess("Da cham vao", other.gameObject);
        if (other.gameObject.CompareTag("Player"))
        {
            otherSr = other.GetComponent<SpriteRenderer>();
            changeColor();
            thisBlock.SetActive(false);
        }
    }

    private void ShowMess(string temp, GameObject obj)
    {
        Debug.Log(temp + " From " + obj.name);
    }
}
