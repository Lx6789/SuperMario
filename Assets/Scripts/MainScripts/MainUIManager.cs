using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using UnityEngine;

public class MainUIManager : MonoBehaviour
{

    public GameObject mainUI;
    public GameObject levelUI;
    public GameObject moreUI;
    public GameObject loadUI;


    //打开主页
    public void OpenMainUI() {
        UpdateUI(true, false, false, false);
    }

    //打开列表
    public void OpenLevelUI()
    {
        UpdateUI(false, true, false, false);
    }

    //打开更多
    public void OpenMoreUI()
    {
        UpdateUI(false, false, true, false);
    }

    //加载
    public void OpenLoadUI()
    {
        UpdateUI(false, false, false, true);
    }

    //修改UI打开情况
    private void UpdateUI(bool isOpenOfMainUI, bool isOpenOfLevelUI, bool isOpenOfMoreUI, bool isOpenOfLoadUI)
    {
        mainUI.SetActive(isOpenOfMainUI);
        levelUI.SetActive(isOpenOfLevelUI);
        moreUI.SetActive(isOpenOfMoreUI);
        loadUI.SetActive(isOpenOfLoadUI);
    }
}
