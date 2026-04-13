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
        var playerGrabInteractor = args.interactorObject; //we are defining the interactor that grabbed the spawner

        GameObject spawned = Instantiate(prefabToSpawn, spawnPoint.position, spawnPoint.rotation);
        
        XRGrabInteractable grabInteractable = spawned.GetComponent<XRGrabInteractable>(); // get the grab interactable component from the spawned object

        // Force the interactor to grab the spawned object
        args.manager.SelectEnter(playerGrabInteractor, grabInteractable);
    }
}
