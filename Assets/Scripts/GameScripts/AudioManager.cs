using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//音效控制器
public class AudioManager : MonoBehaviour
{
    
    public static AudioManager instance;

    [Header("声音资源")]
    public AudioClip bumpBrick;
    public AudioClip coin;
    public AudioClip deth;
    public AudioClip eatFood;
    public AudioClip jump;
    public AudioClip win;
    public AudioClip gameOver;
    public AudioSource backgroundMusicSource;

    private bool isWin = false;

    private void Awake()
    {
        instance = this;
    }

    public void PlayBumpBrick(Vector3 position)
    {
        AudioSource.PlayClipAtPoint(bumpBrick, position, 5f);
    }

    public void PlayCoin(Vector3 position)
    {
        AudioSource.PlayClipAtPoint(coin, position);
    }

    public void PlayDeth(Vector3 position)
    {
        if (backgroundMusicSource != null && backgroundMusicSource.isPlaying)
        {
            backgroundMusicSource.Pause();
        }
        AudioSource.PlayClipAtPoint(deth, position);
        // 计算死亡音效的时长并延迟恢复背景音乐
        float deathClipLength = deth.length;
        Invoke("ResumeBackgroundMusic", deathClipLength);
    }

    public void PlayEatFood(Vector3 position)
    {
        AudioSource.PlayClipAtPoint(eatFood, position);
    }

    public void PlayJump(Vector3 position)
    {
        AudioSource.PlayClipAtPoint(jump, position);
    }

    public void PlayWin(Vector3 position)
    {
        isWin = true;
        if (backgroundMusicSource != null && backgroundMusicSource.isPlaying)
        {
            backgroundMusicSource.Pause();
        }
        AudioSource.PlayClipAtPoint(win, position);
    }

    public void PlayGameOver(Vector3 position)
    {
        if (isWin) return;
        if (backgroundMusicSource != null && backgroundMusicSource.isPlaying)
        {
            backgroundMusicSource.Pause();
        }
        AudioSource.PlayClipAtPoint(gameOver, position);
    }

    private void ResumeBackgroundMusic()
    {
        // 恢复背景音乐
        if (backgroundMusicSource != null && !backgroundMusicSource.isPlaying)
        {
            backgroundMusicSource.Play();
        }
    }
}
