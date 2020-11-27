using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelChange : MonoBehaviour
{
    // Panels index info
    // 0 : Chicken Panel
    // 1 : Pork Panel
    // 2 : Beef Panel
    // 3 : Others Panel
    // 4 : Items Panel
    // 5 : Recipes Panel
    // 6 : Main quest Panel
    // 7 : Sub quest Panel
    public GameObject[] Panels;

    public void ItemsPanelOpen()
    {
        for (int i = 0; i < Panels.Length; i++)
            Panels[i].SetActive(false);
        Panels[0].SetActive(true);
    }

    public void RecipesPanelOpen()
    {
        for (int i = 0; i < Panels.Length; i++)
            Panels[i].SetActive(false);
        Panels[1].SetActive(true);
    }

    public void MainQuestPanelOpen()
    {
        for (int i = 0; i < Panels.Length; i++)
            Panels[i].SetActive(false);
        Panels[2].SetActive(true);
    }

    public void SubQuestPanelOpen()
    {
        for (int i = 0; i < Panels.Length; i++)
            Panels[i].SetActive(false);
        Panels[3].SetActive(true);
    }
}
