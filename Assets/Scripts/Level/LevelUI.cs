using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelUI : MonoBehaviour
{
    public GameObject LoadUI;

    public GameObject[] levels;

    public GameSO gameSO;

    public GameObject load;

    private void Start()
    {
        Show();
    }

    //返回按钮
    public void OnClickBackButton()
    {
        SceneManager.LoadScene("MainScene");
    }

    //刷新关卡数据并显示关卡解锁状态
    public void Show()
    {
        if (gameSO.starNumberOfLevel.Length != levels.Length) return;
        for (int i = 0; i < gameSO.starNumberOfLevel.Length; i++) {
            levels[i].GetComponent<Level>().ShowLevel(gameSO.starNumberOfLevel[i]);
            levels[i].GetComponent<Level>().levelId = i + 1;
        }
    }

    //打开加载页面
    public void Load(int levelId)
    {
        gameSO.levelId = levelId;
        LoadUI.SetActive(true);
        gameObject.SetActive(false); 
        load.GetComponent<LoadUI>().openGame();
    }
}
