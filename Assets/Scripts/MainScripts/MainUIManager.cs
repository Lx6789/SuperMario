using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using UnityEngine;

public class MainUIManager : MonoBehaviour
{

    public GameObject mainUI;
    //public GameObject levelUI;
    public GameObject moreUI;


    //打开主页
    public void OpenMainUI() {
        UpdateUI(true, false);
    }

    //打开更多
    public void OpenMoreUI()
    {
        UpdateUI(false, true);
    }

    //修改UI打开情况
    private void UpdateUI(bool isOpenOfMainUI, bool isOpenOfMoreUI)
    {
        mainUI.SetActive(isOpenOfMainUI);
        moreUI.SetActive(isOpenOfMoreUI);
    }
}
