using System.Collections;
using UnityEngine;

public class CoroutineManager : MonoBehaviour
{
    public static CoroutineManager Instance;

    private void Awake()
    {
        // 创建单例
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 使此 GameObject 不会在场景切换时被销毁
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // 修改此方法返回 Coroutine
    public Coroutine StartManagedCoroutine(IEnumerator coroutine)
    {
        return StartCoroutine(coroutine); // 返回协程对象
    }

    // 停止协程的方法
    public void StopManagedCoroutine(Coroutine coroutine)
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine); // 停止指定的协程
        }
    }
}
