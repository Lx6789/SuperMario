using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator), typeof(SpriteRenderer))]
public class SuperMario : MonoBehaviour
{
    [Header("物理组件")]
    [SerializeField] private Rigidbody2D rigidbody2d;
    [SerializeField] private Animator animator;
    [SerializeField] private BoxCollider2D boxCollider;

    [Header("移动参数")]
    public float speed = 5f;
    public Vector2 jumpForce = new Vector2(0, 10f);
    public Vector2 doubleJumpForce = new Vector2(0, 12f);
    private int direction = 1;
    private bool canMotion = true;

    [Header("跳跃控制")]
    private bool isGrounded = true;
    private bool canDoubleJump = false;

    [Header("变身系统")]
    public float changeTime = 10f;
    private float timer = 0;
    private bool isTransformed = false;
    private Vector3 originalScale;
    private Vector2 originalColliderSize;

    [Header("生命值")]
    public int maxHealth = 5;
    public int currentHealth;
    private Vector3 respawnPoint;

    [Header("金币系统")]
    public int goldValue = 100;
    public int currentGold;
    public UIManager uiManager;

    [Header("引用对象")]
    public GameObject gameOverUI;
    private LevelManager levelManager;

    private void Start()
    {
        InitializeComponents();
        InitializeState();
        FindLevelManager();
        FindUIManager();
    }


    private void InitializeComponents()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void InitializeState()
    {
        currentHealth = maxHealth;
        respawnPoint = transform.position;
        currentGold = 0;

        originalScale = transform.localScale;
        originalColliderSize = boxCollider.size;
    }

    private void FindLevelManager()
    {
        levelManager = LevelManager.Instance;
        if (levelManager != null && levelManager.player == null)
        {
            levelManager.player = this;
        }
    }

    private void FindUIManager()
    {
        uiManager = UIManager.Instance;
    }

    private void Update()
    {
        HandleJumpInput();
        HandleSquatInput();
        CheckTransformationTimer();
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        if (!canMotion) return;

        float input = Input.GetAxisRaw("Horizontal");
        if (input != 0)
        {
            direction = (int)Mathf.Sign(input);
            transform.rotation = Quaternion.Euler(0, direction < 0 ? 180 : 0, 0);
            UpdateAnimation(true, false, false);
        }
        else if (!isGrounded)
        {
            UpdateAnimation(false, false, true);
        }
        else
        {
            UpdateAnimation(false, true, false);
        }

        rigidbody2d.velocity = new Vector2(input * speed, rigidbody2d.velocity.y);
    }

    private void HandleJumpInput()
    {
        if (!canMotion || !Input.GetButtonDown("Jump")) return;

        if (isGrounded)
        {
            PerformJump(jumpForce);
            canDoubleJump = true;
        }
        else if (canDoubleJump)
        {
            PerformJump(doubleJumpForce);
            canDoubleJump = false;
        }
    }

    private void PerformJump(Vector2 force)
    {
        AudioManager.instance.PlayJump(transform.position);
        rigidbody2d.AddForce(force, ForceMode2D.Impulse);
        isGrounded = false;
        UpdateAnimation(false, false, true);
    }

    private void HandleSquatInput()
    {
        if (Input.GetAxis("Vertical") < 0 && isTransformed)
        {
            RevertTransformation();
        }
    }

    private void CheckTransformationTimer()
    {
        if (!isTransformed) return;

        timer += Time.deltaTime;
        if (timer >= changeTime)
        {
            timer = 0;
            RevertTransformation();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        foreach (ContactPoint2D contact in collision.contacts)
        {
            if (contact.normal.y > 0.7f)
            {
                isGrounded = true;
                UpdateAnimation(false, true, false);
                break;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("RespawnPoint"))
        {
            respawnPoint = collision.transform.position;
        }
        else if (collision.CompareTag("Gold"))
        {
            CollectGold(collision.gameObject);
        }
    }

    
    private void CollectGold(GameObject goldObject)
    {
        currentGold += goldValue;
        if (uiManager != null)
        {
            uiManager.UpdateGold(currentGold);
        }
        Destroy(goldObject);
    }

    public void TransformMario()
    {
        speed += 1;
        jumpForce.y += 2;
        doubleJumpForce.y += 2;
        transform.localScale = originalScale * 1.5f;
        isTransformed = true;
    }

    private void RevertTransformation()
    {
        speed = 5;
        jumpForce = new Vector2(0, 10);
        doubleJumpForce = new Vector2(0, 12);
        transform.localScale = originalScale;
        boxCollider.size = originalColliderSize;
        isTransformed = false;
    }

    public void ChangeHealth(int amount)
    {
        if (amount > 0 && currentHealth == maxHealth) return;

        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);

        if (currentHealth <= 0)
        {
            HandleDeath();
        }
        else if (amount < 0)
        {
            AudioManager.instance.PlayDeth(transform.position);
            StartCoroutine(RespawnRoutine());
        }
    }

    private IEnumerator RespawnRoutine()
    {
        canMotion = false;
        UpdateAnimation(false, false, false, true);

        yield return new WaitUntil(() =>
            animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f &&
            !animator.IsInTransition(0)
        );

        Respawn();
    }

    private void Respawn()
    {
        transform.position = respawnPoint;
        canMotion = true;
        RevertTransformation();
    }

    private void HandleDeath()
    {
        if (uiManager != null)
        {
            uiManager.ShowGameOver(transform.position);
        }

        SetMotion(false);

        // 通过LevelManager控制敌人
        if (levelManager != null && levelManager.enemyManager != null)
        {
            levelManager.enemyManager.SetChildrenMotion(false);
        }

        Destroy(gameObject, 1f);
    }


    public void SetMotion(bool isActive)
    {
        canMotion = isActive;
    }

    public List<int> GetHealthStatus()
    {
        return new List<int> { currentHealth, maxHealth };
    }

    public int GetGoldCount()
    {
        return currentGold / goldValue;
    }

    public void UpdateAnimation(bool isRunning, bool isIdle, bool isJumping, bool isDead = false)
    {
        animator.SetBool("isRun", isRunning);
        animator.SetBool("isIdle", isIdle);
        animator.SetBool("isJump", isJumping);
        animator.SetBool("isDead", isDead);
    }
}