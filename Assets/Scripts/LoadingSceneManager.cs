using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.EventSystems;

public class LoadingSceneManager : MonoBehaviour
{
    public static string nextScene;
    [SerializeField]
    Slider ProgressBar;
    [SerializeField]
    TextMeshProUGUI text;
    AsyncOperation op;

    bool totheNextScene;

    private void Start()
    {
        totheNextScene = false;
        text.text = "Loading....";
        StartCoroutine(LoadScene());
    }

    private void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0) && EventSystem.current.IsPointerOverGameObject())
            totheNextScene = true;
    }

    public static void LoadScene(string sceneName)
    {
        nextScene = sceneName;
        SceneManager.LoadScene("LoadingScene");
    }
    IEnumerator LoadScene()
    {
        yield return null;
        op = SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = false;

        float timer = 0.0f;

        System.GC.Collect();

        while (!op.isDone)
        {
            yield return null;
            timer += Time.deltaTime;
            if (op.progress >= 0.9f)
            {
                ProgressBar.value = Mathf.Lerp(ProgressBar.value, 1f, timer);
                if (ProgressBar.value >= 1.0f)
                {
                    text.text = "Loading completed!\nTouch to progress.";
                    if (totheNextScene)
                        op.allowSceneActivation = true;
                }
            }
            else
            {
                ProgressBar.value = Mathf.Lerp(ProgressBar.value, op.progress, timer);
                if (ProgressBar.value >= op.progress)
                    timer = 0.0f;
            }

        }
    }
}
