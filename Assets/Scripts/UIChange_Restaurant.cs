using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIChange_Restaurant : MonoBehaviour
{
    public GameObject CookUI, OrderListUI;
    public GameObject CloseButton, MoneyImage;
    bool isPause;

    private void Start()
    {
        isPause = false;
    }

    private void Pause_Restart_Game()
    {
        if (!isPause)
        {
            Time.timeScale = 0.0f;
            isPause = true;
        }
        else
        {
            Time.timeScale = 1.0f;
            isPause = false;
        }
    }
    public void CookUIONOFF()
    {
        CookUI.SetActive(!CookUI.activeSelf);
    }
    public void OrderListUIONOFF()
    {
        OrderListUI.SetActive(!OrderListUI.activeSelf);
    }
    public void ButtonImageONOFF()
    {
        CloseButton.SetActive(!CloseButton.activeSelf);
        MoneyImage.SetActive(!MoneyImage.activeSelf);
    }

    public void UIChange()
    {
        //Pause_Restart_Game();
        CookUIONOFF();
        OrderListUIONOFF();
        ButtonImageONOFF();
    }
}
