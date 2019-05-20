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
    [HideInInspector] public static bool flag = false; //The game-over flag
    private Rigidbody rb;
    private GameController controller;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed;
        controller = FindObjectOfType<GameController>();
    }
    private void Update()
    {
        flag = controller.gameOverFlag;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Bound")
        {
            if (gameObject.tag == "Asteroid")
            {
                if (!flag)//if the game is not over and the asteroid moves out of bounds, the score drops by gameController.asteroidShootScore
                {
                    controller.scoreUpdate(false);
                }
            }
            Destroy(gameObject);
        }
    }
}
