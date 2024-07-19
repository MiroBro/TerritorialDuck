using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class DuckMovement : MonoBehaviour
{
    //public static float speed = 3.5f;
    //public static float originalSpeed = speed;
    public Rigidbody2D rigidBody;
    public Animator animator;
    // public QuackHandler quackhandler;
    private float prevInputH;
    public static Vector3 direction = Vector3.right;

    public Transform duck;
    public Transform duckHit;
    public static Transform duckHitArea;
    public static Transform duckPos;

    [SerializeField] enum aimInput { MouseAim, Controller, AutoAim }
    [SerializeField] aimInput currentAimSetting;

    //UnityEngine.InputSystem (New Input Manager related variables)---------------------------
    public float speed;
    private Vector2 move, mouseLook, joystickLook;
    private Key buttonInput;
    //----------------------------------------------------------------------------------------

    public void OnMove(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector2>();
    }

    public void OnMouseLook(InputAction.CallbackContext context)
    {
        mouseLook = context.ReadValue<Vector2>();
    }

    public void OnJoystickLook(InputAction.CallbackContext context)
    {
        joystickLook = context.ReadValue<Vector2>();
    }

    public void OnButtonInput(InputAction.CallbackContext context)
    {
        //If button in group is pressed
        if (context.performed)
        {
            if(currentAimSetting == aimInput.AutoAim)
            {
                currentAimSetting = aimInput.MouseAim;
            } 
            else if(currentAimSetting == aimInput.MouseAim)
            {
                currentAimSetting = aimInput.AutoAim;
            }
        }

        //buttonInput = context.ReadValue<Key>();
        //var pressedButton = ((KeyControl)context.control).keyCode;
        //else if (context.canceled) ;
        /* released */
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Annoyance") && !MoveEnemy.stopTime)
        {
            References.Instance.playerHealthHandler.AlterPatience(-5);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Annoyance") && !MoveEnemy.stopTime)
        {
            References.Instance.playerHealthHandler.AlterPatience(-5);
        }
    }
    private void Awake()
    {
        duckPos = duck;
        duckHitArea = duckHit;

    }

    public void FixedUpdate()
    {
        PlayerAim();
        PlayerMove();

    }

    private void PlayerAim()
    {

        switch (currentAimSetting)
        {
            case aimInput.MouseAim:
                // Get the mouse position in screen coordinates
                Vector3 mousePosition = mouseLook;
                // Convert the mouse position to a point in the game world
                Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
                // Calculate the direction vector between the rigid body and the mouse position
                // Vector3 direction = worldMousePosition - rb.transform.position;
                direction = (worldMousePosition - References.Instance.attackHandler.projectileOriginLocation.transform.position);//.Normalize();
                break;

            case aimInput.Controller:
                var aimDirection = new Vector3(joystickLook.x, joystickLook.y, 0f);
                direction = aimDirection;
                break;

            case aimInput.AutoAim:
                FindClosestEnemy();
                break;

            default:
                Debug.Log("No Aim Input found");
                break;
        }

    }

    public void FindClosestEnemy()
    {
        // !!!! This search should use the OverlapCircleAll parameter LayerMask to register only enemies. 
        // !!!! Once Enemies are spawned into a specific layer!
        // !!!! OverlapCircleAll( origin, size, layerMask)
        // !!!! https://docs.unity3d.com/ScriptReference/Physics2D.OverlapCircleAll.html
        // !!!!

        Vector3 playerPos = rigidBody.transform.position;

        // Searches for nearby GameObjects with 2D Circle Colliders
        var searchRadia = 4;
        var searchRadiaMax = 20;
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(playerPos, searchRadia);
        while (hitColliders.Length <= 0 || searchRadia <= searchRadiaMax)
        {
            searchRadia += 4;
            hitColliders = Physics2D.OverlapCircleAll(playerPos, searchRadia);
        }
        
        // Checks the distance for each Object found in the above step and saves away the closes if it is tagged with "Annoyance"
        var closest = hitColliders[0];
        float smallestDist = float.MaxValue;
        foreach (var enemyObj in hitColliders)
        {
            float dist = Vector3.SqrMagnitude(enemyObj.transform.position - playerPos);
            if (enemyObj.tag == "Annoyance" && dist < smallestDist )
            {
                closest = enemyObj;
                smallestDist = dist;
            }
        }

        // If a object was found that was tagged with "Annoyance" and is closest set the shooting direction
        if (closest.tag == "Annoyance")
        {
            direction = (closest.transform.position - References.Instance.attackHandler.projectileOriginLocation.transform.position);//.Normalize();
        }

    }

    public void PlayerMove()
    {
        float h = move.x;
        float v = move.y;

        if (h != 0 || v != 0)
        {
            // direction = new Vector3(h, v, 0);
            animator.SetBool("Move", true);
        }
        else
        {
            animator.SetBool("Move", false);
        }

        if (h < 0)
            transform.localScale = new Vector3(1, 1, 1);
        else if (h > 0)
            transform.localScale = new Vector3(-1, 1, 1);
        else
        {
            if (prevInputH < 0)
                transform.localScale = new Vector3(1, 1, 1);
            else
                transform.localScale = new Vector3(-1, 1, 1);
        }


        Vector3 tempVect = new Vector3(h, v, 0);
        //tempVect = tempVect.normalized * speed * Time.deltaTime;
        tempVect = tempVect.normalized * EffectVariables.duckSpeed * Time.deltaTime;
        rigidBody.MovePosition(rigidBody.transform.position + tempVect);



        if (h != 0)
            prevInputH = h;
    }

    /*
void OnDrawGizmosSelected()
{
    // Display the radius of the FindNeighbors function
    Gizmos.color = Color.white;
    Gizmos.DrawWireSphere(rigidBody.transform.position, 20);
}
*/

}
