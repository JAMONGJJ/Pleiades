using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestaurantScript : MonoBehaviour
{
    public void OpenRestaurant()
    {
        LoadingSceneManager.LoadScene("Restaurant_Open");
    }

    public void ExitRestaurant()
    {
        GameObject.FindWithTag("Database").GetComponent<BDDPositionScript>().enabled = true;
        BDDPositionScript.index = 1;
        LoadingSceneManager.LoadScene("Village");
    }

    public void CloseRestaurant()
    {
        LoadingSceneManager.LoadScene("Restaurant_Close");
    }
}
