using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followMouse : MonoBehaviour
{
    public SwapSpriteOnHover[] spHo;
    public Camera cam;
    /*void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = cam.nearClipPlane;
        this.transform.position = cam.ScreenToWorldPoint(mousePos);
    }*/

    void FixedUpdate()
    {
        /*
        // Cast a ray straight down.
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        // If it hits something...
        if (hit.collider != null) //&& hit.collider.CompareTag("Interactable"))
        {
            Debug.Log("hit duck!");
        }
        */
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);//Input.GetTouch(0).position);
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray);//(ray, Mathf.Infinity);

        //foreach (var hit in hits)
        {
            if (hit.collider != null && hit.collider.CompareTag("Interactable"))
            {
                //Debug.Log("hit something");
                hit.collider.GetComponent<SwapSpriteOnHover>().TurnOnSprite();
            } 
            else if (hit.collider == null)
            {
                foreach (var item in spHo)
                {
                    item.TurnOffSprite();
                }
            }

            
        }
    }

}
