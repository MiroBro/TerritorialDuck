using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class AllEffects
{
    public static Dictionary<Enums.PowerUpEffect, AttackEffect> allEffects = AllEffectsBlanked();
    public static List<Enums.PowerUpEffect> allAppliedEffects = new List<Enums.PowerUpEffect> { Enums.PowerUpEffect.Default };

    public static HashSet<Enums.PickupEffect> allEncounteredEffects = new();

    public static Dictionary<Enums.PowerUpEffect, AttackEffect> AllEffectsBlanked()
    {
        return new Dictionary<Enums.PowerUpEffect, AttackEffect> {
        {Enums.PowerUpEffect.Default, new FXDefault()},
        {Enums.PowerUpEffect.Bigger, new FXBigger()},
        {Enums.PowerUpEffect.FastHonker, new FXFastQuack()},
        {Enums.PowerUpEffect.ExtraRandom, new FXExtraRandomQuack()},
        {Enums.PowerUpEffect.SlowHonkBall, new FXSlowQuackSound()},
        {Enums.PowerUpEffect.FasterLegs, new FXFasterLegs()},
        {Enums.PowerUpEffect.PiercingQuacks, new FXPiercingQuacks()},
        {Enums.PowerUpEffect.IncreaseDamage, new FXIncreasedDamage()}
        };
    }

    public static List<KeyValuePair<Enums.PowerUpEffect, AttackEffect>> GetRandomEffects(int n)
    {
        System.Random rnd = new System.Random();

        //Make sure to get all except default
        var clone = new Dictionary<Enums.PowerUpEffect, AttackEffect>(allEffects); //copies by value;
        clone.Remove(Enums.PowerUpEffect.Default);

        List<KeyValuePair<Enums.PowerUpEffect, AttackEffect>> fx = clone.OrderBy(x => rnd.Next()).Take(n).ToList();
        return fx;
    }

    public static void ResetAppliedEffects()
    {
        allAppliedEffects = new List<Enums.PowerUpEffect> { Enums.PowerUpEffect.Default};
        //allSeenEffects = new HashSet<Enums.ItemEffects> { Enums.ItemEffects.Default};
        allEffects = AllEffectsBlanked();
        EffectVariables.ResetValues();
        //References.Instance.ballHandler.ResetBallHandler();
        References.Instance.duckOutfitHandler.ResetLooks();
        References.Instance.attackAssets.ResetQuackLook();
    }

    public static void AddEffect(Enums.PowerUpEffect effectToAdd)
    {
        //allSeenEffects.Add(effectToAdd);

        if (!allAppliedEffects.Contains(effectToAdd))
        {
            allAppliedEffects.Add(effectToAdd);
            allEffects[effectToAdd].DoOnNewApplication();
        }
        else
        {
            allEffects[effectToAdd].DoOnNewApplication();
        }
    }

    public static void ApplyEffectsWhenHonking()
    {
        foreach (var effect in allAppliedEffects)
        {
            allEffects[effect].EffectWhenQuacking();
        }
    }

    public static void ApplyAllEffectsOnBall(ProjectileInstanceChanger sae)
    {
        foreach (var effect in allAppliedEffects)
        {
            allEffects[effect].EffectOnQuackBall(sae);
        }
    }
}
