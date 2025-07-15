using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))] // 自动添加必要组件
public class Enemy1 : MonoBehaviour
{
    [Header("移动设置")]
    [SerializeField] private float speed = 1f;          // 移动速度
    [SerializeField] private float directionChangeInterval = 3f; // 方向切换间隔
    [SerializeField] private int direction = 1;         // 当前朝向（1右/-1左）
    [SerializeField] private float deathDelay = 1f;     // 死亡后销毁延迟

    [Header("组件引用")]
    [SerializeField] private Rigidbody2D rb;           // 刚体组件
    [SerializeField] private Animator animator;         // 动画控制器
    private SuperMario player;                          // 玩家引用
    private UIManager uiManager;                        // UI管理器

    [Header("状态标记")]
    private bool canMove = true;    // 能否移动
    private bool isDead = false;     // 是否死亡
    private bool hasCollided = false;// 碰撞状态锁
    private float directionTimer;    // 方向切换计时器

    private void Awake()
    {
        GetComponentReferences();
    }

    private void Start()
    {
        InitializeValues();
    }

    private void Update()
    {
        HandleDirectionChange();
    }

    // 获取组件引用
    private void GetComponentReferences()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        player = FindObjectOfType<SuperMario>();
        uiManager = FindObjectOfType<UIManager>();
    }

    // 初始化数值
    private void InitializeValues()
    {
        directionTimer = directionChangeInterval;
    }

    // 处理方向切换逻辑
    private void HandleDirectionChange()
    {
        if (!canMove) return;

        directionTimer -= Time.deltaTime;
        if (directionTimer <= 0)
        {
            ChangeDirection();
        }
        MoveEnemy();
    }

    // 改变移动方向
    private void ChangeDirection()
    {
        direction *= -1;
        directionTimer = directionChangeInterval;
    }

    // 执行敌人移动
    private void MoveEnemy()
    {
        transform.rotation = Quaternion.Euler(0, direction < 0 ? 0 : 180, 0);
        Vector2 velocity = rb.velocity;
        velocity.x = speed * direction;
        rb.velocity = velocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !hasCollided && !isDead)
        {
            hasCollided = true;
            HandlePlayerCollision(collision);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            hasCollided = false;
        }
    }

    // 处理与玩家的碰撞
    private void HandlePlayerCollision(Collision2D collision)
    {
        bool playerLandedOnEnemy = false;

        // 检测碰撞点法线方向
        foreach (ContactPoint2D contact in collision.contacts)
        {
            if (contact.normal.y >= 0) // 玩家从上方踩踏
            {
                playerLandedOnEnemy = true;
                break;
            }
        }

        if (playerLandedOnEnemy)
        {
            player?.ChangeHealth(-1); // 玩家扣血
        }
        else
        {
            Die(); // 敌人死亡
        }
    }

    // 敌人死亡处理
    public void Die()
    {
        if (isDead) return;

        canMove = false;
        isDead = true;
        animator.SetBool("isDead", true); // 触发死亡动画

        uiManager?.UpdateKillText(); // 更新击杀数
        StartCoroutine(DestroyAfterDelay(deathDelay)); // 延迟销毁
    }

    // 延迟销毁协程
    private IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }

    // 设置移动权限
    public void SetMotion(bool canMove)
    {
        this.canMove = canMove;
    }
}