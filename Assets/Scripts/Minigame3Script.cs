using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Minigame3Script : MinigameScript
{
    // Start is called before the first frame update
    private void OnEnable()
    {
        initiated = false;
        playTime = 23.0f;
        time = 0.0f;
        Count = 5;
        minigameFinished = false;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        TimerText.text = (Math.Truncate(playTime - time)).ToString();
        RemainCountText.text = Count.ToString();

        if (!minigameFinished)
        {
            if (time <= playTime)
                time += Time.deltaTime;

            if (Count <= 0 && time < playTime)
            {
                MinigameSuccess(3);
            }
            else if (time >= playTime && Count > 0)
            {
                MinigameFail();
            }
        }

        if ((playTime - time) <= playTime - 3.0f && !initiated)
        {
            SpawnAnimal();
            initiated = true;
        }
    }

    protected override void SpawnAnimal()
    {
        base.SpawnAnimal();
    }

    protected override void MinigameSuccess(int n)
    {
        DatabaseScript.Add_Inventory(1, 5);
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

    public override void GetAnimals()
    {
        base.GetAnimals();
    }

    protected override void TutorialMessageONOFF()
    {
        base.TutorialMessageONOFF();
    }
}
