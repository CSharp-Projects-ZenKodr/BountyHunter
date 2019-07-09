using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    private float MyTime, MoveHorizontal, Speed, Tilt, ShotDiff;
    public Boundary boundary;
    public GameObject shot;
    public Transform shotSpawn;

    //Class Methods

    private void Start()
    {
        Tilt = 0.5f;
        MyTime = 0.0f;
        Speed = 297.14f;
        ShotDiff = 0.15f;
        RigidBody = GetComponent<Rigidbody>();
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

            {
                ////Keyboard
                //MoveHorizontal = Input.GetAxis("Horizontal");
            }

            {
                //Android
                MoveHorizontal = Input.acceleration.x;
            }
            
        }
    }

    private void FixedUpdate()
    {
        //Place all of this in a seperate function if possible
        RigidBody.velocity = new Vector3(MoveHorizontal*Speed, 0.0f, 0.0f);
        RigidBody.position = new Vector3
        (
            Mathf.Clamp(RigidBody.position.x, boundary.xMin, boundary.xMax), 
            0.0f, 
            Mathf.Clamp(RigidBody.position.z, boundary.zMin, boundary.zMax)
        );

        rotate = new Vector3(0.0f, 0.0f, RigidBody.velocity.x);
        RigidBody.rotation = Quaternion.Euler(rotate*(-Tilt));
    }

}
