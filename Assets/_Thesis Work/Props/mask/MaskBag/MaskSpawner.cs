using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class MaskSpawner : MonoBehaviour
{
    public GameObject maskPrefab;
    public Transform spawnPoint;

    

    public void SpawnMask()
    {
        Instantiate(maskPrefab, spawnPoint.position, spawnPoint.rotation);
    }

    public void SpawnMaskInHand(Transform handTransform)
    {
        Instantiate(maskPrefab, handTransform.position, handTransform.rotation, handTransform);
        Debug.Log("Mask spawned in hand at position: " + handTransform.position);
        // return mask;
    }
    public void LetGoOfMask(Transform handTransform)
    {
        // Detach the mask from the hand
        if (handTransform.childCount > 0)
        {
            Transform mask = handTransform.GetChild(0);
            mask.SetParent(null);
            Debug.Log("Mask released from hand at position: " + mask.position);
        }
    }
}
