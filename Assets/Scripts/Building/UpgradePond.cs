using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradePond : MonoBehaviour
{
    public GameObject dirtyPond;
    public GameObject cleanPond;
    public void Upgrade()
    {
        dirtyPond.SetActive(false);
        cleanPond.SetActive(true);
    }
}
