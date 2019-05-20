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
    private Rigidbody rb;
    private Vector3 rotate;
    private float myTime = 0.0f;
    private float moveHorizontal;
    private float speed = 297.14f;
    private float tilt = 0.5f;
    private float shotDiff = 0.15f;

    public Boundary boundary;
    public GameObject shot;
    public Transform shotSpawn;


    //Class Methods

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > myTime) {
            myTime = Time.time + shotDiff;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            
        }
        //moveHorizontal = Input.GetAxis("Horizontal");
        moveHorizontal = Input.acceleration.x;
    }

    private void FixedUpdate()
    {

        rb.velocity = new Vector3(moveHorizontal*speed, 0.0f, 0.0f);
        rb.position = new Vector3
            (
            Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax), 
            0.0f, 
            Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
            );

        rotate = new Vector3(0.0f, 0.0f, rb.velocity.x);
        rb.rotation = Quaternion.Euler(rotate*(-tilt));
    }
}
