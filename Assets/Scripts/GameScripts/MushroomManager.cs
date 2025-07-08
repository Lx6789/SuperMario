using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomManager : MonoBehaviour
{
    public GameObject mushroomPrefab;

    private bool canGenerateMushroom = true;

    //生成蘑菇
    public void GenerateMushroom(Vector2 position)
    {
        if (!canGenerateMushroom) return;
        canGenerateMushroom = false;
        GameObject mushroom = Instantiate(mushroomPrefab, position, Quaternion.identity);
        //调用移动蘑菇的方法
        StartCoroutine(mushroom.GetComponent<Mushroom>().MoveMushroom(mushroom));
    }
}
