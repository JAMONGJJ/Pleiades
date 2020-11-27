using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Minigame1Script : MinigameScript
{
    private void Start()
    {
        initiated = false;
        minigameFinished = false;
        playTime = 33.0f;
        time = 0.0f;
        Count = 10;
    }

    private void FixedUpdate()
    {
        TimerText.text = (Math.Truncate(playTime - time)).ToString();
        RemainCountText.text = Count.ToString();

        if (!minigameFinished)
        {
            if (time <= playTime)
                time += Time.deltaTime;

            if (Count <= 0 && time < 33.0f)
            {
                MinigameSuccess(1);
            }
            else if (time >= 33.0f && Count > 0)
            {
                MinigameFail();
            }
        }

        if((playTime - time) <= 30.0f && !initiated)
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
        DatabaseScript.Add_Inventory(0, 5);
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
