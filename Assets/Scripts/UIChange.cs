using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


// UI 전반적인 상황에 맞는 아이콘들을 노출시키고 숨기는 역할 수행, 더해서 시스템 메세지 출력
public class UIChange : MonoBehaviour
{
    public GameObject MainButtons;          // 기본 버튼들
    public GameObject UIButton;
    public GameObject PortalButton, DialogButton, MinigameButton;
    public GameObject ProfileUI, InventoryUI, DictionaryUI, QuestUI, SettingsUI, DialogUI, MinigameUI;
    public GameObject Minimap, Worldmap;
    public GameObject SM;
    public TextMeshProUGUI systemMessage;

    private bool isPause = false;

    // 모든 버튼과 UI 비활성화 후 초기상태로.
    public void ResetCanvas()
    {
        MainButtons.SetActive(true);
        UIButton.SetActive(true);
        PortalButton.SetActive(false);
        DialogButton.SetActive(false);
        MinigameButton.SetActive(false);
        ProfileUI.SetActive(false);
        InventoryUI.SetActive(false);
        DictionaryUI.SetActive(false);
        QuestUI.SetActive(false);
        SettingsUI.SetActive(false);
        DialogUI.SetActive(false);
        MinigameUI.SetActive(false);
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
    private void MainButtonsONOFF()
    {
        MainButtons.SetActive(!MainButtons.activeSelf);
    }
    private void ProfileUIONOFF()
    {
        ProfileUI.SetActive(!ProfileUI.activeSelf);
    }
    private void InventoryUIONOFF()
    {
        InventoryUI.SetActive(!InventoryUI.activeSelf);
    }
    private void DictionaryUIONOFF()
    {
        DictionaryUI.SetActive(!DictionaryUI.activeSelf);
    }
    private void QuestUIONOFF()
    {
        QuestUI.SetActive(!QuestUI.activeSelf);
    }
    private void SettingsUIONOFF()
    {
        SettingsUI.SetActive(!SettingsUI.activeSelf);
    }
    private void DialogUIONOFF()
    {
        DialogUI.SetActive(!DialogUI.activeSelf);
    }
    private void CookUIONOFF()
    {
        ProfileUI.SetActive(!ProfileUI.activeSelf);
    }
    private void MinigameUIONOFF()
    {
        MinigameUI.SetActive(!MinigameUI.activeSelf);
    }
    private void MinimapUIONOFF()
    {
        Minimap.SetActive(!Minimap.activeSelf);
    }
    private void WorldmapUIONOFF()
    {
        Worldmap.SetActive(!Worldmap.activeSelf);
    }

    public void ProfileONOFF()
    {
        ProfileUIONOFF();
        MainButtonsONOFF();
        MinimapUIONOFF();
    }
    public void InventoryONOFF()
    {
        InventoryUIONOFF();
        MainButtonsONOFF();
        MinimapUIONOFF();
    }
    public void DictionaryONOFF()
    {
        DictionaryUIONOFF();
        MainButtonsONOFF();
        MinimapUIONOFF();
    }
    public void SettingsONOFF()
    {
        SettingsUIONOFF();
        MainButtonsONOFF();
        MinimapUIONOFF();
    }
    public void DialogONOFF()
    {
        DialogUIONOFF();
        MainButtonsONOFF();
        MinimapUIONOFF();
    }
    public void QuestONOFF()
    {
        QuestUIONOFF();
        MainButtonsONOFF();
        MinimapUIONOFF();
    }
    public void MinigameONOFF()
    {
        MinigameUIONOFF();
        MainButtonsONOFF();
        MinimapUIONOFF();
    }
    public void WorldmapONOFF()
    {
        WorldmapUIONOFF();
        MainButtonsONOFF();
        MinimapUIONOFF();
    }

    public void QuitButtonClicked()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
    
    // 상황에 맞게 시스템 메세지 출력
    private void SystemMessageON(int index)
    {
        string text = "";   //  시스템 메세지로 출력될 텍스트
        switch (index)
        {
            case 1:     // 퀘스트 수락 메세지
                text = "Quest Accepted!";
                break;
            case 2:     // 퀘스트 완료 메세지
                text = "Quest Success!";
                break;
            default:
                break;
        }
        systemMessage.text = text;
        SM.SetActive(true);
        StartCoroutine("TurnOffMessage");
    }
    IEnumerator TurnOffMessage()
    {
        yield return new WaitForSeconds(2.0f);
        SM.SetActive(false);
    }

    // 다른 스크립트에서 호출되는 함수들, SystemMessageON 함수를 호출해 필요한 메세지 출력
    public void CookCompleteMessage()
    {
        SystemMessageON(1);
    }
    public void QuestAcceptMessage()
    {
        SystemMessageON(2);
    }
    public void QuestCompleteMessage()
    {
        SystemMessageON(3);
    }
    public void MinigameFailMessage()
    {
        SystemMessageON(4);
    }
    public void Minigame1SuccessMessage()
    {
        SystemMessageON(5);
    }
}
