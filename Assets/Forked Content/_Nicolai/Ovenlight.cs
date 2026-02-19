using UnityEngine;

public class ToggleLight : MonoBehaviour
{
    public GameObject pointLight; // Drag your Point Light into this field in the Inspector

    private bool isLightOn = false; // Keeps track of the light's current state

    // This method will be called when the button is interacted with
    public void ToggleLightState()
    {
        if (pointLight != null)
        {
            isLightOn = !isLightOn; // Toggle the light state
            pointLight.SetActive(isLightOn); // Set the light's active state
        }
        else
        {
            Debug.LogWarning("Point Light is not assigned in the Inspector!");
        }
    }
}
