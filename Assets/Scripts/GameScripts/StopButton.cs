using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopButton : MonoBehaviour
{

    private UIManager uiManager;

    private void Start()
    {
        uiManager = GetComponent<UIManager>();
    }

    public void OnClicked()
    {
        uiManager.OnStopButtonClicked();
    }
}
