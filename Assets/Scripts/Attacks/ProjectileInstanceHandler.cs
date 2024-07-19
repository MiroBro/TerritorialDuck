using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileInstanceHandler : MonoBehaviour
{
    public Vector3 projectileDirection = Vector3.right;
    public AudioSource audioSource;
    public int projectileID;

    private int penetration = EffectVariables.amountOfEnemiesAProjectileCanHitBeforeDestroy;

    void Update()
    {
        transform.position += (EffectVariables.quacksBaseTravelSpeed * EffectVariables.quacksTravelSpeedMultiplier) * Time.deltaTime * projectileDirection;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Annoyance"))
        {
            penetration--;

            collision.gameObject.GetComponent<EnemyHealthHandler>().DecreasePatience(EffectVariables.patienceEffectPerQuackHit,audioSource, true);
            if (penetration <= 0)
            {
                StartCoroutine(DestroyAtEndOfFrame());
            }
        }
    }

    IEnumerator DestroyAtEndOfFrame()
    {
        yield return new WaitForEndOfFrame();
        References.Instance.attackHandler.DestroyProjectile(projectileID);
        //Destroy(this.transform.parent.gameObject);
    }
}
