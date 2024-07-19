using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Common
{
    public static int CalcSortOrder(float yPos, int offset)
    {
        return (int)-((yPos * 1000) + offset);
    }
}
