using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightManager : MonoBehaviour
{
    public GameObject greenLamp, redLamp;
    public Material redMaterial, greenMaterial;

    public void ToggleLight() 
    {
        if (gameObject.GetComponent<MeshRenderer>() && gameObject.GetComponent<MeshRenderer>().material)
        {
            if (redLamp.activeInHierarchy) 
            {
                gameObject.GetComponent<MeshRenderer>().material = greenMaterial;
                redLamp.SetActive(false);
                greenLamp.SetActive(true);
            }
            else
            {
                gameObject.GetComponent<MeshRenderer>().material = redMaterial;
                greenLamp.SetActive(false);
                redLamp.SetActive(true);
            }
        }
    }

    public void ConfirmPress()
    {
        Debug.Log("Pressed!");
    }
}
