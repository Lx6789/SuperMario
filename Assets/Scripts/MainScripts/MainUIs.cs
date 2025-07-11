using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainUIs : MonoBehaviour
{

    public GameObject moreUI;
    //public GameObject loadingUI;
    public GameObject LevellistGrid;
    public GameObject levelManager;

    public MainUIManager mainUIManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
        //调用启动level界面的函数
        mainUIManager.OpenLevelUI();
    }
}
