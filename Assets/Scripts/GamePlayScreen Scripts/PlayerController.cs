using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
/*
APPLIED TO:
    Player 
*/

/// <summary>
/// Class for spaceship motion
/// </summary>
/// 

[System.Serializable]
public class Boundary
{
    //The X and Z bounds of the player ship.
    public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour {

    #region Classvariables
    
    Rigidbody rb;
    Vector3 rotate;
    [SerializeField]
    float speed;  
    float tilt;
    public Boundary boundary;
    float defaultValueVertical;

    #endregion

    #region Class Methods

    private void Start()
    {
        tilt = 0.5f;
        rb = GetComponent<Rigidbody>();
        if (Application.platform == RuntimePlatform.Android)
        {
            speed *= 5; //speed up the spaceship when in android
            defaultValueVertical = Input.acceleration.y;
        }
    }

    private void FixedUpdate()
    {
        if (!GameController.GameOverFlag)
        {
            if (Application.platform == RuntimePlatform.Android)
                AndroidMotion();   
            else 
                DesktopMotion();            
        }
    }

    private void DesktopMotion()
    {
        float moveVertical = Input.GetAxis("ShipVertical");
        float moveHorizontal = Input.GetAxis("ShipHorizontal");

        Motion(moveHorizontal, moveVertical);
    }

    private void AndroidMotion ()
    {
        float moveVertical = Input.acceleration.y - defaultValueVertical;
        float moveHorizontal = Input.acceleration.x;

        Motion(moveHorizontal, moveVertical);
    }

    private void Motion (float horizontalAxis, float verticalAxis)
    {
        rb.velocity = Vector3.right * horizontalAxis + Vector3.forward * verticalAxis;
        rb.velocity.Normalize();

        rb.velocity *= speed;

        rb.position = new Vector3
        (
            Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
            0.0f,
            Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
        );

        rotate = new Vector3(0.0f, 0.0f, rb.velocity.x);
        rb.rotation = Quaternion.Euler(rotate * (-tilt));
    }
    #endregion
}
