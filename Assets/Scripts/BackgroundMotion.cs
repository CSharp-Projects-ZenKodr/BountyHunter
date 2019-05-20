using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 Applied to:
 Background Capsule
 */


public class BackgroundMotion : MonoBehaviour
{

    public float tumble;
    public static bool flag = false;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        flag = false;
    }
    private void Update()
    {
        if (!flag)
        {
            rb.transform.Rotate(0f, tumble * Time.deltaTime, 0f);
        }
    }
}
