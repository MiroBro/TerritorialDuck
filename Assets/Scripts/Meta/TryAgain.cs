using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TryAgain : MonoBehaviour
{
    //public string sceneName;
    public void RestartScene()
    {
        References.Instance.sceneStager.RestartScene();
    }
}
