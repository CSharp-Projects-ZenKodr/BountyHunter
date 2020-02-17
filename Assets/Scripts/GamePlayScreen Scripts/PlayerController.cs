using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
/*
APPLIED TO:
    Player 
*/

[System.Serializable]
public class Boundary
{
    //The X and Z bounds of the player ship.
    public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour {

    // Class Variables 
    private Rigidbody RigidBody;
    private Vector3 rotate;
    private float MyTime, MoveHorizontal, MoveVertical, Speed, Tilt, ShotDiff;
    public Boundary boundary;
    public GameObject shot;
    public Transform shotSpawn;
    private bool smooth;
    public bool testMode;
    public float default_x_val;
    public float default_y_val;

    //Class Methods

    private void Start()
    {
        Tilt = 0.5f;
        MyTime = 0.0f;
        Speed = 330f; //magic number. 
        ShotDiff = 0.15f;
        RigidBody = GetComponent<Rigidbody>();
        testMode = false;
        default_x_val = Input.acceleration.x;
        default_y_val = Input.acceleration.y;
    }

    private void Update()
    {
        if (!GameController.GameOverFlag)
        {
            if (Input.GetButton("Fire1") && Time.time > MyTime && !GameController.PauseFlag)
            {
                MyTime = Time.time + ShotDiff;
                Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            }
            if (!testMode)
            {
                MoveHorizontal = Input.acceleration.x;
                MoveVertical = Input.acceleration.y;
            }
            else
            {
                ////Keyboard
                MoveHorizontal = Input.GetAxis("Horizontal");
            }
        }
    }

    private void FixedUpdate()
    {
        AddForce();
    }


    private void AddForce()
    {
        //Refactor this motion. Use physics equations and vectors.
        // Also add slight motion in z axis for natural feel 
        if (!testMode)
        {//Android Controlls 

            if (MoveVertical < default_y_val)
            {
                RigidBody.velocity = new Vector3((MoveHorizontal - default_x_val) * Speed * (float)Math.Cos(Math.PI / 4), 0f, -(MoveVertical - default_y_val) * Speed * (float)Math.Cos(Math.PI / 4));

            }
            else if (MoveHorizontal > default_y_val)
            {
                RigidBody.velocity = new Vector3((MoveHorizontal - default_x_val) * Speed * (float)Math.Cos(Math.PI / 4), 0f, -(MoveVertical - default_y_val) * Speed * (float)Math.Cos(Math.PI / 4));

            }
            else
            {
                RigidBody.velocity = new Vector3((MoveHorizontal - default_x_val) * Speed * (float)Math.Cos(Math.PI / 4), 0.0f, 0.0f);
            }
        }
        else
        { //Keyboard controlls
            RigidBody.velocity = new Vector3(MoveHorizontal * Speed, 0.0f, 0.0f);
        }
        RigidBody.position = new Vector3
        (
            Mathf.Clamp(RigidBody.position.x, boundary.xMin, boundary.xMax),
            0.0f,
            Mathf.Clamp(RigidBody.position.z, boundary.zMin, boundary.zMax)
        );

        rotate = new Vector3(0.0f, 0.0f, RigidBody.velocity.x);
        RigidBody.rotation = Quaternion.Euler(rotate * (-Tilt));
    }

}
