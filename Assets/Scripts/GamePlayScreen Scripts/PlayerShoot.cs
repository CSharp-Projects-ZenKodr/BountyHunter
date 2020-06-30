using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Class to handle player's shooting
/// </summary>
public class PlayerShoot : MonoBehaviour
{
    float MyTime = 0.0f;
    float ShotDiff = 0.15f;
    public GameObject shot;
    Transform shotSpawn;
    // Start is called before the first frame update
    void Start()
    {
        shotSpawn = transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameController.GameOverFlag)
        {
            if (Input.GetButton("Fire1") && Time.time > MyTime && !GameController.PauseFlag)
            {
                MyTime = Time.time + ShotDiff;
                Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            }
        }
    }
}
