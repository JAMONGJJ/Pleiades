using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingCanvas : MonoBehaviour
{
    public GameObject mainCanvas;
    public GameObject settingCanvas;

    // Start is called before the first frame update
    void Start()
    {

    }
    
    public void EnableSettingCanvas()
    {
        settingCanvas.SetActive(true);
        mainCanvas.SetActive(false);
    }

    public void DisableSettingCanvas()
    {
        settingCanvas.SetActive(false);
        mainCanvas.SetActive(true);
    }
}
