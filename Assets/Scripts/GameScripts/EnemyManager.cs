using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [Header("敌人生成设置")]
    [Tooltip("初始敌人总数")]
    private int totalEnemyCount;

    [Tooltip("当前存活的敌人数量")]
    private int currentEnemyCount;

    private void Start()
    {
        InitializeEnemyCount();
    }

    private void InitializeEnemyCount()
    {
        totalEnemyCount = CountEnemies();
        currentEnemyCount = totalEnemyCount;
    }

    private void Update()
    {
        UpdateEnemyCount();
    }

    /// <summary>
    /// 实时更新当前敌人数量
    /// </summary>
    private void UpdateEnemyCount()
    {
        currentEnemyCount = CountEnemies();
    }

    /// <summary>
    /// 计算场景中存活的敌人数量
    /// </summary>
    private int CountEnemies()
    {
        return transform.childCount;
    }

    /// <summary>
    /// 获取敌人状态 [0]=当前存活数, [1]=初始总数
    /// </summary>
    public List<int> GetEnemyStatus()
    {
        return new List<int>
        {
            currentEnemyCount,
            totalEnemyCount
        };
    }

    /// <summary>
    /// 设置所有子物体的移动状态
    /// </summary>
    // 调用当前物体所有直接子物体的SetMotion方法
    public void SetChildrenMotion(bool isActive)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);

            // 方案1A：如果子物体自身有SetMotion方法
            if (child.TryGetComponent<Enemy2>(out var enemy))
            {
                enemy.SetMotion(isActive);
            }
        }
    }

    /// <summary>
    /// 重置敌人管理器（切换关卡时调用）
    /// </summary>
    public void ResetManager()
    {
        InitializeEnemyCount();
    }
}