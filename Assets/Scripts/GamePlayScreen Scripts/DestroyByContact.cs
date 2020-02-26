using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
APPLIED TO:
    Asteroids
*/

public class DestroyByContact : MonoBehaviour {

    //Class Variables

    public GameObject AsteroidExplosion;
    public GameObject PlayerExplosion;

    private GameController controller;
    private Vector3 default_position;

    //Class Methods
    private void Awake()
    {
        controller = FindObjectOfType<GameController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bound" || other.tag == "Background") { return; }
        if (other.tag == "Asteroid") { return; }
    	if (other.tag == "Player") {
            Instantiate(PlayerExplosion, other.transform.position, other.transform.rotation);
            GameController.GameOverFlag = true;
        }
        else {  //Bullet hits asteroid 
            Instantiate(AsteroidExplosion, transform.position, transform.rotation);
            controller.ScoreUpdate();
        }
        Destroy(other.gameObject);
        Destroy(gameObject);
    }
}
