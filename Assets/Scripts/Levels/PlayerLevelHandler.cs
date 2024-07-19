using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLevelHandler : MonoBehaviour
{
    //Levels in code were previously called BreadLevel
    //The thought was that the duck leveled up when it had eaten enoug bread. 
    //Because of this you may still see variables that use the bread-name to signify level-related variables.
    public Slider baguetteSlider;

    public TextMeshProUGUI breadLvl; 
    public TextMeshProUGUI breadPoints;

    //Maybe this shouldn't be hardcoded? And be like an exponential function or something?
    //public static int[] breadNeededToLevel = { 5, 10, 20, 32, 45, 68, 80, 95, 105, 110, 120, 140, 150 };
    public static int[] breadNeededToLevel = Enumerable.Range(2, 1000).ToArray();

    public int totalBread;
    public int crntBreadLevel;
    public int crntRelativeBreadInCrntLvl;

    public bool stopLeveling = false;

    private void Start()
    {
        UpdateBreadUi();
    }
    public void IncreaseBread(int inc)
    {
        totalBread += inc;
        crntRelativeBreadInCrntLvl += inc;
        
        if (HasBreadReachedNewLevel())
        {
            int prevBreadMax = breadNeededToLevel[crntBreadLevel];
            crntBreadLevel++;

            if (IsBreadLvlOverMaxLvl())
            {
                //Hardcap lvl
                crntBreadLevel = breadNeededToLevel.Length-1; 
                Debug.LogWarning("Beyond max level, need to code for this! Should this be able to happen?");
            }

            //Calculate new relative bread-lvl to show on baguette-UI
            crntRelativeBreadInCrntLvl = Mathf.Abs(prevBreadMax - crntRelativeBreadInCrntLvl);

            if (!stopLeveling)
            {
                DisplayNewItemChoice();
            }
        }
        UpdateBreadUi();
    }

    private void DisplayNewItemChoice()
    {
        References.Instance.soundHandler.PlayLevelUpSound();
        References.Instance.uiToggler.OpenUpgradeUI();
    }

    private bool IsBreadLvlOverMaxLvl()
    {
        return crntBreadLevel >= breadNeededToLevel.Length;
    }

    private bool HasBreadReachedNewLevel()
    {
        return crntRelativeBreadInCrntLvl >= breadNeededToLevel[crntBreadLevel];
    }

    private void UpdateBreadUi()
    {
        baguetteSlider.maxValue = breadNeededToLevel[crntBreadLevel];
        baguetteSlider.value = crntRelativeBreadInCrntLvl;
        breadLvl.text = crntBreadLevel.ToString();
        breadPoints.text = totalBread.ToString();
    }

}
