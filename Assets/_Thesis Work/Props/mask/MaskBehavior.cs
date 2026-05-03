using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.XR.CoreUtils;
using UnityEditor;
// using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
// using UnityEngine.SceneManagement;
// using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;

public class MaskBehavior : MonoBehaviour
{
    public bool isWearingMask
    { get; set; }
    
    SocketTagFunc _socketTagFunc;

    TutorialSteps _speechTutorialStepsScript;
    AudioManager audioManagerScript;
    DataCollectionManager _datacollectionManagerScript;

    // AudioDetection _audioDetection;
    void Start()
    {
        _socketTagFunc = GetComponent<SocketTagFunc>();
        // Debug.Log(UnitySceneManager.GetActiveScene().name);
        if (GameObject.Find("TutorialSteps") != null)
        {
            _speechTutorialStepsScript = GameObject.Find("TutorialSteps").GetComponent<TutorialSteps>();
        }
        else
        {
            Debug.Log("TutorialComponent not in this scene FYI");
        }
        audioManagerScript = GameObject.Find("_AudioManager").GetComponent<AudioManager>();

        _datacollectionManagerScript = GameObject.Find("_DataCollection_Manager").GetComponent<DataCollectionManager>();

        // _speechTutorialStepsScript = GameObject.Find("TutorialSteps").GetComponent<TutorialSteps>();
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
            // _datacollectionManagerScript.LogMaskWorn();

            if (wornMask.transform.GetChild(2).name == "mask_Contamination")
            {
                wornMask.transform.GetChild(2).gameObject.SetActive(true);
                Debug.Log("Bacteria is active");
                // _datacollectionManagerScript.LogMaskContaminated();
                if (_speechTutorialStepsScript != null && _speechTutorialStepsScript._successfullyContaminatedMask == false)
                {
                    _speechTutorialStepsScript._successfullyContaminatedMask = true;
                    // audioManagerScript.Play("good"); 
                }
            }
        }
    }
    void FixedUpdate() //for logging
    {
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "Menu")
    {   
            return;
        } 

        
        if (isWearingMask)
        {
            _datacollectionManagerScript.LogMaskWorn();

            var wornMask = _socketTagFunc.GetOldestInteractableSelected();
            if (wornMask.transform.GetChild(2).name == "mask_Contamination" && wornMask.transform.GetChild(2).gameObject.activeSelf == true)
            {
                _datacollectionManagerScript.LogMaskContaminated();
            }
        }

        
    }
}
