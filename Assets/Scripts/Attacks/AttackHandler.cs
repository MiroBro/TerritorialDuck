using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class AttackHandler : MonoBehaviour
{
    //Attacks were previously called quacks/spit in code to make them more non-violent
    //Because of this you may see them called this in variable names
    public Transform spawnedProjectilesParent;
    public Transform projectileOriginLocation;
    public float destroyTime;

    public GameObject beakClosed;
    public GameObject beakOpened;

    public float quackSoundMaxVolume;
    public float quackSoundMinVolume;

    private static int projectileCount;
    private static int projectileIDTicker;

    private Dictionary<int, ProjectileInstanceHandler> allProjectilesInScene = new Dictionary<int, ProjectileInstanceHandler>();

    public void StartShootingProjectile()
    {
        InvokeRepeating(nameof(DoProjectileFX), 2.0f, EffectVariables.timeBetweenQuacks);
    }

    public void StopShootingProjectile()
    {
        CancelInvoke();
    }

    public void RestartProjectileShooting()
    {
        StopShootingProjectile();
        StartShootingProjectile();
    }

    public void ResetProjectileFrequency()
    {
        EffectVariables.timeBetweenQuacks = 1;
    }

    public void DoProjectileFX()
    {
        References.Instance.soundHandler.PlayRandomQuack(quackSoundMinVolume, quackSoundMaxVolume);
        AllEffects.ApplyEffectsWhenHonking();
    }

    IEnumerator OpenBeak()
    {
        beakClosed.SetActive(false);
        beakOpened.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        beakClosed.SetActive(true);
        beakOpened.SetActive(false);
    }

    public void ShootProjectileInDirection(Vector3 direction)
    {
        var inst = Instantiate(References.Instance.attackAssets.projectileToInstantiate, projectileOriginLocation.position, References.Instance.attackAssets.projectileToInstantiate.transform.rotation, spawnedProjectilesParent);

        projectileCount++;
        projectileIDTicker++;

        var projectilInstanceHandler = inst.transform.GetComponent<ProjectileInstanceHandler>();
        projectilInstanceHandler.projectileID = projectileIDTicker;
        projectilInstanceHandler.projectileDirection = direction;

        //Store projectile data for monitoring during testing/optmizing
        SaveProjectileData(projectileIDTicker, inst.GetComponent<ProjectileInstanceHandler>());

        float angle;
        angle = Vector3.SignedAngle(Vector3.up, direction.normalized, Vector3.up);

        if (direction.x >= 0)
            inst.transform.rotation = Quaternion.Euler(new Vector3(0, 0, -angle));
        else
            inst.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        inst.GetComponent<ProjectileInstanceChanger>().EnableSpitEffects();

        StopCoroutine(OpenBeak());
        StartCoroutine(OpenBeak());

        StartCoroutine(AutoDestroyProjectileAfterTime(projectileCount, destroyTime));
    }

    public void RestartProjectileShootingRepeating()
    {
        CancelInvoke();
        InvokeRepeating(nameof(DoProjectileFX), 0f, EffectVariables.timeBetweenQuacks);
    }

    private void SaveProjectileData(int id, ProjectileInstanceHandler projectileID)
    {
        References.Instance.statsForTesting.DisplayProjectileCount(projectileCount);
        allProjectilesInScene.Add(id, projectileID);
    }

    public void DestroyProjectile(int id)
    {
        if (allProjectilesInScene.ContainsKey(id))
        {
            var projectile = allProjectilesInScene[id];
            projectileCount--;
            References.Instance.statsForTesting.DisplayProjectileCount(projectileCount);
            allProjectilesInScene.Remove(id);
            Destroy(projectile.gameObject);
        }
    }

    IEnumerator AutoDestroyProjectileAfterTime(int id, float destroyAfterSeconds)
    {
        yield return new WaitForSeconds(destroyAfterSeconds);
        if (allProjectilesInScene.ContainsKey(id))
        {
            DestroyProjectile(id);
        }
    }

    public void ResetSavedProjectileData()
    {
        allProjectilesInScene.Clear();
        projectileCount = 0;
        projectileIDTicker = 0;
    }
}
