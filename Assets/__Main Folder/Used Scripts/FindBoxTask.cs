using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindBoxTask : MonoBehaviour
{

    public ClipboardManager _clipboardManager;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered the area!");
            _clipboardManager.Task2Completed();
        }
    }


}
