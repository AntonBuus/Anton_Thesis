using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ginterract : MonoBehaviour
{
    public GloveChange1 gl;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gl.changeGloves();
        }

    }
}
