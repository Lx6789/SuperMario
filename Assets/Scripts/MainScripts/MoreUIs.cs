﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoreUIs : MonoBehaviour
{

    public GameObject mainUIs;
    public MainUIManager mainUIManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //返回主页
    public void BackMain()
    {
        mainUIManager.OpenMainUI();
    }
}
