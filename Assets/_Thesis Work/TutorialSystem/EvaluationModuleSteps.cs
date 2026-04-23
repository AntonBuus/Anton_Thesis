using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Threading.Tasks;

public class EvaluationModuleSteps : MonoBehaviour
{
    private int _stepindex;
    
    [Header("UI Elements")]
    public TextMeshProUGUI _title;
    public TextMeshProUGUI _text;
    public TextMeshProUGUI _buttonText;
    public GameObject _nextButton;
    public GameObject _previousButton;
    public GameObject _endTutorialButton;

    [Header("Tracking Contamination")]
    public TextMeshProUGUI _dynamicText; 
    TrackContamination _trackContaminationScript;

    // public GameObject _image_goodPractice;

    [Header("Toggleable Props")]
    // public GameObject _UDF_System;
    // public GameObject _products;

    [Header("Tutorial step variables")]
    //Change these
    private bool _requirement1Met = false;
    private bool _requirement2Met = false;
    private bool _taskCompleted = false;
    private bool _showingResults = false;

    [Header("Core functionality variables")]
    public GameObject movingCanvas;
    
    public Transform playerHead;
    // public float distanceFromPlayer = 2;
    MaskBehavior _maskBehaviorScript;

    SceneLoader _sceneLoader;

    void Update()
    {
        movingCanvas.transform.LookAt(new Vector3(playerHead.position.x, movingCanvas.transform.position.y, playerHead.position.z));
        movingCanvas.transform.forward *= -1;

        if (_dynamicText.gameObject.activeSelf)
        {
            DocumentProgressWithText();
        }
    }
    
    public void Start()
    {
        _title.text = "Evaluation";
        _text.text = "Welcome to the evaluation module. You will have to apply everything you have learned in previous modules.";
        Debug.Log("stepindex: " + _stepindex);
        _maskBehaviorScript = GameObject.Find("_Snap_mask").GetComponent<MaskBehavior>();
        _previousButton.SetActive(false);
        _sceneLoader = GameObject.Find("_sceneLoader").GetComponent<SceneLoader>();
        _trackContaminationScript = GameObject.Find("Product_dishes").GetComponent<TrackContamination>();
    }

    public void Requirement1()
    {
        // Debug.Log("PlayerHasSpoken Called");
        if(_stepindex == 4 && !_requirement1Met)
        {
            _requirement1Met = true;
            // Debug.Log("Player has spoken correctly");
            //Should i call StepNext here?
                StepNext();
        }
    }
    public void Requirement2()
    {
        // Debug.Log("PlayerHasGrabbedNewMask Called");
        if(_stepindex == 8 && !_requirement2Met)
        {
            _requirement2Met = true;
            // Debug.Log("Player has grabbed new mask correctly");
            //Should i call StepNext here?
                // StepNext();
        }
    }

    public void DocumentProgressWithText()
    {
        if(_stepindex == 5)
        {
            _dynamicText.text = "Lids placed on petridishes: " + _trackContaminationScript._lidsPlacedTotal + "/"+ _trackContaminationScript._dishesTotalAmount;
            if (_trackContaminationScript._lidsPlacedTotal == _trackContaminationScript._dishesTotalAmount)
            {   
                _taskCompleted = true;
                StepNext();
            }
        }
        if(_stepindex == 6)
        {
            if(_showingResults == false && _taskCompleted)
            {
                _showingResults = true;
                int SuccesCalculation = _trackContaminationScript._dishesTotalAmount-_trackContaminationScript._contaminatedDishesAmount;
                _dynamicText.text = "Succesfull placements: " + SuccesCalculation + "/" + _trackContaminationScript._dishesTotalAmount;
                Debug.Log("final results shown");
            }

            
        }
    }
    public void StepNext()
    {
        switch(_stepindex)
        {
            case -1:
                _title.text = "Evaluation";
                _text.text = "Welcome to the evaluation module. You will have to apply everything you have learned in previous modules.";
                _stepindex++;
                Debug.Log("stepindex: " + _stepindex);

                _previousButton.SetActive(false);
                break;

            case 0:
                _title.text = "Tasks";
                _text.text = "Your tasks include correctly placing lids on top of clean surfaces without contaminating the contents.";
                _stepindex++;
                Debug.Log("stepindex: " + _stepindex);

                // _previousButton.SetActive(true);
                break;

            case 1:
                _title.text = "Recap";
                _text.text = "Remember, all of this is supposed to be done without speech, and with a non-contaminated mask.";
                _stepindex++;
                Debug.Log("stepindex: " + _stepindex);
                break;

            case 2:
                _title.text = "Recap";
                _text.text = "Change your mask if it contaminated, before executing your assigned task";
                _stepindex++;
                Debug.Log("stepindex: " + _stepindex);
                break;

            case 3:
                _title.text = "Ready to begin?";
                _text.text = "Start the evaluation when you are ready.";
                _stepindex++;
                Debug.Log("stepindex: " + _stepindex);
                //Show picture here
                // _UDF_System.SetActive(true);
                break;

            case 4: //&& _requirement1Met
                _title.text = "Go!";
                _text.text = "Put lids on the petridishes. Don't speak and change your mask if relevant.";
                _dynamicText.gameObject.SetActive(true);
                _nextButton.SetActive(false);

                _stepindex++;
                Debug.Log("stepindex: " + _stepindex);
                break;

            case 5: //&& _requirement2Met
                _title.text = "Finished";
                _text.text = "You have placed the lids";
                _stepindex++;
                _nextButton.SetActive(true); 
                
                Debug.Log("stepindex: " + _stepindex);
                break;

            case 6:
                _title.text = "Exit?";
                _text.text = "Press the exit button and take off your VR headset.";
                _stepindex++;
                Debug.Log("stepindex: " + _stepindex);
                _dynamicText.gameObject.SetActive(false);
                _buttonText.text = "Next";
                _buttonText.gameObject.SetActive(false);
                _endTutorialButton.SetActive(true);

                // _image_goodPractice.SetActive(false);
                break;

            default:
                break;
        }
    }
    public void PreviousStep()
    {
        if(_stepindex > -1 && _stepindex!=0)
        {
            // _stepindex--;
            // _stepindex--;

            _stepindex -= 2;
            Debug.Log("Went back to step: " + _stepindex);
            StepNext();
        }
    }
    public void EndTutorial()
    {
        _sceneLoader.LoadDesiredScene("Menu");
    }
}
