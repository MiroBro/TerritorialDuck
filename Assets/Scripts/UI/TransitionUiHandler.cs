using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionUiHandler : MonoBehaviour
{
    public GameObject tranUi;
    public CanvasGroup cv;

    public static string currentScene;
    public static string sceneToLoad;

    private void Start()
    {
        StartCoroutine(SmoothSceneChange(0.5f));
    }

    IEnumerator SmoothSceneChange(float duration)
    {
        float elapsed = 0.0f;
        while (elapsed < duration)
        {
            cv.alpha = Mathf.Lerp(0, 1, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        cv.alpha = 1;


        //UNLOAD AND LOAD
        var loaded0 = SceneManager.UnloadSceneAsync(currentScene);
        var loaded1 = SceneManager.LoadSceneAsync(sceneToLoad);

        while (!loaded0.isDone && !loaded1.isDone)
        {
            yield return null;
        } 
        // FINISHED LOADING, TRANSITION TO NEW

        float elapsed2 = 0.0f;
        while (elapsed2 < duration)
        {
            cv.alpha = Mathf.Lerp(1, 0, elapsed2 / duration);
            elapsed2 += Time.deltaTime;
            yield return null;
        }
        cv.alpha = 0;

        var loaded2 = SceneManager.UnloadSceneAsync("Loading");
    }
    
}
