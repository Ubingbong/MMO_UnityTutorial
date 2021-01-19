using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionTest : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        Debug.Log($"Collision! @{other.gameObject.name}");
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger!");
    }
    void Start()
    {

    }

    void Update()
    {

    }
}