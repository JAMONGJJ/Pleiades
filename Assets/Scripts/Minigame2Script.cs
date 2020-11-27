using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Minigame2Script : MinigameScript
{
    public Slider slider;
    public GameObject HitpointPrefab;
    public GameObject HitpointParent;

    private int[,] arr = new int[,] { { 0, 1, 2 }, { 0, 2, 1 }, { 1, 0, 2 }, { 1, 2, 0 }, { 2, 0, 1 }, { 2, 1, 0 } };
    private Vector3[] hitpointPosition;
    private float speed;
    private bool flag;                      // true : 슬라이더 value값 상승 / false : 슬라이더 value값 감소
    private int phase;
    private Vector3 currentPos;

    int columnNum;
    int clicked;                            // 슬라이더의 Hitpoint 위치에서 버튼이 눌린 횟수(3번이 되어야 미니게임 성공)
    GameObject hitpointInstance;
    float width;

    // Start is called before the first frame update
    private void OnEnable()
    {
        width = slider.GetComponent<RectTransform>().sizeDelta.x;
        hitpointPosition = new Vector3[] { new Vector3(width / 4, 0.0f, 0.0f), new Vector3(width / 4 * 2, 0.0f, 0.0f), new Vector3(width / 4 * 3, 0.0f, 0.0f) };
        phase = 0;
        flag = true;
        speed = 0.2f;
        clicked = 0;
        minigameFinished = false;
        columnNum = (int)UnityEngine.Random.Range(0, 5);
        hitpointInstance = Instantiate(HitpointPrefab, hitpointPosition[arr[columnNum, phase]], Quaternion.identity);
        hitpointInstance.transform.SetParent(HitpointParent.transform, false);
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (!minigameFinished)
        {
            if (clicked == 3)
            {
                MinigameSuccess(2);
            }

            if (flag)
            {
                slider.value += Time.deltaTime * speed;
            }
            else if (!flag)
            {
                slider.value -= Time.deltaTime * speed;
            }

            if (slider.value >= 1.0f)
            {
                flag = false;
                phase++;
                hitpointInstance.GetComponent<RectTransform>().anchoredPosition = hitpointPosition[arr[columnNum, phase]];
                hitpointInstance.SetActive(true);
                if (phase != clicked)
                    MinigameFail();
            }
            else if (slider.value <= 0.0f)
            {
                flag = true;
                phase++;
                hitpointInstance.GetComponent<RectTransform>().anchoredPosition = hitpointPosition[arr[columnNum, phase]];
                hitpointInstance.SetActive(true);
                if (phase != clicked)
                    MinigameFail();
            }
        }
    }

    public void HitpointTouched()
    {
        float tmpX = hitpointInstance.GetComponent<RectTransform>().anchoredPosition.x;

        if (slider.value >= (tmpX - 25) / width && slider.value <= (tmpX + 25) / width)
        {
            clicked++;
            hitpointInstance.SetActive(false);
        }
        else
        {
            MinigameFail();
        }
    }

    protected override void MinigameSuccess(int n)
    {
        DatabaseScript.Add_Inventory(3, 5);
        base.MinigameSuccess(n);
    }

    protected override void MinigameFail()
    {
        base.MinigameFail();
    }

    protected override void ExitMinigame()
    {
        base.ExitMinigame();
    }

    protected override void TutorialMessageONOFF()
    {
        base.TutorialMessageONOFF();
    }
}
