using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainUIs : MonoBehaviour
{

    public GameObject moreUI;
    //public GameObject loadingUI;
    //public GameObject LevellistGrid;
    //public GameObject levelManager;

    public MainUIManager mainUIManager;

    //打开mama
    public void OpenMaMa()
    {
        mainUIManager.OpenMoreUI();
    }

    //退出游戏
    public void ExitGame()
    {
        Application.Quit();
    }

    //开始游戏
    public void PlayGame()
    {
        //加载新场景
        SceneManager.LoadScene("Level");
    }
}
