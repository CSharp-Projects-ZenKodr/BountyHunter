using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
APPLIED TO:
    Asteroids
 Makes the Asteroids rotate as they move towards the player. 
*/

public class RandomRotator : MonoBehaviour {

    public float tumble;

    private void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.angularVelocity = Random.insideUnitSphere * tumble;
    }

}
