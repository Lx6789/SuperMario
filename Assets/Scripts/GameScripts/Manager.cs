using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 将Manager设为静态类
public static class Manager
{
    // 使用静态变量来存储UI元素
    public static Text healthText;
    public static Text goldText;
    public static Text killText;

    public static Golds golds;

    public static GameObject stopPanel;
    public static GameObject gameOver;

    public static Image starImage;

    // 用于初始化静态变量，一般在初始化的时候使用
    public static void Initialize(Text health, Text gold, Text kill, Golds _golds, GameObject stop, GameObject gameOverPanel, Image star)
    {
        healthText = health;
        goldText = gold;
        killText = kill;
        golds = _golds;
        stopPanel = stop;
        gameOver = gameOverPanel;
        starImage = star;
    }

    public static Text GetHealthText()
    {
        return healthText;
    }

    public static Text GetGoldText()
    {
        return goldText;
    }

    public static Text GetKillText()
    {
        return killText;
    }

    public static Golds GetGolds()
    {
        return golds;
    }

    public static GameObject GetStopPanel()
    {
        return stopPanel;
    }

    public static GameObject GetGameOver()
    {
        return gameOver;
    }

    public static Image GetStarImage()
    {
        return starImage;
    }
}