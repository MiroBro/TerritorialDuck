using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupEffectsHandler : MonoBehaviour
{
    public void Applyeffect(PickUpInfo itemEffectInfo)
    {
        Enums.PickupEffect effect = itemEffectInfo.pickupEffect;

        if (!AllEffects.allEncounteredEffects.Contains(effect))
        {
            DisplayItemInfo(itemEffectInfo);
            AllEffects.allEncounteredEffects.Add(effect);
        }

        ApplyPickupEffect(effect);
    }

    private void DisplayItemInfo(PickUpInfo pickupInfo)
    {
        References.Instance.uiToggler.OpenNewItemDisplay(pickupInfo.itemIcon, pickupInfo.GetName(), pickupInfo.GetDescription());
    }

    private void ApplyPickupEffect(Enums.PickupEffect pickupEffect)
    {
        switch (pickupEffect)
        {
            case Enums.PickupEffect.Leaf1:
                References.Instance.soundHandler.PlayLeaf();
                References.Instance.moneyHandler.AddMoney(1);
                break;
            case Enums.PickupEffect.Leaf2:
                References.Instance.soundHandler.PlayLeaf();
                References.Instance.moneyHandler.AddMoney(5);
                break;
            case Enums.PickupEffect.Leaf3:
                References.Instance.soundHandler.PlayLeaf();
                References.Instance.moneyHandler.AddMoney(10);
                break;
            case Enums.PickupEffect.Leaf4:
                References.Instance.soundHandler.PlayLeaf();
                References.Instance.moneyHandler.AddMoney(100);
                break;
            case Enums.PickupEffect.NailPolish:
                References.Instance.soundHandler.PlayPickUp();
                References.Instance.playerHealthHandler.AlterPatience(1);
                break;
            case Enums.PickupEffect.RainbowPolish:
                References.Instance.soundHandler.PlayPickUp();
                References.Instance.playerHealthHandler.AlterPatience(30);
                break;
            case Enums.PickupEffect.Tea:
                References.Instance.soundHandler.PlayPickUp();
                References.Instance.playerHealthHandler.AlterPatience(35);
                break;
            case Enums.PickupEffect.DogBag:
                References.Instance.uiToggler.OpenBagUpgradeUi();
                break;
            case Enums.PickupEffect.CatBag:
                References.Instance.uiToggler.OpenBagUpgradeUi();
                break;
            case Enums.PickupEffect.Bread:
                References.Instance.soundHandler.PlayPickUp();
                References.Instance.levelHandler.IncreaseBread(1);
                break;
            case Enums.PickupEffect.Loaf:
                References.Instance.soundHandler.PlayPickUp();
                References.Instance.levelHandler.IncreaseBread(30);
                break;
            case Enums.PickupEffect.Clover:
                EffectVariables.luck++;
                break;
            case Enums.PickupEffect.Megaphone:
                StopCoroutine(ScareAwayAllEnemies());
                StartCoroutine(ScareAwayAllEnemies());
                break;
            case Enums.PickupEffect.Beans:
                Debug.Log("Should fart for 7 seconds");
                break;
            case Enums.PickupEffect.RomanceNovel:
                StopCoroutine(StopTime());
                StartCoroutine(StopTime());
                break;
            case Enums.PickupEffect.DinoEggs:
                EffectVariables.RandomStatInc();
                break;
            case Enums.PickupEffect.Vaccuum:
                StartCoroutine(SuckInBread());
                break;
            case Enums.PickupEffect.LeafBlower:
                StartCoroutine(SuckInLeaves());
                break;
            default:
                break;
        }
    }

    IEnumerator SuckInLeaves()
    {
        ItemMover.suckInAllLeaves = true;
        yield return 0;
        ItemMover.suckInAllLeaves = false;
    }

    IEnumerator SuckInBread()
    {
        ItemMover.suckInAllBread = true;
        yield return 0;
        ItemMover.suckInAllBread = false;
    }

    IEnumerator StopTime()
    {
        MoveEnemy.stopTime = true;
        MoveEnemy.colorFreezing = true;
        yield return new WaitForSeconds(15);
        MoveEnemy.colorDefault = true;
        MoveEnemy.stopTime = false;
    }

    IEnumerator ScareAwayAllEnemies()
    {
        MoveEnemy.scareAwayAllEnemies = true;
        yield return new WaitForSeconds(2);
        MoveEnemy.scareAwayAllEnemies = false;
    }
}