using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MysteryBox : MonoBehaviour
{
    public MushroomManager mushroomManager;

    public Sprite[] sprites;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // 检查是否从上方落地（避免侧碰重置）
            foreach (ContactPoint2D contact in collision.contacts)
            {
                if (contact.normal.y > 0) // 碰撞点法线朝下
                {
                    //被碰撞后切换精灵然后出现蘑菇
                    gameObject.GetComponent<SpriteRenderer>().sprite = sprites[1];
                    //调用蘑菇出现的函数
                    mushroomManager.GenerateMushroom(transform.position);
                }
            }
        }
    }
}
