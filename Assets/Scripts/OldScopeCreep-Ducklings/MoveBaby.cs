using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBaby : MonoBehaviour
{
    public static Transform babyTrans;
    public Animator babyAnim;
    //public Rigidbody2D babyRb;
    public static int currentIndex = 0;
    public float speed;
    public static List<Vector3> path;

    private void Start()
    {
        babyTrans = GetComponent<Transform>();
    }

    private void Update()
    {
        MoveBabyHandler();
    }

    public static Vector3 GetBabyPos()
    {
        return babyTrans.position;
    }

    public static void ResetPath()
    {
        currentIndex = 0;
        path = null;
    }

    public void MoveBabyHandler()
    {
        if (path != null)
        {
            Vector3 targetPos = path[currentIndex];
            if((Vector3.Distance(transform.position, targetPos) > 0.4f && (currentIndex != (path.Count-1)) || Vector3.Distance(transform.position, targetPos) > 0.1f))
            {
                Vector3 moveDir = (targetPos - babyTrans.position).normalized;

                float distanceBefore = Vector3.Distance(babyTrans.position, targetPos);
                SetMoveVector(moveDir);
                babyTrans.position = transform.position + moveDir * speed * Time.deltaTime;
            }
            else
            {
                currentIndex++;
                if (currentIndex >= path.Count)
                {
                    StopMoving();
                    SetMoveVector(Vector3.zero);
                }
            }
        } else
        {
            SetMoveVector(Vector3.zero);
        }
    }
    private Vector3 movementVector;

    private void StopMoving()
    {
        path = null;
    }

    private void SetMoveVector(Vector3 moveDir)
    {
        if (moveDir != Vector3.zero)
        {
            babyAnim.SetBool("Walk", true);
            if (moveDir.x < 0)
            {
                babyTrans.localScale = new Vector3(Mathf.Abs(babyTrans.localScale.x), babyTrans.localScale.y, babyTrans.localScale.z);
            } else
            {
                babyTrans.localScale = new Vector3(-Mathf.Abs(babyTrans.localScale.x), babyTrans.localScale.y, babyTrans.localScale.z);
            }

        } else
        {
            babyAnim.SetBool("Walk", false);
        }


    }
}
