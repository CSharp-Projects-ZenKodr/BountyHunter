using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMover : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform SpawnLoc;
    private int RepositionCount;
    public float BGSpeed;

    private void Start()
    {
        RepositionCount = 0;
        BGSpeed = 0.1f;
    }

    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - BGSpeed);
    }
    private void FixedUpdate()
    {
        if (RepositionCount >= 1)
        {
            if (RepositionCount == 3)
            {
                transform.position = SpawnLoc.position;
                RepositionCount = 0;
            }
            else
            {
                RepositionCount += 1;
            }
        }
    }
    //private void OnTriggerEnter(Collider other)
    //{
    //    Debug.Log("Trigger Inbound");
    //    Instantiate(gameObject, SpawnLoc.position, SpawnLoc.rotation);
    //}
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Bound")
        {
            RepositionCount = 1;
            Debug.Log("destroyCount Set.");
        }
        
    }
}
