using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golds : MonoBehaviour
{
    public SuperMario superMario;
    private int allGulds;

    private void Start()
    {
        allGulds = AllGoldNunber();
    }

    public int AllGoldNunber()
    {
        //// 获取当前物体的 Transform 组件
        //Transform currentTransform = transform;

        //// 获取子物体的数量
        //int childCount = currentTransform.childCount;
        int childCount = transform.childCount;
        return childCount;
    }

    //传出黄金数即总黄金数
    public List<int> getGuldNumber()
    {
        List<int> gold = new List<int>();
        gold.Add(allGulds);
        gold.Add(superMario.GoldNumber());
        return gold;
    } 
}
