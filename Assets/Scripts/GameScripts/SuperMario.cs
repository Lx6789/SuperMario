using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UI.Image;

public class SuperMario : MonoBehaviour
{
    public Rigidbody2D rigidbody2d;

    public Animator animator;

    private SpriteRenderer spriteRenderer;

    public GameObject gameOver;

    public Enemie1 enemy1;
    public Enemy2 enemy2;

    

    public Vector2 jumpForce;
    public Vector2 twoJumpForce;

    private Vector3 respawnPoint;//重生点

    private BoxCollider2D boxCollider;
    private BoxCollider2D originBoxCollider;

    public UIManager uiManager;

    public float speed = 1f;
    public float changeTime = 10f; //变身时间
    private float timer = 0;

    public int maxHealth = 5;//最大生命值
    public int currentHealth;//当前生命值
    public int currentGold;//当前金币
    public int direction = 1; // 方向
    private int gold = 100;//金币分数
    //private int currentKillNumber;

    private bool isChange = false;
    private bool isGrounded = true;
    private bool isJump = false;
    private bool canDoubleJump = false;
    private bool canMotion = true;
    
    //public bool isDead = false;
    //public bool isRespawnPoint = false;

    //private int groundContactCount = 0; // 用于跟踪地面接触点

    private void Start()
    {
        currentHealth = maxHealth;
        respawnPoint = transform.position;
        boxCollider = GetComponent<BoxCollider2D>();
        originBoxCollider = boxCollider;
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentGold = 0;
        //currentKillNumber = 0;
    }

    private void Update()
    {
        MarioJump();
        MarioSquat();
        JudgmentTime();
    }

    //private void test()
    //{
    //    if (isRespawnPoint)
    //    {
    //        transform.position = respawnPoint;
    //        isRespawnPoint = false;
    //        return;
    //    }
    //}

    private void FixedUpdate()
    {
        MarioMove();
    }

    private void MarioMove()
    {
        if (!canMotion) return;
        //float index = Input.GetAxis("Horizontal"); index是线性的变化
        float index = Input.GetAxisRaw("Horizontal");
        if (index != 0)
        {
            direction = (int)Mathf.Sign(index);
            transform.rotation = Quaternion.Euler(0, direction < 0 ? 180 : 0, 0);
            //行走
            //animator.SetBool("isRun", true);
            //animator.SetBool("isIdle", false);  // 停止等待动画
            //animator.SetBool("isJump", false);
            MarioAmimator(true, false, false, false);
        } else if(isJump)
        {
            //跳跃
            //Debug.Log(isJump);
            MarioAmimator(false, false, true, false);
        }
        else 
        {
            //等待
            //animator.SetBool("isRun", false);
            //animator.SetBool("isIdle", true);  // 开始等待动画
            //animator.SetBool("isJump", false);
            MarioAmimator(false, true, false, false);
        }
        rigidbody2d.velocity = new Vector2(index * speed, rigidbody2d.velocity.y);
        //animator.SetBool("isRun", false);
        //if (Input.GetKeyDown(KeyCode.Space) && jumpReady)
        //{
        //    rigidbody2d.AddForce(jumpForce, ForceMode2D.Impulse);
        //    jumpReady = false;
        //} 
    }

    //跳跃与二段跳
    private void MarioJump()
    {
        if (!canMotion) return;
        if (Input.GetButtonDown("Jump"))
        {
            //Debug.Log("A" + isGrounded);
            if (isGrounded)
            {
                isJump = true;
                rigidbody2d.AddForce(jumpForce, ForceMode2D.Impulse);
                //groundContactCount++; // 增加计数以允许双跳
                isGrounded = false;
                canDoubleJump = true;
                MarioAmimator(false, false, true, false);
            }
            else if (canDoubleJump)
            {
                //Debug.Log("b" + groundContactCount);
                rigidbody2d.AddForce(twoJumpForce, ForceMode2D.Impulse);
                //groundContactCount++; // 增加计数以允许双跳
                canDoubleJump = false;
                MarioAmimator(false, false, true, false);
            }
        }
    }

    //跳跃动画
    private void MarioAmimator(bool isRun, bool isIdle, bool isJump, bool isDead)
    {
        //Debug.Log("跳跃动画播放");
        animator.SetBool("isRun", isRun);
        animator.SetBool("isIdle", isIdle);  // 停止等待动画
        animator.SetBool("isJump", isJump);
        animator.SetBool("isDead", isDead);
    }

    // 处理死亡的协程
    private IEnumerator Oligemia()
    {
        canMotion = false;
        MarioAmimator(false, false, false, true); // 播放死亡动画

        // 等待死亡动画播放完成
        yield return new WaitUntil(() =>
            animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f &&
            !animator.IsInTransition(0)
        );

        // 设置重生点
        setRespawnPoint();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 检查是否从上方落地（避免侧碰重置）
        foreach (ContactPoint2D contact in collision.contacts)
        {
            if (contact.normal.y > 0.7f) // 碰撞点法线朝上
            {
                //groundContactCount++; // 增加地面接触计数
                isGrounded = true;
                isJump = false;
                //Debug.Log(groundContactCount);
                break;
            }
        }
        //if (collision.gameObject.CompareTag("Ground"))
        //{
        //    // 检查是否从上方落地（避免侧碰重置）
        //    foreach (ContactPoint2D contact in collision.contacts)
        //    {
        //        if (contact.normal.y > 0.7f) // 碰撞点法线朝上
        //        {
        //            //groundContactCount++; // 增加地面接触计数
        //            isGrounded = true;
        //            //Debug.Log(groundContactCount);
        //            break;
        //        }
        //    }
        //}
    }

    //马里奥变大
    public void MarioChange()
    {
        //移速减1，跳跃高度加1
        speed += 1;
        jumpForce.y += 2;
        twoJumpForce.y += 2;
        // 计算新的缩放值
        Vector3 currentScale = transform.localScale;
        Vector3 newScale = currentScale * 1.5f; // 增大 50%
        //Vector3 newCollisonScale = boxCollider.size * 1.5f;
        // 应用新的缩放值
        transform.localScale = newScale;
        boxCollider.size = new Vector2(boxCollider.size.x, boxCollider.size.y);
        isChange = true;
    }

    //判断变身时间是否结束
    private void JudgmentTime()
    {
        if (!isChange) return;
        //Debug.Log(timer);
        if (timer >= changeTime)
        {
            timer = 0;
            //调用马里奥变回原样函数
            returnToItsOriginalState();
        }
        timer += Time.deltaTime;
    }

    //马里奥变回原样
    private void returnToItsOriginalState()
    {
        speed = 5;
        jumpForce = new Vector3(0, 10, 0);
        twoJumpForce = new Vector3(0, 12, 0);
        transform.localScale = new Vector3(1, 1, 1);
        boxCollider.size = originBoxCollider.size;
        boxCollider.size = new Vector2(boxCollider.size.x, boxCollider.size.y);
        isChange = false;
    }

    //马里奥下蹲
    private void MarioSquat()
    {
        //判断是否按下下键
        if (Input.GetAxis("Vertical") < 0)
        {
            //如果还在变大状态，则变回原样
            if (isChange)
            {
                returnToItsOriginalState();
            }
            //换为下蹲动画
        }
    }

    //血量改变
    public void HealthChange(int amount) 
    {
        Debug.Log("amount");
        if (amount > 0 && currentHealth == maxHealth) return;
        currentHealth += amount;
        if (currentHealth <= 0)
        {
            MarioDead();
        }
        //若没死则将马里奥移动到重生点
        if (currentHealth != 0 && amount < 0)
        {
            StartCoroutine(Oligemia());
        }
    }

    //马里奥死亡
    public void MarioDead()
    {
        //Debug.Log("马里奥死亡...");
        uiManager.GameOver();
        gameOver.SetActive(true);
        setMotion(false);
        enemy1.setMotion(false);
        enemy2.setMotion(false);
        Destroy(gameObject);
    }

    //当玩家碰到则调用设置重生点函数
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("RespawnPoint"))
        {
            respawnPoint = collision.gameObject.transform.position;
        }else if (collision.gameObject.CompareTag("Gold"))
        {
            currentGold += gold;
            uiManager.UpdateGold(currentGold);
        }
    }

    //返回重生点
    private void setRespawnPoint()
    {
        canMotion = true;
        transform.position = respawnPoint; 
        returnToItsOriginalState();
    }

    //设置马里奥不能动
    public void setMotion(bool isMotion)
    {
        canMotion = isMotion;
    }

    //传出最大生命值及当下生命值
    public List<int> getHealth()
    {
        List<int> health = new List<int>();
        health.Add(currentHealth);        
        health.Add(maxHealth);
        return health;
    }

    //传出所获金币的数量
    public int GoldNumber()
    {
        return currentGold / gold;
    }
}