using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;

public interface AttackEffect
{
    string effectName { get; }
    string description { get; }

    Sprite icon { get; }
    int timesApplied { get => timesApplied; set => timesApplied = value; }

    int maxApplications { get => maxApplications; }
    Enums.PowerUpEffect effect { get; }

    void DoOnNewApplication() { }
    void EffectWhenQuacking() { }
    void EffectOnQuackBall(ProjectileInstanceChanger sae) { }
}

public class FXDefault : AttackEffect
{
    protected int timesApplied = 0;
    protected int maxApplications = -1;
    public string effectName => "Default";

    public Enums.PowerUpEffect effect => Enums.PowerUpEffect.Default;

    public string description => "You start in this state";

    public Sprite icon => null;

    public void EffectWhenQuacking()
    {
        References.Instance.attackHandler.ShootProjectileInDirection(DuckMovement.direction);
    }

}

public class FXBigger : AttackEffect
{
    protected int timesApplied = 0;
    protected int maxApplications = -1;
    public string effectName => "Sea Tea";

    public Enums.PowerUpEffect effect => Enums.PowerUpEffect.Bigger;

    public string description => " Does wonders to the throat. Increases the size of your quacks.";

    public Sprite icon => References.Instance.itemAssets.bigger; 

    public void DoOnNewApplication()
    {
        timesApplied++;
        EffectVariables.quackSizeMultiplier = 3 * timesApplied;
    }
    public void EffectOnQuackBall(ProjectileInstanceChanger quackAlterer)
    {
        quackAlterer.AlterQuackSize();//SetSize(3 * timesApplied);
    }
}

public class FXFastQuack : AttackEffect
{
    protected int timesApplied = 0;
    protected int maxApplications = 10;
    public string effectName => "Yummy Bills";

    public Enums.PowerUpEffect effect => Enums.PowerUpEffect.FastHonker;

    public string description => "Macabre ... but invigoratingly delicious. Increases your quacking speed by 30 %.";

    public Sprite icon => References.Instance.itemAssets.fastQuack;

    public void DoOnNewApplication()
    {
        timesApplied++;
        EffectVariables.timeBetweenQuacks = EffectVariables.initialTimeBetweenQuacks / (1.05f * timesApplied);

        References.Instance.attackHandler.RestartProjectileShootingRepeating();
        //QuackHandler.origTimeBetweenQuaks / (1.05f * timesApplied));
    }
}

public class FXExtraRandomQuack : AttackEffect
{
    protected int timesApplied = 0;
    protected int maxApplications = 10;
    public string effectName => "Mirror of Quackora";

    public Enums.PowerUpEffect effect => Enums.PowerUpEffect.ExtraRandom;

    public string description => "Infamous relic. Adds another quack sound in a random direction";

    public Sprite icon => References.Instance.itemAssets.extraRandomProjectile;

    public void DoOnNewApplication()
    {
        timesApplied++;
    }

    public void EffectWhenQuacking()
    {
        for (int i = 0; i < timesApplied; i++)
        {
            References.Instance.attackHandler.ShootProjectileInDirection(UnityEngine.Random.insideUnitCircle.normalized);
        }
    }
}

public class FXSlowQuackSound : AttackEffect
{
    protected int timesApplied = 0;
    protected int maxApplications = -1;
    protected float speedDec = 0.7f;
    public string effectName => "Heavy Love Letter";

    public Enums.PowerUpEffect effect => Enums.PowerUpEffect.Default;

    public string description => "Mail takes so long nowadays! Decrease quack sound's speed to " + speedDec + "% speed";

    public Sprite icon => References.Instance.itemAssets.slowerProjectiles;

    public void DoOnNewApplication()
    {
        timesApplied++;
        EffectVariables.quacksTravelSpeedMultiplier = Mathf.Pow(speedDec, timesApplied);
    }
}

public class FXFasterLegs : AttackEffect
{
    protected int timesApplied = 0;
    protected int maxApplications = -1;
    public float speedInc = 0.1f;
    public string effectName => "Rainboots";

    public Enums.PowerUpEffect effect => Enums.PowerUpEffect.Default;

    public string description => "Feathers of Phoenix, they fit your webbed feet like silky gloves. Makes you run " + speedInc + "% faster";

    public Sprite icon => References.Instance.itemAssets.fasterLegs;

    public void DoOnNewApplication()
    {
        timesApplied++;
        EffectVariables.duckSpeed = EffectVariables.initialDuckSpeed * ((1 + speedInc) * timesApplied);
    }

}

public class FXPiercingQuacks : AttackEffect
{
    protected int timesApplied = 0;
    protected int maxApplications = -1;
    public string effectName => "Opera Tutu";

    public Enums.PowerUpEffect effect => Enums.PowerUpEffect.PiercingQuacks;

    public string description => "A beautiful tutu for the stage. Makes quacks pass through one more animal  \n (Changes Appearance)";

    public Sprite icon => References.Instance.itemAssets.piercingProjectiles;

    public void DoOnNewApplication()
    {
        timesApplied++;
        EffectVariables.amountOfEnemiesAProjectileCanHitBeforeDestroy++;
        References.Instance.duckOutfitHandler.EnableTutu();
    }

}

public class FXIncreasedDamage : AttackEffect
{
    protected int timesApplied = 0;
    protected int maxApplications = -1;
    protected int damageIncrease = 50;
    public string effectName => "Magic Girl Wig";

    public Enums.PowerUpEffect effect => Enums.PowerUpEffect.IncreaseDamage;

    public string description => "Sparkling feathers, you will look fabulous in this wig. \n Increases annoyance of Quacks by " + damageIncrease + "p \n (Changes Appearance)";

    public Sprite icon => References.Instance.itemAssets.increaseDamage;

    public void DoOnNewApplication()
    {
        timesApplied++;
        EffectVariables.patienceEffectPerQuackHit += damageIncrease;
        References.Instance.duckOutfitHandler.EnableWig();

        //Change quack-prefab to magic-girl quacks
        References.Instance.attackAssets.SetQuackType(AttackAssets.Quacks.MagicGirl);
    }

}







//DuckMovement.speed = (DuckMovement.originalSpeed * (1 + (speedInc/100)*timesApplied));


/*
public void EffectOnQuackBall(QuackAlterer quackAlterer)
{
    //quackAlterer.AlterQuackSpeed();
    //sae.SetSpeed(Mathf.Pow(speedDec, timesApplied));
}*/



// Resources.LoadAll<Sprite>("Assets/Sprites/Items/items1").Single(s => s.name == "fastQuack");
// Resources.LoadAll<Sprite>("Assets/Sprites/Items/items1").Single(s => s.name == "extraRandomQuack");
//DuckieStats.runningSpeed++;

//speedDec*timesApplied);


// Resources.LoadAll<Sprite>("Assets/Sprites/Items/item1").Single(s => s.name == "slowQuackSound");

// Resources.LoadAll<Sprite>("Assets/Sprites/Items/item1").Single(s => s.name == "slowQuackSound");
//Resources.LoadAll<Sprite>("Assets/Sprites/Items/items1").Single(s => s.name == "bigger");
//public Sprite icon => Addressables.LoadAsset<Sprite>("Assets/Sprites/Items/Items1.png");

/*
  // Load all sprites in atlas
Sprite[] abilityIconsAtlas = Resources.LoadAll<Sprite>("AbilityIcons");
// Get specific sprite
Sprite fireBallSprite = abilityIconsAtlas.Single(s => s.name == "FireBall");
 */


//public string description => "A beautiful tutu for the stage. Makes you quack like an opera star and your quacks pass through one more animal  \n (Changes Appearance)";