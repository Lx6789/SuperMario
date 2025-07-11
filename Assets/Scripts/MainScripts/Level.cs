using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level : MonoBehaviour
{

    public GameObject unlockGo;
    public GameObject lockGo;
    public Image star;
    public Sprite[] stars;

    //public int starsCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Show(starsCount);
    }

    //关卡图片 starCount（-1 已锁； >=0 解锁）
    public void  Show(int starCount)
    {
        if (starCount < 0)
        {
            unlockGo.SetActive(false);
            lockGo.SetActive(true);
        }
        else {
            if (starCount > 3) return;
            star.sprite = stars[starCount];
        }
    }

    public void OnClick()
    {

    }
}
