using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadUI : MonoBehaviour
{

    //延迟1-3秒后打开游戏
    public void openGame()
    {
        StartCoroutine(LazyLoading());
    }

    private IEnumerator LazyLoading()
    {
        yield return new WaitForSeconds(Random.Range(1f, 2f));
        SceneManager.LoadScene("GameScene");
    }
}