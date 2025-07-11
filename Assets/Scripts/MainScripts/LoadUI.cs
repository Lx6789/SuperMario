using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //延迟1-3秒后加载新场景
        LoadSceneWithDelay("GameScene", UnityEngine.Random.Range(1f, 3f));
    }

    // 加载场景的函数
    public void LoadSceneWithDelay(string sceneName, float delay)
    {
        StartCoroutine(LoadSceneAfterDelay(sceneName, delay));
    }

    // 协程，等待指定时间后加载场景
    private IEnumerator LoadSceneAfterDelay(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay); // 等待指定的时间
        SceneManager.LoadScene(sceneName); // 加载新场景
    }
}



    //public Button loadButton; // 你的按钮引用

    //void Start()
    //{
    //    // 确保按钮点击时触发加载场景的方法
    //    if (loadButton != null)
    //    {
    //        loadButton.onClick.AddListener(OnLoadButtonClicked);
    //    }
    //}

    //// 按钮点击时调用的方法
    //private void OnLoadButtonClicked()
    //{
    //    // 延迟1-3秒后加载新场景
    //    LoadSceneWithDelay("GameScene", UnityEngine.Random.Range(1f, 3f));
    //}

    //// 加载场景的函数
    //public void LoadSceneWithDelay(string sceneName, float delay)
    //{
    //    StartCoroutine(LoadSceneAfterDelay(sceneName, delay));
    //}

    //// 协程，等待指定时间后加载场景
    //private IEnumerator LoadSceneAfterDelay(string sceneName, float delay)
    //{
    //    yield return new WaitForSeconds(delay); // 等待指定的时间
    //    SceneManager.LoadScene(sceneName); // 加载新场景
    //}
