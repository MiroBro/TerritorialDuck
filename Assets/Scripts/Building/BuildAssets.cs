using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildAssets : MonoBehaviour
{
    public GameObject tree1;
    public GameObject tree2;
    public GameObject bush;
    public GameObject rock;
    public GameObject rock2;
    public GameObject cauldron;
    public GameObject cleanWater;

    public GameObject GetBuild(Enums.Buildings building)
    {
        switch (building)
        {
            case Enums.Buildings.Tree1:
                return tree1;
            case Enums.Buildings.Tree2:
                return tree2;
            case Enums.Buildings.Bush:
                return bush;
            case Enums.Buildings.Rock:
                return rock;
            case Enums.Buildings.Rock2:
                return rock2;
            case Enums.Buildings.Cauldron:
                return cauldron;
            case Enums.Buildings.CleanWater:
                return cleanWater;
            default:
                return null;
        }
    }
    
}
