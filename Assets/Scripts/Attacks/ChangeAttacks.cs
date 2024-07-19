using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeAttacks : MonoBehaviour
{
    public GameObject projectilePrefab;

    public void Change()
    {
        References.Instance.soundHandler.PlayClickBtn();
        References.Instance.attackAssets.projectileToInstantiate = projectilePrefab;
        //References.Instance.uiToggler.CloseUpgraedUI();
    }
}
