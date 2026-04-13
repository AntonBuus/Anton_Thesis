using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectAirCollision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Check for yellow particle
        if (other.CompareTag("Yellow"))
        {
            Debug.Log("Yellow particle hit detected!");
        }
        
        // Check for green particle
        if (other.CompareTag("Green"))
        {
            Debug.Log("Green particle hit detected!");
        }
    }
}
