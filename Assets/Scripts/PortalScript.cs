using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PortalScript : MonoBehaviour
{
    public void SceneLoad()
    {
        LoadingSceneManager.LoadScene("Restaurant_Close");
    }
}
