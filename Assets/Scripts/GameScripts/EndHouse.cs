﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndHouse : MonoBehaviour
{
    public Enemie1 enemy1;
    public Enemy2 enemy2;
    public SuperMario SuperMario;
    public UIManager uiManager;

    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;
        //Debug.Log("Win");
        uiManager.GameOver();
        Destroy(collision.gameObject);
    }
}
