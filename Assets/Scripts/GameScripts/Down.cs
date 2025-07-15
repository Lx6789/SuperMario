using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Down : MonoBehaviour
{
    [Header("玩家引用")]
    [SerializeField] private SuperMario player;
    [SerializeField] private float deathAnimationDelay = 0.167f; // 匹配死亡动画时长

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;

        StartCoroutine(HandlePlayerCollision());
    }

    private IEnumerator HandlePlayerCollision()
    {
        if (player == null) yield break;

        // 触发死亡动画
        player.UpdateAnimation(false, false, false, true);

        // 等待动画播放
        yield return new WaitForSeconds(deathAnimationDelay);

        // 执行后续逻辑
        player.ChangeHealth(-1);
    }

    // 自动获取引用（如果Inspector未赋值）
    private void Awake()
    {
        if (player == null)
        {
            player = FindObjectOfType<SuperMario>();
        }
    }
}