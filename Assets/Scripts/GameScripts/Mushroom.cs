using UnityEngine;
using System.Collections;

public class Mushroom : MonoBehaviour
{
    [Header("移动设置")]
    [SerializeField] private float moveDistance = 0.3f;  // 上浮距离
    [SerializeField] private float moveDuration = 1.0f;  // 上浮时间
    //[SerializeField] private float rotationSpeed = 30f;  // 旋转速度（可选）

    private bool isCollected = false;

    private void Start()
    {
        
    }

    /// <summary>
    /// 蘑菇上浮动画
    /// </summary>
    private IEnumerator RiseAnimation()
    {
        Vector2 startPos = transform.position;
        Vector2 targetPos = startPos + Vector2.up * moveDistance;
        float elapsed = 0f;

        while (elapsed < moveDuration)
        {
            transform.position = Vector2.Lerp(startPos, targetPos, elapsed / moveDuration);
            // 可选旋转效果
            //transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPos;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isCollected || !other.CompareTag("Player")) return;

        CollectMushroom(other.GetComponent<SuperMario>());
    }

    /// <summary>
    /// 收集蘑菇后的处理
    /// </summary>
    private void CollectMushroom(SuperMario player)
    {
        isCollected = true;

        if (player != null)
        {
            player.TransformMario(); // 触发玩家变身
        }

        // 播放收集特效（可扩展）
        // Instantiate(collectEffect, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }

    public void PlayRiseAnimation()
    {
        StartCoroutine(RiseAnimation());
    }
}