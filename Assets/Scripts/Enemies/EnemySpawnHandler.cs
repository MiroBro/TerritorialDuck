using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnHandler : MonoBehaviour
{
    //Enemies were previously called annoyances in code
    //Because of this reason you may still see variables called annoyances etc.
    public GameObject[] enemiesToSpawn;

    public Transform spawnedEnemiesParent;
    public float distance;
    public float spawnFrequency = 0.001f;

    public static int amountOfEnemiesSpawned;
    public static int maxEnemies = 1000;

    public static int maxEnemyeDistance = 50;

    public bool updateSpawnRate = false;

    public void RestartSpawning()
    {
        StopSpawning();
        StartSpawning();
    }

    private void Update()
    {
        if (updateSpawnRate)
        {
            updateSpawnRate = false;

            CancelInvoke("Spawn");
            InvokeRepeating("Spawn", 0.0f, spawnFrequency);
        }
    }

    public void StartSpawning()
    {

        InvokeRepeating("Spawn", 1.0f, spawnFrequency);
    }

    public void StopSpawning()
    {
        CancelInvoke();
    }

    public void Spawn()
    {
        if (amountOfEnemiesSpawned < maxEnemies)
        {
            int rand = Random.Range(0, enemiesToSpawn.Length);
            amountOfEnemiesSpawned++;
            //Debug.Log(annoyancesSpawned);
            Instantiate(enemiesToSpawn[rand], GetRandomPosOffScreen(), enemiesToSpawn[rand].transform.rotation, spawnedEnemiesParent);
        }
        References.Instance.statsForTesting.setEnemyCount(amountOfEnemiesSpawned);
    }

    private Vector3 GetRandomPosOffScreen()
    {
        int randomSide = Random.Range(0, 2);
        float x;
        float y;

        if (randomSide == 0)
        {
            x = Random.Range(0, 2);

            if (x == 0) x = -distance;
            else x = 1 + distance;

            y = Random.Range(-distance, 1 + distance);
        }
        else
        {
            y = Random.Range(0, 2);

            if (y == 0) y = -distance;
            else y = 1 + distance;

            x = Random.Range(-distance, 1 + distance);
        }

        Vector3 zNulled = References.Instance.mainCam.ViewportToWorldPoint(new Vector3(x, y, 0));
        zNulled.z = 0;
        return zNulled;
    }

    private float GetDistance(float edge)
    {
        if (edge <= 0)
        {
            return -distance;
        }
        else
            return 1 + distance;
    }

}

