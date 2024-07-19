using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthHandler : MonoBehaviour
{
    //Health was previously called annoyance/patience, the idea being that when the duck ran out of patience/became too annoyed you lost the run.
    //So you may see these names being used in variables!
    //Enemies were called "annoyances"
    public GameObject parentObj;
    public float patience = 100;

    public SpriteRenderer[] sr;

    public MoveEnemy moveEnemy;

    [ColorUsageAttribute(true, true)]
    public Color freezeColor;
    //[ColorUsageAttribute(true, true)]
    public Color defaultColor;
    [ColorUsageAttribute(true, true)]
    public Color hitColor;
    [ColorUsageAttribute(true, true)]
    public Color runColor;

    public HitSoundAssets.HitType hitSoundType;

    private float glowAmountOnHit = 10;
    private float glowAmountDefault = 0;
    //private float glowGlobalAmountDefault = 1;

    public void DecreasePatience(float patienceChange, AudioSource hitSound, bool dropItem)
    {
        if(patience > 0)
        {
            patience -= patienceChange;

            if (patience <= 0)
                RunAway(hitSound, dropItem);
            else
                AlterEffect(hitSound);
        }
    }

    private void AlterEffect(AudioSource hitSound)
    {
        //References.Instance.soundHandler.PlayRandomHit();
        //References.Instance.soundHandler.PlayHit(hitSound);
        References.Instance.soundHandler.hitSoundAssets.PlayHit(hitSoundType);
        StopAllCoroutines();
        SetDefaultColor();
        StartCoroutine(HitEffect());
    }

    private void RunAway(AudioSource hitSound, bool dropItem)
    {
        StopAllCoroutines();
        SetDefaultColor();
        StartCoroutine(RunAwayEffect(hitSound, dropItem));
    }

    IEnumerator RunAwayEffect(AudioSource hitSound, bool dropItem)
    {
        moveEnemy.move = false;
        SetRunColor();
        yield return new WaitForSeconds(0.2f);
        //References.Instance.soundHandler.PlayRandomLeave();

        if (dropItem)
        {
            //References.Instance.soundHandler.PlayHit(hitSound);
            References.Instance.soundHandler.hitSoundAssets.PlayLeave(hitSoundType);
            References.Instance.itemSpawnHandler.SpawnPickup(transform.position);
        }
        
        EnemySpawnHandler.amountOfEnemiesSpawned--;
        Destroy(parentObj);
    }

    /*
    [ColorUsageAttribute(true, true)]
    public Color hitColor;
    public Color defaultColor;
    [ColorUsageAttribute(true, true)]
    public Color freezeColor;
    */

    IEnumerator HitEffect()
    {
        SetHitColor();
        yield return new WaitForSeconds(0.2f);
        if (MoveEnemy.stopTime)
        {
            SetFreezeColor();
        } else
        {
            SetDefaultColor();
        }
    }

    public void SetDefaultColor()
    {
        foreach (var s in sr)
        {
            s.material.SetColor("_Color", defaultColor);
            s.material.SetFloat("_Glow", glowAmountDefault);
            //s.material.SetFloat("_GlowGlobal", glowGlobalAmountDefault);
        }
    }

    public void SetFreezeColor()
    {
        foreach (var s in sr)
        {
            s.material.SetColor("_Color", freezeColor);
            s.material.SetFloat("_Glow", glowAmountOnHit);
            //s.material.SetFloat("_GlowGlobal", glowAmountOnHit);
        }
    }

    public void SetHitColor()
    {
        foreach (var s in sr)
        {
            s.material.SetColor("_Color", hitColor);
            s.material.SetFloat("_Glow", glowAmountOnHit);
            //s.material.SetFloat("_GlowGlobal", glowAmountOnHit);
        }
    }

    public void SetRunColor()
    {
        foreach (var s in sr)
        {
            s.material.SetColor("_Color", runColor);
            s.material.SetFloat("_Glow", glowAmountOnHit);
            //s.material.SetFloat("_GlowGlobal", glowAmountOnHit);
        }
    }


}
