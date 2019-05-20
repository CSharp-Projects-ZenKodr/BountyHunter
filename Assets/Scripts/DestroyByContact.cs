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

    public GameObject asteroidExplosion;
    public GameObject playerExplosion;

    private GameController controller;
    private Vector3 default_position;

    //Class Methods
    private void Awake()
    {
        controller = FindObjectOfType<GameController>();
        
        
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.tag == "Bound"){return;}
        if (other.tag == "Asteroid"){return;}

    	if (other.tag == "Player")
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
            Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
            BackgroundMotion.flag = true;
            controller.gameOverFlag = true;
        }
        else {
            Destroy(other.gameObject);
            Instantiate(asteroidExplosion, transform.position, transform.rotation);
            Destroy(gameObject);
            controller.scoreUpdate();
            if (!System.Convert.ToBoolean(controller.GetScore() % 100))
            {
                GameController.raiseSpeedFlag = true;

            }

        }

    }

    
}
