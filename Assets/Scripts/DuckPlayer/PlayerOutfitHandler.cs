using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOutfitHandler : MonoBehaviour
{
    public SpriteRenderer outfit;
    public SpriteRenderer wig;
    public SpriteRenderer wig2;

    public void ResetLooks()
    {
        outfit.enabled = false;
        wig.enabled = false;
        wig2.enabled = false;
        //Debug.Log("Shold reset duck--looks here");
    }

    public void EnableTutu()
    {
        outfit.enabled = true;
        //Debug.Log("Should put on tutu here");
    }

    public void EnableWig()
    {
        wig.enabled = true;
        wig2.enabled = true;
    }
}
