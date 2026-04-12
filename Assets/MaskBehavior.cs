using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.XR.CoreUtils;
using UnityEditor;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class MaskBehavior : MonoBehaviour
{
    public bool isWearingMask
    { get; set; }
    
    SocketTagFunc _socketTagFunc;

    // AudioDetection _audioDetection;
    void Start()
    {
        _socketTagFunc = GetComponent<SocketTagFunc>();
        // _audioDetection = GameObject.Find("Micinput2").GetComponent<AudioDetection>();
    }
    public void ToggleBacteria()
    {
        var wornMask = _socketTagFunc.GetOldestInteractableSelected();

        // foreach (Transform child in wornMask.transform)
        // {
        //     Debug.Log("Child name: " + child.name);
        // }
        if (isWearingMask)
        {
            Debug.Log("Worn mask child 2 name: " + wornMask.transform.GetChild(2).name);
            Debug.Log("Is wearing mask: " + isWearingMask);

            if (wornMask.transform.GetChild(2).name == "mask_Contamination")
            {
                wornMask.transform.GetChild(2).gameObject.SetActive(true);
                Debug.Log("Bacteria is active");
            }
        }
    }



    // void OnCollisionEnter(Collision _facemask)
    // {
    //     if (_facemask.gameObject.CompareTag("_facemask"))
    //     {
    //         Transform[] children = _facemask.gameObject.GetComponentsInChildren<Transform>();
    //         if (children.Length > 3)
    //         {
                
    //             children[3].gameObject.SetActive(true);
    //             Debug.Log("Mask is being worn");
    //         }
    //     }
    // }

    // Update is called once per frame
    void Update()
    {
        
    
    }
}
