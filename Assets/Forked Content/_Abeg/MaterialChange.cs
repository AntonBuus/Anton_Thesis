using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialChange : MonoBehaviour
{
    public Material[] material;
    Renderer rend;

    
    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        rend.sharedMaterial = material[0];
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "blueBox")
        {
            rend.sharedMaterial = material[1];
        }
    }
}
