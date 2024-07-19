using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class DuckSortingOrder : MonoBehaviour
{
    public SortingGroup sg;
    public int offset;
    public Transform duckTrans;

    void Update()
    {
        int calcSortOrd = (int) (duckTrans.position.y * 100 + offset);
        sg.sortingOrder = Common.CalcSortOrder(duckTrans.position.y, offset);
    }

}
