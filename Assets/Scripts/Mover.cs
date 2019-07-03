using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
APPLIED TO:
    Asteroids
    Bolts
*/

public class Mover : MonoBehaviour
{

    public float speed;
    private Rigidbody rb;
    private GameController controller;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed;
        controller = FindObjectOfType<GameController>();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Bound")
        {
            if (gameObject.tag == "Asteroid")
            {
                if (!GameController.GameOverFlag)
                    controller.ScoreUpdate(false);
            }
            Destroy(gameObject);
        }
    }
}
