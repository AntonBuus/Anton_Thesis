using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LookAtPlayer : MonoBehaviour
{
    public GameObject menu;
    
    public Transform head;
    public float spawnDistance = 0.1f;


    private Vector3 relativePosition;

    void Update()
    {
        // PositionFromHead();
        PositionFromHand();
        
    }

    private void PositionFromHand()
    {
        menu.transform.LookAt(new Vector3(head.position.x, menu.transform.position.y, head.position.z));
        menu.transform.forward *= -1;
    }
    private void PositionFromHead()
    {
       if (menu.activeSelf)
        {
            menu.transform.position = head.position + new Vector3(head.forward.x, 0, head.forward.z).normalized * spawnDistance;
        }
        menu.transform.LookAt(new Vector3(head.position.x, menu.transform.position.y, head.position.z));
        menu.transform.forward *= -1;
    }
}
