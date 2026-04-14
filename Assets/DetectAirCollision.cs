using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectAirCollision : MonoBehaviour
{
    
    private void OnTriggerEnter(Collider collision)
    {
        ParticleSystem particleSystem = collision.GetComponent<ParticleSystem>();
        if (particleSystem != null)
        {
            ParticleSystem.MainModule main = particleSystem.main;
            if (main.startColor.color == Color.yellow)
            {
                Debug.Log("Yellow particle hit detected!");
            }
            if (main.startColor.color == Color.green)
            {
                Debug.Log("Green particle hit detected!");
            }

        }

    }
}
