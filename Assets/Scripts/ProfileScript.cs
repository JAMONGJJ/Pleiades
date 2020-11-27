using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ProfileScript : MonoBehaviour
{
    public TextMeshProUGUI cookedCuisineNumber;
    public TextMeshProUGUI completedQuestNumber;
    public TextMeshProUGUI collectedRecipesNumber;

    [HideInInspector]
    public int ccn;
    [HideInInspector]
    public int cqn;
    [HideInInspector]
    public int crn;

    private void OnEnable()
    {
        ccn = cqn = crn = 0;
        cookedCuisineNumber.text = DatabaseScript.cookedCuisineNumber.ToString();
        completedQuestNumber.text = DatabaseScript.completedQuestNumber.ToString();
        collectedRecipesNumber.text = DatabaseScript.collectedRecipesNumber.ToString();
    }
}
