using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLoadingScene : MonoBehaviour
{
    public void LoadNewScene(string newScene)
    {
        TransitionUiHandler.sceneToLoad = newScene;
        TransitionUiHandler.currentScene = SceneManager.GetActiveScene().name;
        //Debug.Log(TransitionUiHandler.currentScene);
        SceneManager.LoadSceneAsync("Loading", LoadSceneMode.Additive);
    }
}
