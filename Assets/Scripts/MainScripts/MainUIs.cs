using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainUIs : MonoBehaviour
{

    public GameObject moreUI;
    //public GameObject loadingUI;
    public GameObject LevellistGrid;

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
        gameObject.SetActive(false);
        moreUI.SetActive(true);
    }

    //退出游戏
    public void ExitGame()
    {
        Application.Quit();
    }

    //开始游戏
    public void PlayGame()
    {
        gameObject.SetActive(false);
        LevellistGrid.SetActive(true);
        //DeactivateAllChildren();
        //loadingUI.SetActive(true);
        //StartCoroutine(LoadNextScene());
    }

    //等待0-3秒后更换场景
    IEnumerator LoadNextScene()
    {
        yield return new WaitForSeconds(Random.Range(0, 3));
        SceneManager.LoadScene("GameScene");
    }

    private void DeactivateAllChildren()
    {
        // 使用栈避免递归
        Stack<Transform> stack = new Stack<Transform>();

        // 添加所有直接子对象
        for (int i = 0; i < transform.childCount; i++)
        {
            stack.Push(transform.GetChild(i));
        }

        while (stack.Count > 0)
        {
            Transform current = stack.Pop();
            current.gameObject.SetActive(false);

            // 添加当前对象的子对象
            for (int i = 0; i < current.childCount; i++)
            {
                stack.Push(current.GetChild(i));
            }
        }
    }
}
