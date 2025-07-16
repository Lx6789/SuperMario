using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class Enemy2 : MonoBehaviour
{
    [Header("移动设置")]
    [SerializeField] private float normalSpeed = 1f;
    [SerializeField] private float shellSpeed = 10f;
    [SerializeField] private float directionChangeInterval = 3f;
    [SerializeField] private float shellKnockbackForce = 1f;
    [SerializeField] private float deathDelay = 5f;
    private int direction = 1; // 1=右, -1=左

    [Header("状态控制")]
    private bool isTurtleShell = false;
    private bool isDead = false;
    private bool canMove = true;
    private bool hasCollided = false;
    private float directionTimer;

    [Header("组件引用")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;
    private SuperMario player;
    private UIManager uiManager;
    private Enemy1 enemy1;

    private void Awake()
    {
        GetRequiredComponents();
    }

    private void Start()
    {
        InitializeValues();
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void GetRequiredComponents()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        player = FindObjectOfType<SuperMario>();
        uiManager = FindObjectOfType<UIManager>();
        enemy1 = FindObjectOfType<Enemy1>();
    }

    private void InitializeValues()
    {
        directionTimer = directionChangeInterval;
    }

    private void HandleMovement()
    {
        if (!canMove) return;

        UpdateDirection();
        MoveCharacter();
    }

    private void UpdateDirection()
    {
        if (isTurtleShell) return;

        directionTimer -= Time.deltaTime;
        if (directionTimer <= 0)
        {
            direction *= -1;
            directionTimer = directionChangeInterval;
        }
    }

    private void MoveCharacter()
    {
        transform.rotation = Quaternion.Euler(0, direction < 0 ? 0 : 180, 0);
        Vector2 velocity = rb.velocity;
        velocity.x = (isTurtleShell ? shellSpeed : normalSpeed) * direction;
        rb.velocity = velocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            HandlePlayerCollision(collision);
        }
        else if (collision.gameObject.CompareTag("Enemy") && isTurtleShell)
        {
            enemy1?.Die();
        }
        else if (isTurtleShell && !collision.gameObject.CompareTag("Ground"))
        {
            direction *= -1;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            hasCollided = false;
        }
    }

    private void HandlePlayerCollision(Collision2D collision)
    {
        if (hasCollided) return;
        
        hasCollided = true;
        bool playerOnTop = CheckIfPlayerOnTop(collision);
        bool shouldNotDamage = playerOnTop && !isTurtleShell;

        // 伤害判定
        if (playerOnTop || isTurtleShell)
        {
            player?.ChangeHealth(-1);
        }

        // 变身判定
        if (!shouldNotDamage)
        {
            TurnIntoTurtleShell();
        }
    }

    private bool CheckIfPlayerOnTop(Collision2D collision)
    {
        foreach (ContactPoint2D contact in collision.contacts)
        {
            if (contact.normal.y >= 0) return true;
        }
        return false;
    }

    private void TurnIntoTurtleShell()
    {
        if (!isDead) uiManager?.UpdateKillText();
        isTurtleShell = true;
        isDead = true;
        animator.SetBool("isDead", true);
        rb.AddForce(new Vector2(shellKnockbackForce * direction, 0), ForceMode2D.Impulse);
        StartCoroutine(DestroyAfterDelay(deathDelay));
    }

    private IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }

    public void SetMotion(bool canMove)
    {
        this.canMove = canMove;
    }
}