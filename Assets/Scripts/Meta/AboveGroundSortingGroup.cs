using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class AboveGroundSortingGroup : MonoBehaviour
{
    public SortingGroup sg;
    public int offset;

    private void Update()
    {
        SetSortingOrder();
    }

    public void SetSortingOrder()
    {
        sg.sortingOrder = Common.CalcSortOrder(transform.position.y, offset);
    }
}
