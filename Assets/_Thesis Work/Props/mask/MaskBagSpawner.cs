using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class MaskBagSpawner : MonoBehaviour
{
    public GameObject prefabToSpawn;
    public Transform spawnPoint;

    private XRSimpleInteractable simple;

    void Start()
    {
        simple = GetComponent<XRSimpleInteractable>();
        simple.selectEntered.AddListener(OnGrab);
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        var playerGrabInteractor = args.interactorObject;

        GameObject spawned = Instantiate(prefabToSpawn, spawnPoint.position, spawnPoint.rotation);
        
        XRGrabInteractable grabInteractable = spawned.GetComponent<XRGrabInteractable>(); 
        args.manager.SelectEnter(playerGrabInteractor, grabInteractable);
    }
}
