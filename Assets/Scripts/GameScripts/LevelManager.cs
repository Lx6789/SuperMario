using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    [Header("关卡引用")]
    public Golds golds;
    public EnemyManager enemyManager;
    public SuperMario player;

    [Header("游戏数据")]
    public GameSO gameSO;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            // 根据需求决定是否持久化
            // DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        SceneManager.sceneLoaded += OnSceneLoaded;

        LoadSelectedLevel();
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        FindLevelReferences();
    }

    private void FindLevelReferences()
    {
        // 查找关卡中的关键对象
        player = FindObjectOfType<SuperMario>();
        golds = FindObjectOfType<Golds>();
        enemyManager = FindObjectOfType<EnemyManager>();
    }

    // 提供给外部获取关卡状态的接口
    public List<int> GetGoldStatus()
    {
        return golds != null ? golds.GetGoldStatus() : new List<int> { 0, 0 };
    }

    public List<int> GetEnemyStatus()
    {
        return enemyManager != null ? enemyManager.GetEnemyStatus() : new List<int> { 0, 0 };
    }

    //加载选中的关卡
    private void LoadSelectedLevel()
    {
        int levelId = gameSO.levelId;
        GameObject levelPrefab = Resources.Load<GameObject>("Level" + levelId);
        GameObject.Instantiate(levelPrefab);
    }
}
