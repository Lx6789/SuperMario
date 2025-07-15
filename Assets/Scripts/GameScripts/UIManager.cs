using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("玩家引用")]
    public SuperMario superMario;

    [Header("敌人引用")]
    public EnemyManager enemyManager;

    [Header("UI元素")]
    public Text healthText;       // 生命值文本
    public Text goldText;         // 金币文本
    public Text killText;         // 击杀数文本
    public Golds golds;           // 金币系统
    public GameObject stopPanel;  // 暂停面板
    public GameObject gameOverPanel; // 游戏结束面板
    public Image starImage;       // 星级图标

    [Header("游戏数据")]
    public GameSO gameSO;         // 游戏脚本化对象
    public Sprite[] stars;        // 星级图标数组

    private int _currentHealth;
    private int _killCount;
    private int _starCount;

    private void Start()
    {
        InitializeUI();
    }

    // 初始化UI状态
    private void InitializeUI()
    {
        _currentHealth = 0;
        _killCount = 0;
        _starCount = 0;
        UpdateGold(0);

        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);
    }

    private void Update()
    {
        UpdateHealth();
    }

    // 更新生命值显示
    private void UpdateHealth()
    {
        _currentHealth = Mathf.Max(0, superMario.currentHealth);
        healthText.text = "X" + _currentHealth;
    }

    // 更新金币显示
    public void UpdateGold(int gold)
    {
        goldText.text = gold.ToString();
    }

    // 更新击杀数
    public void UpdateKillText()
    {
        _killCount++;
        killText.text = _killCount.ToString();
    }

    // 点击暂停按钮
    public void OnStopButtonClicked()
    {
        stopPanel.SetActive(true);
        SetGameObjectsActive(false);
    }

    // 点击继续按钮
    public void OnContinueButtonClicked()
    {
        stopPanel.SetActive(false);
        SetGameObjectsActive(true);
    }

    // 点击重新开始
    public void OnRestartButtonClicked()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }

    // 点击关卡选择
    public void OnLevelSelectButtonClicked()
    {
        SceneManager.LoadScene("Level");
    }

    // 设置游戏对象活动状态
    private void SetGameObjectsActive(bool isActive)
    {
        if (superMario != null)
            superMario.SetMotion(isActive);

        enemyManager.SetChildrenMotion(isActive);
    }

    // 计算获得星级
    public int CalculateStars()
    {
        _starCount = 0;

        List<int> health = superMario.GetHealthStatus();
        List<int> gold = golds.GetGoldStatus();
        List<int> enemy = enemyManager.GetEnemyStatus();

        // 无伤通关
        if (health[0] == health[1])
            _starCount++;

        // 收集全部金币
        if (gold[0] == gold[1])
            _starCount++;

        // 消灭所有敌人
        if (enemy[0] == 0)
            _starCount++;

        return _starCount;
    }

    // 显示游戏结束界面
    public void ShowGameOver()
    {
        _starCount = CalculateStars();

        if (_starCount >= 0 && _starCount < stars.Length)
            starImage.sprite = stars[_starCount];

        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);
    }
}
