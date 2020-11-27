using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToMainScript : MonoBehaviour
{
    public void ToMainScene()
    {
        SceneManager.LoadScene("StartScene");
    }
}
