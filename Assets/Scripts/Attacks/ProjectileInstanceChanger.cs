using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileInstanceChanger : MonoBehaviour
{
    public void EnableSpitEffects()
    {
        AllEffects.ApplyAllEffectsOnBall(this);
    }

    public void AlterQuackSize()
    {
        this.transform.localScale *= EffectVariables.quackSizeMultiplier;
    }

    /*
    public void AlterQuackSpeed()
    {
        quackProjectile.baseSpeed *= EffectVariables.quacksSpeedMultiplier;
    }
    */
    /*
    public void SetSize(float multifactor)
    {
        quack.localScale *= multifactor;

    }

    public void SetSpeed(float newSpeedFactor)
    {
        quackProjectile.baseSpeed *= newSpeedFactor;
    }*/
}