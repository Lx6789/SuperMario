using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Level : MonoBehaviour
{

    public GameObject lockGO;
    public GameObject unlockGO;

    public Sprite[] starSprites;

    //展示关卡样式
    public void ShowLevel(int starCount)
    {
        if (starCount < 0)
        {
            isLockOfLevel(true);
        }
        else { 
            if (starCount > 3) return;
            isLockOfLevel(false);
            gameObject.transform.Find("Unlock").transform.Find("star").GetComponent<Image>().sprite = starSprites[starCount];
        }
    }

    private void isLockOfLevel(bool isLock)
    {
        if (isLock) {
            lockGO.SetActive(true);
            unlockGO.SetActive(false);
        } else
        {
            lockGO.SetActive(false);
            unlockGO.SetActive(true);
        }
    }

    //点击关卡
    public void OnClickLevel()
    {
        GetComponentInParent<LevelUI>().Load();
    }

}
