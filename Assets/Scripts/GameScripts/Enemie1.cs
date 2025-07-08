using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

public class Enemie1 : MonoBehaviour
{

    public Rigidbody2D rigidbody2d;

    public SuperMario superMario;

    public Animator animator;

    public UIManager uiManager;

    public float speed = 1f;
    public float changeTime = 3;//方向改变时间间隔
    public float timer; // 计时器
    public float delay = 1f; //怪物死亡延迟时间

    public int direction = 1; //方向

    private bool canMove = true;
    private bool isDead = false;
    private bool hasCollided = false;

    private void Start()
    {
        timer = changeTime;
    }

    private void Update()
    {
        isChangeDirection();
    }

    //移动
    private void EnemyMove()
    {
        transform.rotation = Quaternion.Euler(0, direction < 0 ? 0 : 180, 0);
        Vector2 currentVelocity = rigidbody2d.velocity; // 获取当前速度
        currentVelocity.x = speed * direction; // 更新水平速度
        rigidbody2d.velocity = currentVelocity; // 赋值新速度
    }

    //判断是否需要转向
    private void isChangeDirection()
    {
        if (!canMove) return;
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            direction = -direction;
            timer = changeTime;
        }
        EnemyMove();
    }

    //与玩家碰撞（被玩家杀死，杀死玩家）
    //与乌龟壳碰撞
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !hasCollided && !isDead)
        {
            hasCollided = true; // 标记为已碰撞
            bool landedOnEnemy = false; // 变量记录是否是踩在敌人上

            foreach (ContactPoint2D contact in collision.contacts)
            {
                if (contact.normal.y >= 0)
                {
                    landedOnEnemy = true; // 玩家踩在敌人头上
                    break; // 找到后可以直接退出循环
                }
            }

            if (landedOnEnemy)
            {
                Debug.Log("asd");
                superMario.HealthChange(-1); // 扣血
            }
            else
            {
                if (isDead) return;
                Enemy1Dead(); // 杀死敌人
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            hasCollided = false; // 重置状态
        }
    }

    //enemy1死亡
    public void Enemy1Dead()
    {
        if(isDead) return;
        canMove = false;
        isDead = true;
        Debug.Log("C");
        uiManager.UpdateKillText();
        animator.SetBool("isDead", true);
        StartCoroutine(DestroyAfterDelay(delay));
    }

    // 协程用于处理延迟销毁
    private IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // 等待指定的时间
        Destroy(gameObject); // 销毁当前GameObject
    }

    //设置怪物不能动
    public void setMotion(bool isMotion)
    {
        canMove = isMotion;
    }
}