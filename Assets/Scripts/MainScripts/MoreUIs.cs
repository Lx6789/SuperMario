using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoreUIs : MonoBehaviour
{

    public GameObject mainUIs;
    public MainUIManager mainUIManager;

    //返回主页
    public void BackMain()
    {
        mainUIManager.OpenMainUI();
    }
}
