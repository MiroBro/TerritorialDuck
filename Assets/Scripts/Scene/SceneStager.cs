using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneStager : MonoBehaviour
{
    public bool startInBuildMode;

    //public GameObject spawner;
    //public QuackHandler quackHandler;
    //public GameObject duckHandler;

   //public static bool canWorldInteract;

    private void Start()
    {
        if (startInBuildMode)
        {
            TurnOnBuild();
        } else
        {
            if (!SavedInfo.hasSeenIntro)
            {
                //References.Instance.uiToggler.OpenIntroUI();
                References.Instance.uiToggler.OpenChoiceUi();
                References.Instance.soundHandler.SilenceMusic();

                Time.timeScale = 0;
                //spawner.SetActive(false);
                //quackHandler.SetActive(false);
                //duckHandler.SetActive(false);
                //SavedInfo.hasSeenIntro = true;
            }
            else
            {
                Time.timeScale = 1;
                References.Instance.uiToggler.OpenChoiceUi();
            }
        }
    }

    public void RestartScene()
    {
        References.Instance.soundHandler.PlayClickBtn();

        EnemySpawnHandler.amountOfEnemiesSpawned = 0;
        References.Instance.attackHandler.ResetSavedProjectileData();

        AllEffects.ResetAppliedEffects();

        //TransitionUiHandler.sceneToLoad = SceneManager.GetActiveScene().name;
        //TransitionUiHandler.currentScene = SceneManager.GetActiveScene().name;
        //Debug.Log(TransitionUiHandler.currentScene);
        //SceneManager.LoadSceneAsync("Loading", LoadSceneMode.Additive);

        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadSceneAsync(scene.name);


        //ResetALLValues
        //SceneManager.LoadScene(sceneName);
    }
    public void TurnOnChase()
    {
        if (SavedInfo.hasSeenIntro)
        {

            Time.timeScale = 1;
            References.Instance.attackHandler.enabled = true;
            References.Instance.enemySpawnHandler.enabled = true;
            References.Instance.uiToggler.EnableChaseUi();

            References.Instance.soundHandler.StopChoiceMusic();
            References.Instance.soundHandler.EnableMusic();
            References.Instance.enemySpawnHandler.RestartSpawning();
            References.Instance.attackHandler.StartShootingProjectile();
        }
        else
        {
            SavedInfo.hasSeenIntro = true;
            References.Instance.uiToggler.OpenIntroUI();
            References.Instance.soundHandler.StopChoiceMusic();
            References.Instance.soundHandler.TurnOnMusic();
            References.Instance.enemySpawnHandler.RestartSpawning(); 
            References.Instance.attackHandler.StartShootingProjectile();
        }
    }

    public void TurnOnBuild()
    {
        Time.timeScale = 1;
        References.Instance.uiToggler.EnableBuildUi();
        References.Instance.attackHandler.enabled = false;
        References.Instance.enemySpawnHandler.enabled = false;

        References.Instance.soundHandler.SilenceMusic();
        References.Instance.soundHandler.StopChoiceMusic();
        References.Instance.soundHandler.EnableBuildMusic();
    }
}
