using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class MoveEnemy : MonoBehaviour
{
    // Adjust the speed for the application.
    public float speed;

    // The target (cylinder) position.
    public Vector3 movingTowards;

    public static bool scareAwayAllEnemies;
    public static bool stopTime;

    public EnemyHealthHandler enemyHealthHandler;
    public bool move = true;

    public static bool colorFreezing;
    public static bool colorDefault;

    private static Vector3 left = new Vector3(1, 1, 1);
    private static Vector3 right = new Vector3(-1, 1, 1);

    public Rigidbody2D rb;

    public Transform toFlip;

    public SortingGroup sg;
    public int offset;
    void Update()
    {
        if (move && !stopTime)
        {
            // Move our position a step closer to the target.
            var step = speed * Time.deltaTime; // calculate distance to move
            movingTowards = DuckMovement.duckHitArea.position;//duckHitPoint.position;


            //Vector3 dir = (rb.transform.position - movingTowards).normalized; <- They will RUN AWAY
            Vector3 dir = (movingTowards - rb.transform.position).normalized;

            rb.MovePosition(rb.transform.position + dir * step);
        }

        if (DuckMovement.duckHitArea.position.x > toFlip.transform.position.x)
            toFlip.transform.localScale = right;
        else
            toFlip.transform.localScale = left;


        if (scareAwayAllEnemies)
        {
            enemyHealthHandler.DecreasePatience(100000000, References.Instance.soundHandler.leaf, true);
        }
        else if (Vector3.Distance(DuckMovement.duckPos.position, transform.position) >= EnemySpawnHandler.maxEnemyeDistance)
        {
            enemyHealthHandler.DecreasePatience(100000000, References.Instance.soundHandler.leaf, false);
        }

        sg.sortingOrder = Common.CalcSortOrder(rb.position.y, offset);

    }
}
