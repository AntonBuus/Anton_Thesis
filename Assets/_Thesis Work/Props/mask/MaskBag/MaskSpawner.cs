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

    public GameObject SpawnMaskInHand(Transform handTransform)
    {
        GameObject mask = Instantiate(maskPrefab, handTransform.position, handTransform.rotation, handTransform);
        Debug.Log("Mask spawned in hand at position: " + handTransform.position);
        return mask;
    }

    public void SpawnMaskInWorldSpace(GameObject maskObject, Vector3 position, Quaternion rotation)
    {
        maskObject.transform.SetParent(null);
        maskObject.transform.position = position;
        maskObject.transform.rotation = rotation;
        Debug.Log("Mask spawned in world space at position: " + position);
    }


    //cleaner version?
    public Transform cleanspawnPoint;

    private XRSimpleInteractable interactable;
     private XRBaseInteractable _baseinteractable;

    void Awake()
    {
        interactable = GetComponent<XRSimpleInteractable>();
        interactable.selectEntered.AddListener(OnGrab);

        
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        // The interactor that grabbed this object
        var interactor = args.interactorObject;

        // Spawn the real object
        GameObject spawned = Instantiate(maskPrefab, cleanspawnPoint.position, cleanspawnPoint.rotation);

        // Get its interactable
        var spawnedInteractable = spawned.GetComponent<XRGrabInteractable>();

        // Force the interactor to grab the new object
        args.manager.SelectEnter(interactor, spawnedInteractable);

        // Optionally destroy the placeholder
        // Destroy(gameObject);
    }
    
}
