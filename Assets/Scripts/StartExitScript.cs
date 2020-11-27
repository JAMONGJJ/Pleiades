using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartExitScript : MonoBehaviour
{
    public void StartGame()
    {
        LoadingSceneManager.LoadScene("Village");
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
