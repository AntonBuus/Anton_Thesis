using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class OvenScript : MonoBehaviour
{
    public bool item1place
    { get; set; }

    public bool item2place
    { get; set; }

    public bool item3place
    { get; set; }

    private bool hasRun = false;

    public ClipboardManager _clipboardManager;
    public GameObject pointLight;

    public GameObject[] objectsToHide;

    private bool areObjectsVisible = true;

    public void Microwave()
    {
        if (!hasRun && item1place && item2place && item3place)
        {
            hasRun = true;
            AudioManager.Instance.PlaySound("Oven", 0.5f);
            ToggleVisibility();
            _clipboardManager.Task4Completed();
        }
        else if (!hasRun && !item1place && !item2place && !item3place)
        {
            AudioManager.Instance.PlaySound("Fail", 0.5f);
        }
    }

    void Update()
    {
        if (hasRun && item1place && item2place && item3place)
        {
            pointLight.SetActive(true);
        }
        else
        {
            pointLight.SetActive(false);
        }
    }

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
