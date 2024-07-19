using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Build : MonoBehaviour
{
    public Enums.Buildings toBuild;
    public void InstantiateBuilding()
    {
        References.Instance.buildHandler.InstBuilding(toBuild);
    }
}
