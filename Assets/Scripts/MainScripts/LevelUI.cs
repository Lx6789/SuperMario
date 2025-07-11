using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUI : MonoBehaviour
{
    public MainUIManager mainUIManager;

    public GameObject[] levels;

    public GameSO gameSO;

    private void Start()
    {
        Show();
    }

    //返回按钮
    public void OnClickBackButton()
    {
        mainUIManager.OpenMainUI();
    }

    //刷新关卡数据并显示关卡解锁状态
    public void Show()
    {
        if (gameSO.starNumberOfLevel.Length != levels.Length) return;
        for (int i = 0; i < gameSO.starNumberOfLevel.Length; i++) {
            levels[i].GetComponent<Level>().ShowLevel(gameSO.starNumberOfLevel[i]);
        }
    }

    //切换加载场景
    public void Load()
    {
        mainUIManager.OpenLoadUI();
    }
}
