using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMover : MonoBehaviour
{
    // Start is called before the first frame update
    Transform SpawnLoc;

    private void Awake()
    {
        SpawnLoc = transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 0.1f);

        //Debug.Log(transform.position);
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger Inbound");
    }
    private void OnTriggerExit(Collider other)
    {
        //Debug.Log("Trigger Inbound");
        Instantiate(gameObject, SpawnLoc);
    }
}
