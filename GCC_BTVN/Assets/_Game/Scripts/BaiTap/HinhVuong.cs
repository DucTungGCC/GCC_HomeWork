using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HinhVuong : ShowMess
{
    protected override void Messenger()
    {
        Debug.Log("Toi la hinh vuong");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Messenger();
    }
}
