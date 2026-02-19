using UnityEngine;

public class ToggleMultipleObjects : MonoBehaviour
{
    public GameObject[] objectsToHide; // Array to hold multiple GameObjects

    private bool areObjectsVisible = true; // Tracks the visibility state

    public void ToggleVisibility()
    {
        if (objectsToHide != null && objectsToHide.Length > 0)
        {
            areObjectsVisible = !areObjectsVisible; // Toggle the visibility state

            // Loop through each GameObject and set its active state
            foreach (GameObject obj in objectsToHide)
            {
                if (obj != null)
                {
                    obj.SetActive(areObjectsVisible);
                }
            }
        }
        else
        {
            Debug.LogWarning("Objects to hide are not assigned in the Inspector!");
        }
    }
}
