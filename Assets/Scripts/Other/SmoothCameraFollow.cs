using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCameraFollow : MonoBehaviour
{
    public bool canZoom;
    //The speed of the movement
    public float cameraSpeed = 0.5f;
    public float speedofIncrease = 3;

    //Min and max-values the camera can move to.
    public float MAX_X = 6;
    public float MIN_X = -6;
    public float MAX_Y = 3;
    public float MIN_Y = -3;


    //The current x and y movement of the cursor
    private float Xmouse;
    private float Ymouse;

    //Variables for zoom
    float ZoomAmount = 0; //With Positive and negative values
    public float MaxToClamp = 5;
    public float ROTSpeed = 5;
    void Update()
    {

        //THE ZOOM
        if (canZoom)
        {
            ZoomAmount += Input.GetAxis("Mouse ScrollWheel");
            ZoomAmount = Mathf.Clamp(ZoomAmount, -MaxToClamp, MaxToClamp);
            var translate = Mathf.Min(Mathf.Abs(Input.GetAxis("Mouse ScrollWheel")), MaxToClamp - Mathf.Abs(ZoomAmount));
            gameObject.transform.Translate(0, 0, translate * ROTSpeed * Mathf.Sign(Input.GetAxis("Mouse ScrollWheel")));
        }
        //END OF ZOOM


        Xmouse = Input.GetAxisRaw("Mouse X");
        Ymouse = Input.GetAxisRaw("Mouse Y");

        //Fångar upp mus-positionen först i pixlar sen översatt till koord denna frame/update
        Vector3 v3 = Input.mousePosition;
        v3.z = transform.position.z;
        //v3 = Camera.main.ScreenToWorldPoint(v3);

        //Skapar koordinater dit kameran ska röra sig.
        Vector3 newPos = transform.position;
        newPos.x += Xmouse * speedofIncrease;
        newPos.y += Ymouse * speedofIncrease;

        
        //Making sure that the camera doesn't exceed the min/max values it's allowed to move to!
        if (newPos.x > MAX_X)
        {
            newPos.x = MAX_X;
        }
        if (newPos.x < MIN_X)
        {
            newPos.x = MIN_X;
        }

        if (newPos.y > MAX_Y)
        {
            newPos.y = MAX_Y;
        }
        if (newPos.y < MIN_Y)
        {
            newPos.y = MIN_Y;
        }

        //Flyttar långsamt/linjärt med Lerp kameran mot den nya koordinaten.
        transform.position = Vector3.Lerp(transform.position, newPos, cameraSpeed * Time.deltaTime);
    }
}