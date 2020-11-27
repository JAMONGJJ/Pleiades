using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MinigameScript : MonoBehaviour
{
    [SerializeField]
    protected TextMeshProUGUI TimerText;
    [SerializeField]
    protected TextMeshProUGUI RemainCountText;
    [SerializeField]
    protected GameObject AnimalPrefab;
    [SerializeField]
    protected GameObject SM;
    [SerializeField]
    protected TextMeshProUGUI systemMessage;
    [SerializeField]
    protected GameObject TutorialMessage;

    protected float playTime;                   // 미니게임 제한시간
    protected float time;                        // 미니게임 플레이시간
    protected int Count;                         // 남은 동물의 수
    protected bool initiated;                   // true : 미니게임 시작함 / false : 미니게임 시작안함
    protected bool minigameFinished;

    protected virtual void SpawnAnimal()
    {
        for (int i = 0; i < Count; i++)
        {
            Vector3 tmpPos = new Vector3(UnityEngine.Random.Range(-10.0f, -2.0f), 0.0f, UnityEngine.Random.Range(-12.0f, 7.0f));
            var tmp = Instantiate(AnimalPrefab, tmpPos, Quaternion.identity);
        }
    }

    protected virtual void MinigameSuccess(int n)
    {
        minigameFinished = true;
        SystemMessageON(n);
    }

    protected virtual void MinigameFail()
    {
        minigameFinished = true;
        SystemMessageON(0);
    }

    public virtual void GetAnimals()
    {
        Count--;
    }

    protected virtual void ExitMinigame()
    {
        GameObject.FindWithTag("Database").GetComponent<BDDPositionScript>().enabled = true;
        LoadingSceneManager.LoadScene("Village");
    }

    // 상황에 맞게 시스템 메세지 출력
    protected void SystemMessageON(int index)
    {
        string text = "";   //  시스템 메세지로 출력될 텍스트
        switch (index)
        {
            case 0:     // 미니게임 실패 메세지
                text = "Minigame fail!\nExit minigame...";
                break;
            case 1:     // 미니게임1 성공 메세지
                text = "Minigame clear!\nYou earned chickens as a price.";
                break;
            case 2:     // 미니게임1 성공 메세지
                text = "Minigame clear!\nYou earned eggs as a price.";
                break;
            case 3:     // 미니게임1 성공 메세지
                text = "Minigame clear!\nYou earned porks as a price.";
                break;
            case 4:     // 미니게임1 성공 메세지
                text = "Minigame clear!\nYou earned milks as a price.";
                break;
            case 5:     // 미니게임1 성공 메세지
                text = "Minigame clear!\nYou earned beefs as a price.";
                break;
            default:
                break;
        }
        systemMessage.text = text;
        SM.SetActive(true);
        StartCoroutine("SystemMessageOFF_Minigame1");
    }
    IEnumerator SystemMessageOFF_Minigame1()
    {
        yield return new WaitForSeconds(2.0f);
        SM.SetActive(false);
        ExitMinigame();
    }

    protected virtual void TutorialMessageONOFF()
    {
        if (Time.timeScale == 0.0f)
            Time.timeScale = 1.0f;
        else
            Time.timeScale = 0.0f;
        TutorialMessage.SetActive(!TutorialMessage.activeSelf);
    }
}
