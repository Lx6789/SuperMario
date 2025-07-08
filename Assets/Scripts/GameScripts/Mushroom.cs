using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : MonoBehaviour
{

    // 目标移动距离
    public float distance = 0.3f; // 向上移动的距离
    public float duration = 1.0f;  // 完成移动的时间

    //移动蘑菇
    public IEnumerator MoveMushroom(GameObject mushroom)
    {
        Vector2 startPosition = mushroom.transform.position; // 起始位置
        Vector2 targetPosition = startPosition + new Vector2(0, distance); // 目标位置
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            mushroom.transform.position = Vector2.Lerp(startPosition, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime; // 每帧增加经过的时间
            yield return null; // 等待下一帧
        }

        mushroom.transform.position = targetPosition; // 确保最终位置为目标
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //销毁蘑菇
            Destroy(gameObject);
            //调用马里奥变大函数
            collision.gameObject.GetComponent<SuperMario>().MarioChange();
        }
    }
}
