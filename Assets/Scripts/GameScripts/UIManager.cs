using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager: MonoBehaviour
{
    public Text healthText;
    public Text goldText;
    public Text killText;

    public SuperMario superMario;
    public Enemie1 enemy1;
    public Enemy2 enemy2;
    public Golds golds;
    public EnemyManager enemyManager;

    public GameObject stopPanel;

    public GameObject panel;

    private int health;
    private int killNumber;
    //private int starNumber;

    public Sprite[] stars;
    //public GameObject winStar;
    //public GameObject gameOverStar;

    public int starNumber;

    public Image starImage;
    
    private void Start()
    {
        health = 0;
        killNumber = 0;
        starNumber = 0;
        UpdateGold(0);
    }

    private void Update()
    {
        UpdateHealth();
    }

    //修改UI血量
    private void UpdateHealth()
    {
        health = superMario.currentHealth;
        if (superMario.currentHealth <= 0)
        {
            health = 0;
        }
        healthText.text = "X" + health;
    }

    //修改金币分数
    public void UpdateGold(int gold)
    {
        goldText.text = gold.ToString();
    }

    //修改杀敌数量
    public void UpdateKillText()
    {
        //Debug.Log("A" + killNumber);
        killNumber++;
        //Debug.Log("B" + killNumber);

        killText.text = killNumber.ToString();
    }

    //暂停游戏
    public void ColickStopButton()
    {
        //启用面板
        stopPanel.SetActive(true);
        //停止其他脚本
        superMario.setMotion(false);
        enemy1.setMotion(false);
        enemy2.setMotion(false);
    }

    public void CollickStartButton()
    {
        stopPanel.SetActive(false);
        //停止其他脚本
        superMario.setMotion(true);
        enemy1.setMotion(true);
        enemy2.setMotion(true);
    }

    //从新开始
    public void CollickRestatyButton()
    {
        // 获取当前场景的名字或索引
        Scene currentScene = SceneManager.GetActiveScene();
        // 重新加载当前场景
        SceneManager.LoadScene(currentScene.buildIndex);
    }

    //列表
    public void CollickListButton()
    {
        //加载主场景
        SceneManager.LoadScene(0);
    }

    //星星数量计算
    public int Star()
    {
        //一次没死的一颗星，把怪杀完的一颗星，把金币全收集完的一颗星
        List<int> health = new List<int>();
        List<int> gold = new List<int>();
        List<int> enemy = new List<int>();
        health = superMario.getHealth();
        gold = golds.getGuldNumber();
        enemy = enemyManager.getEnemyNumber();
        Debug.Log(enemy[0] + "  " + enemy[1]);
        Debug.Log("gold0 1 " + gold[0] + " " + gold[1]);
        Debug.Log("health0 1 " + health[0] + " " + health[1]);
        if (health[0] == health[1])
        {
            starNumber++;
            Debug.Log("aa");
        }
        if (gold[0] == gold[1])
        {
            starNumber++;
            Debug.Log("bb");
        }
        if (enemy[0] == 0)
        {
            starNumber++;
            Debug.Log("cc");
        }
        Debug.Log(starNumber);
        return starNumber;
    }

    public void CollickList()
    {
        LoadLevel("MainScene");
    }

    public void LoadLevel(string sceneName)
    {
        StartCoroutine(LoadSceneAfterDelay(sceneName));
    }

    private IEnumerator LoadSceneAfterDelay(string sceneName)
    {
        Debug.Log("bbb");
        // 实际上，场景加载是异步的，因此我们应该使用 SceneManager.LoadSceneAsync
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        //Debug.Log("bbb");
        // 等待场景加载完成
        while (!asyncLoad.isDone)
        {
            yield return null; // 每帧等待
        }
        //Debug.Log("aaa");
        // 获取新场景中某个组件并调用它的方法（在这里我们要确保这个方法是存在的）。
        GameObject uiManager = GameObject.Find("UIManager"); // 替换为目标对象名称
        if (uiManager != null)
        {
            MainUIManager mainUIManager = uiManager.GetComponent<MainUIManager>();
            if (mainUIManager != null)
            {
                mainUIManager.OpenLevelUI();
            }
            else
            {
                Debug.LogError("MainUIManager component not found on the object!");
            }
        }
        else
        {
            Debug.LogError("Object 'YourSceneObjectName' not found in the new scene!");
        }
    }

    public void GameOver()
    {
        starNumber = Star();
        Debug.Log("star" + starNumber);
        Debug.Log("stars.Length" + stars.Length);
        if (starNumber > stars.Length) return;
        starImage.sprite = stars[starNumber];
        Debug.Log("panel" + panel);
        panel.SetActive(true);
        enemy1.setMotion(false);
        enemy2.setMotion(false);
    }

}
