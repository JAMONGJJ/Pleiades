using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Minigame4Script : MinigameScript
{
    public Button buttonA, buttonB;
    public Slider slider;

    float speed;

    // Start is called before the first frame update
    private void OnEnable()
    {
        speed = 0.1f;
        playTime = 20.0f;
        time = 0.0f;
        minigameFinished = false;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        TimerText.text = (Math.Truncate(playTime - time)).ToString();

        if (!minigameFinished)
        {
            if (time <= playTime)
                time += Time.deltaTime;

            if (slider.value >= 1.0f && time < playTime)
            {
                MinigameSuccess(4);
            }
            else if (time >= playTime)
            {
                MinigameFail();
            }

            slider.value -= Time.deltaTime * speed;
        }
    }

    public void ButtonATouched()
    {
        if (!minigameFinished)
        {
            slider.value += 0.05f;
            buttonA.gameObject.SetActive(false);
            buttonB.gameObject.SetActive(true);
        }
    }

    public void ButtonBTouched()
    {
        if (!minigameFinished)
        {
            slider.value += 0.1f;
            buttonB.gameObject.SetActive(false);
            buttonA.gameObject.SetActive(true);
        }
    }

    protected override void MinigameSuccess(int n)
    {
        DatabaseScript.Add_Inventory(4, 5);
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
