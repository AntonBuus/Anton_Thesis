using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlovesMaterialChange : MonoBehaviour
{
    public Material[] materials; // Array of materials (default and blue)
    private SkinnedMeshRenderer handRenderer; // SkinnedMeshRenderer for the hand

    void Start()
    {
        // Try to get the SkinnedMeshRenderer component from the hand
        handRenderer = GetComponent<SkinnedMeshRenderer>();

        if (handRenderer != null)
        {
            if (materials.Length > 0)
            {
                handRenderer.material = materials[0]; // Set the initial material (skin color)
            }
            else
            {
                Debug.LogError("Materials array is empty! Please set the materials in the Inspector.");
            }
        }
        else
        {
            Debug.LogError("SkinnedMeshRenderer not found on the hand object. Please ensure the hand has a SkinnedMeshRenderer component.");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the hand collides with the glove box
        if (collision.gameObject.CompareTag("GloveBox"))
        {
            // If the materials array has a blue material, change the hand's material
            if (handRenderer != null && materials.Length > 1)
            {
                handRenderer.material = materials[1]; // Switch to blue material
            }
            else
            {
                Debug.LogError("SkinnedMeshRenderer is null or materials array is missing elements!");
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        // When the hand exits the collision with the glove box, revert to the default material
        if (collision.gameObject.CompareTag("GloveBox") && handRenderer != null && materials.Length > 0)
        {
            handRenderer.material = materials[0]; // Revert to default material (skin color)
        }
    }
}
