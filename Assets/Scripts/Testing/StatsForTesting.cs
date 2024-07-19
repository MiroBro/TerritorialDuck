using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatsForTesting : MonoBehaviour
{
    public TextMeshProUGUI enemyCountText;
    public static int enemyCount = 0;

    public TextMeshProUGUI projectileCountText;
    public static int projectileCount = 0;

    public void setEnemyCount(int count)
    {
        enemyCount = count;
        enemyCountText.text = enemyCount.ToString();
    }

    public void DisplayProjectileCount(int count)
    {
        projectileCount = count;
        projectileCountText.text = projectileCount.ToString();
    }
}
