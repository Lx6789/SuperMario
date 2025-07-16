using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndHouse : MonoBehaviour
{
    [Header("引用")]
    public UIManager uiManager;

    [Header("游戏数据")]
    public GameSO gameSO;

    private void Start()
    {
        uiManager = UIManager.Instance;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;
        //Debug.Log("Win");
        AudioManager.instance.PlayWin(transform.position);
        uiManager.ShowGameOver(transform.position);
        UnlockNextLevel();
        Destroy(collision.gameObject);
    }

    //解开下一关卡的锁
    private void UnlockNextLevel()
    {
        if (gameSO.starNumberOfLevel[gameSO.levelId] == -1 && gameSO.starNumberOfLevel.Length != gameSO.levelId)
        {
            gameSO.starNumberOfLevel[gameSO.levelId] = 0;
        }
    }
}
