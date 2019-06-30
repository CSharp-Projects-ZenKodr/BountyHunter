using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
APPLIED TO:
    Explosion Animations
*/

public class DestroyByTime : MonoBehaviour {

    public float destroyTime = 1.0f;

    void Start () { Destroy(gameObject, destroyTime); }
}
