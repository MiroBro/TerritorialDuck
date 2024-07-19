using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectVariables 
{
    public static int luck;

    public static float initialTimeBetweenQuacks = 1;
    public static float timeBetweenQuacks = 1;

    public static float quackingSpeedMultipier = 1;
    public static float quackSizeMultiplier = 1;

    public static int amountOfEnemiesAProjectileCanHitBeforeDestroy = 1;

    public static int patienceEffectPerQuackHit = 50;

    public static float initialDuckSpeed = 3.5f;
    public static float duckSpeed = 3.5f;

    public static float quacksBaseTravelSpeed = 5f;
    public static float quacksTravelSpeedMultiplier = 1;



    //TODO: Below needs work too, values missing
    public static void ResetValues()
    {
        luck = 1;

        initialTimeBetweenQuacks = 1;
        timeBetweenQuacks = 1;

        quackingSpeedMultipier = 1;
        quackSizeMultiplier = 1;

        amountOfEnemiesAProjectileCanHitBeforeDestroy = 1;

        patienceEffectPerQuackHit = 50;

        initialDuckSpeed = 3.5f;
        duckSpeed = 3.5f;

        quacksBaseTravelSpeed = 5f;
        quacksTravelSpeedMultiplier = 1;
    }

    //TODO: This is not finished, needs work to work properly with all attributes
    public static void RandomStatInc()
    {
        int rand = Random.Range(0, 5);

        switch (rand)
        {
            case 0:
                luck++;
                break;
            case 1:
                quackingSpeedMultipier++;
                break;
            case 2:
                quacksTravelSpeedMultiplier++;
                break;
            //case 3:
            //    runningSpeed++;
            //    break;
            case 3:
                quackSizeMultiplier++;
                break;
            default:
                break;
        }
    }
}
