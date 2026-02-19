using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipGloves : MonoBehaviour
{
    // Reference to the SkinnedMeshRenderer
    private SkinnedMeshRenderer skinnedMeshRenderer;

    // The new material you want to apply
    public Material newMaterial;

    void Start()
    {
        // Get the SkinnedMeshRenderer component
        skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();

        // Log whether the SkinnedMeshRenderer was successfully found
        if (skinnedMeshRenderer == null)
        {
            Debug.LogError("SkinnedMeshRenderer not found on " + gameObject.name + "!");
        }
        else
        {
            Debug.Log("SkinnedMeshRenderer successfully found on " + gameObject.name + ".");
        }
    }

    // Method to change the material
    public void ChangeMaterialOnCall()
    {
        if (skinnedMeshRenderer != null)
        {
            Debug.Log("Changing material on " + gameObject.name + " to " + newMaterial.name + ".");

            // Apply the new material to the SkinnedMeshRenderer's material
            skinnedMeshRenderer.material = newMaterial;

            Debug.Log("Material successfully changed on " + gameObject.name + ".");
        }
        else
        {
            Debug.LogWarning("Cannot change material because SkinnedMeshRenderer is null on " + gameObject.name + ".");
        }
    }
}
