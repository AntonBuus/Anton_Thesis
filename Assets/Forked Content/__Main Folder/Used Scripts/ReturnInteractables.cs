using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Made with the assists
public class ReturnInteractables : MonoBehaviour
{
    private Dictionary<GameObject, Vector3> originalPositions = new Dictionary<GameObject, Vector3>();
    public LayerMask interactableLayer;

    void Start()
    {
        GameObject[] allObjects = FindObjectsOfType<GameObject>();
        foreach (GameObject obj in allObjects)
        {
            if (obj.layer == LayerMask.NameToLayer("Interactable"))
            {
                originalPositions[obj] = obj.transform.position;
            }
        }
    }

    private void OnTriggerEnter(Collider _interactableToReturn)
    {
        if (_interactableToReturn.gameObject.layer == LayerMask.NameToLayer("Interactable") && originalPositions.ContainsKey(_interactableToReturn.gameObject))
        {
            _interactableToReturn.transform.position = originalPositions[_interactableToReturn.gameObject];
        }
    }
    
}
