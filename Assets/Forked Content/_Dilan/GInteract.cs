using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GInteract : MonoBehaviour
{
    public GloveTest Glov;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Glov.tag))
        {
            Glov.changeGlove();
        }
    }
}
