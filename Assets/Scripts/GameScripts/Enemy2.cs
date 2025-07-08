using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    public Rigidbody2D rigidbody2d;
    public Enemie1 enemie1;
    public SuperMario superMario;
    public Animator animator;

    public UIManager uiManager;

    public float speed = 1f;
    public float changeTime = 3f; // 方向改变时间间隔
    public float timer; // 计时器
    private float delay = 5f; // 怪物死亡延迟时间
    public float focus = 1f;

    public int direction = 1; // 方向

    private bool isTurtleShell = false;
    private bool isDead = false;
    private bool canMove = true;
    private bool hasCollided = false;

    private void FixedUpdate()
    {
        isChangeDirection();
    }

    private void Update()
    {

    }

    // 移动
    private void EnemyMove()
    {
        if (!canMove) return;
        transform.rotation = Quaternion.Euler(0, direction < 0 ? 0 : 180, 0);
        Vector2 currentVelocity = rigidbody2d.velocity; // 获取当前速度
        currentVelocity.x = speed * direction; // 更新水平速度
        rigidbody2d.velocity = currentVelocity; // 赋值新速度
        //Vector2 position = rigidbody2d.position; // 获取当前位置
        //position.x += speed * Time.deltaTime * direction;
        //rigidbody2d.MovePosition(position);
    }

    // 判断是否需要转向
    private void isChangeDirection()
    {
        // 这里只判断正常状态下的转向
        if (!isTurtleShell)
        {
            timer -= Time.deltaTime;

            if (timer < 0)
            {
                direction = -direction;
                timer = changeTime;
            }
        }

        // 无论是正常状态还是龟壳状态，都执行移动
        EnemyMove();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !hasCollided)
        {
            bool landedOnEnemy = false; // 变量记录是否是踩在敌人上
            hasCollided = true;
            foreach (ContactPoint2D contact in collision.contacts)
            {
                if (contact.normal.y >= 0)
                {
                    landedOnEnemy = true; // 玩家踩在敌人头上
                    break; // 找到后可以直接退出循环
                }
                // 如果马里奥踩在怪物头上则怪物死亡
                //if (contact.normal.y >= 0)
                //{
                //    Debug.Log("马里奥死了");
                //    superMario.HealthChange(-1);
                //}
                //else // 其他情况处理
                //{
                //    if (isDead) return;
                //    isDead = true;
                //    Debug.Log("AA");
                //    BecomeTurtleShell();
                //}
            }
            if (landedOnEnemy)
            {
                superMario.HealthChange(-1);
            } else
            {
                if (isDead) return;
                isDead = true;
                Debug.Log("AA");
                BecomeTurtleShell();
            }
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            if (!isTurtleShell) return;
            enemie1.Enemy1Dead();
        }
        else if (isTurtleShell) // 在龟壳状态下，碰撞任何东西都改变方向(除了地板)
        {
            //Debug.Log("b" + isTurtleShell);
            if (collision.gameObject.CompareTag("Ground")) return;
            direction = -direction; // 碰撞后反向移动
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            hasCollided = false; // 重置状态
        }
    }

    private void BecomeTurtleShell()
    {
        isTurtleShell = true;
        speed = 10; // 加速逻辑
        Enemy2Dead();
    }

    // enemy2死亡
    private void Enemy2Dead()
    {
        Debug.Log(focus * direction);
        uiManager.UpdateKillText();
        animator.SetBool("isDead", true);
        rigidbody2d.AddForce(new Vector2(focus * direction, 0), ForceMode2D.Impulse); // 使用 Impulse 更合适
        StartCoroutine(DestroyAfterDelay(delay));
    }

    // 协程用于处理延迟销毁
    private IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // 等待指定的时间
        Destroy(gameObject); // 销毁当前GameObject
    }

    public void setMotion(bool isMotion)
    {
        canMove = isMotion;
    }
}