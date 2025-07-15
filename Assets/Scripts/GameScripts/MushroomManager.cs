using UnityEngine;
using System.Collections;

public class MushroomManager : MonoBehaviour
{
    [Header("蘑菇设置")]
    [SerializeField] private GameObject mushroomPrefab;
    [SerializeField] private float generationCooldown = 5f; // 生成冷却时间

    private bool canGenerate = true;
    private Coroutine currentCoroutine;

    /// <summary>
    /// 在指定位置生成蘑菇
    /// </summary>
    public void GenerateAtPosition(Vector2 spawnPosition)
    {
        if (!canGenerate || mushroomPrefab == null) return;

        StartGenerationCycle(spawnPosition);
    }

    private void StartGenerationCycle(Vector2 spawnPosition)
    {
        canGenerate = false;

        GameObject newMushroom = Instantiate(
            mushroomPrefab,
            spawnPosition,
            Quaternion.identity,
            transform // 作为子物体生成
        );

        // 自动触发蘑菇的上浮动画
        if (newMushroom.TryGetComponent(out Mushroom mushroom))
        {
            mushroom.PlayRiseAnimation();
        }

        // 启动冷却计时
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }
        currentCoroutine = StartCoroutine(GenerationCooldown());
    }

    /// <summary>
    /// 生成冷却计时器
    /// </summary>
    private IEnumerator GenerationCooldown()
    {
        yield return new WaitForSeconds(generationCooldown);
        canGenerate = true;
        currentCoroutine = null;
    }

    /// <summary>
    /// 强制重置生成器状态
    /// </summary>
    public void ResetGenerator()
    {
        canGenerate = true;
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
            currentCoroutine = null;
        }
    }
}