using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    public int currentEnemy;
    public int allEnemy;

    private void Start()
    {
        allEnemy = NowEnemyNunber();
        currentEnemy = allEnemy;
    }

    private void Update()
    {
        currentEnemy = NowEnemyNunber();
    }

    public int NowEnemyNunber()
    {
        int childCount = transform.childCount;
        //// 获取当前物体的 Transform 组件
        //Transform currentTransform = transform;

        //// 获取子物体的数量
        //int childCount = currentTransform.childCount;
        return childCount;
    }

    //返回当前怪物数及总怪物数
    public List<int> getEnemyNumber()
    {
        List<int> enemy = new List<int>();
        enemy.Add(currentEnemy);
        enemy.Add(allEnemy);
        return enemy;
    }
}
