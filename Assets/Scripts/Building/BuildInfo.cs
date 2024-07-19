using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildInfo : MonoBehaviour
{
    public Enums.Buildings building;
    public Collider2D buildingColl;
    public SpriteRenderer[] spriteRenderers;
    public CapsuleCollider2D coll;
}
