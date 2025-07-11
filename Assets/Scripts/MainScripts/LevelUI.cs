using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUI : MonoBehaviour
{

    public GameObject mainUIs;
    public GameObject gameSO;
    public GameObject levels;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //返回主页
    public void BackMain()
    {
        gameObject.SetActive(false);
        mainUIs.SetActive(true);
    }

    //跟新关卡数据
    private void UpdateUIList()
    {

    }
}
