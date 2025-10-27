using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HinhTron : ShowMess
{
    protected override void Messenger()
    {
        Debug.Log("Hinh tron");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Messenger();
    }
}
