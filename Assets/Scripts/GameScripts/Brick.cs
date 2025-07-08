using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    //被马里奥从下往上碰撞到后摧毁
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // 检查是否从上方落地（避免侧碰重置）
            foreach (ContactPoint2D contact in collision.contacts)
            {
                if (contact.normal.y > 0) // 碰撞点法线朝下
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}
