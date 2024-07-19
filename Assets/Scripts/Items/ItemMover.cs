using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMover : MonoBehaviour
{
    public static bool suckInAllBread = false;
    public static bool suckInAllLeaves = false;
    private bool suckIn;
    public float speed;
    public Vector3 movingTowards;
    public GameObject parentObj;
    public PickUpInfo itemEffectInfo;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Sucker"))
        {
            suckIn = true;
        }
    }

    private void Update()
    {
        if (suckIn)
        {
            MoveTowardsDuck();
        } else if (suckInAllBread && !suckIn)
        {
            if (itemEffectInfo.pickupEffect == Enums.PickupEffect.Bread || itemEffectInfo.pickupEffect == Enums.PickupEffect.Loaf)
            {
                suckIn = true;
                MoveTowardsDuck();
            } 
        }  else if (suckInAllLeaves && !suckIn)
        {
            if (itemEffectInfo.pickupEffect == Enums.PickupEffect.Leaf1 || itemEffectInfo.pickupEffect == Enums.PickupEffect.Leaf2 || itemEffectInfo.pickupEffect == Enums.PickupEffect.Leaf3 || itemEffectInfo.pickupEffect == Enums.PickupEffect.Leaf4)
            {
                suckIn = true;
                MoveTowardsDuck();
            }
        }


    }

    private void MoveTowardsDuck()
    {
        var step = speed * Time.deltaTime; // calculate distance to move
        movingTowards = DuckMovement.duckPos.position;
        parentObj.transform.position = Vector3.MoveTowards(transform.position, movingTowards, step);

        if (HasItemReachedDuck())
        {
            References.Instance.pickupEffectHandler.Applyeffect(itemEffectInfo);
            Destroy(parentObj);
        }
    }

    private bool HasItemReachedDuck()
    {
        return Vector3.Distance(parentObj.transform.position, movingTowards) < 0.001f;
    }
}
