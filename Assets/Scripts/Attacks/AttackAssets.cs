using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAssets : MonoBehaviour
{
    public GameObject projectileToInstantiate;

    public GameObject defaultProjectile;
    public GameObject magicGirlProctile;


    public enum Quacks
    {
        Default,
        MagicGirl,
    }

    public void SetQuackType(Quacks quackType)
    {
        switch (quackType)
        {
            case Quacks.Default:
                projectileToInstantiate = defaultProjectile;
                break;
            case Quacks.MagicGirl:
                projectileToInstantiate = magicGirlProctile;
                break;
            default:
                break;
        }
    }

    public void ResetQuackLook()
    {
        projectileToInstantiate = defaultProjectile;
    }
}
