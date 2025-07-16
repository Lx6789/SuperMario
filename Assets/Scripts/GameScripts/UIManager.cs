using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("UI元素")]
    public Text healthText;
    public Text goldText;
    public Text killText;
    public GameObject stopPanel;
    public GameObject gameOverPanel;
    public Image starImage;

    [Header("游戏数据")]
    public GameSO gameSO;
    public Sprite[] stars;

    private int _currentHealth;
    private int _killCount;
    private int _starCount;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        InitializeUI();
    }

    // 当关卡引用更新时调用
    public void OnLevelReferencesUpdated()
    {
        // 可以在这里更新UI或执行其他操作
        UpdateHealth();
    }

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

    private void UpdateHealth()
    {
        if (LevelManager.Instance != null && LevelManager.Instance.player != null)
        {
            _currentHealth = Mathf.Max(0, LevelManager.Instance.player.currentHealth);
            healthText.text = "X" + _currentHealth;
        }
    }

    public void UpdateGold(int gold)
    {
        goldText.text = gold.ToString();
    }

    public void UpdateKillText()
    {
        _killCount++;
        killText.text = _killCount.ToString();
    }

    public void OnStopButtonClicked()
    {
        stopPanel.SetActive(true);
        SetGameObjectsActive(false);
    }

    public void OnContinueButtonClicked()
    {
        stopPanel.SetActive(false);
        SetGameObjectsActive(true);
    }

    public void OnRestartButtonClicked()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }

    public void OnLevelSelectButtonClicked()
    {
        SceneManager.LoadScene("Level");
    }

    private void SetGameObjectsActive(bool isActive)
    {
        if (LevelManager.Instance != null && LevelManager.Instance.player != null)
            LevelManager.Instance.player.SetMotion(isActive);

        if (LevelManager.Instance != null && LevelManager.Instance.enemyManager != null)
            LevelManager.Instance.enemyManager.SetChildrenMotion(isActive);
    }

    public int CalculateStars()
    {
        _starCount = 0;

        if (LevelManager.Instance == null || LevelManager.Instance.player == null)
            return _starCount;

        List<int> health = LevelManager.Instance.player.GetHealthStatus();
        List<int> gold = LevelManager.Instance.GetGoldStatus();
        List<int> enemy = LevelManager.Instance.GetEnemyStatus();

        if (health[0] == health[1]) _starCount++;
        if (gold[0] == gold[1]) _starCount++;
        if (enemy[0] == 0) _starCount++;

        return _starCount;
    }

    public void ShowGameOver(Vector3 position)
    {

        AudioManager.instance.PlayGameOver(position);

        _starCount = CalculateStars();
        if (gameSO.starNumberOfLevel[gameSO.levelId - 1] <= _starCount)
            gameSO.starNumberOfLevel[gameSO.levelId - 1] = _starCount;

        if (_starCount >= 0 && _starCount < stars.Length)
            starImage.sprite = stars[_starCount];

        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);
    }
}