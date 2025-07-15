using System.Collections.Generic;
using UnityEngine;

public class Golds : MonoBehaviour
{
    [Header("依赖引用")]
    [SerializeField] private SuperMario player;  // 通过Inspector赋值或自动获取

    private int totalGoldCount;

    private void Start()
    {
        InitializeReferences();
        CacheTotalGoldCount();
    }

    private void InitializeReferences()
    {
        // 自动获取引用（如果未在Inspector中赋值）
        if (player == null)
        {
            player = FindObjectOfType<SuperMario>();
        }
    }

    private void CacheTotalGoldCount()
    {
        totalGoldCount = CalculateTotalGoldCount();
    }

    /// <summary>
    /// 计算场景中初始金币总数
    /// </summary>
    private int CalculateTotalGoldCount()
    {
        return transform.childCount;
    }

    /// <summary>
    /// 获取金币状态 [0]=场景总金币数, [1]=玩家已收集数
    /// </summary>
    public List<int> GetGoldStatus()
    {
        return new List<int>
        {
            totalGoldCount,                     // 场景初始金币总数
            player != null ? player.GetGoldCount() : 0  // 玩家当前收集数
        };
    }
}