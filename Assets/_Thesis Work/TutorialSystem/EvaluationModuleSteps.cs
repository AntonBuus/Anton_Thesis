using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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

    public TextMeshProUGUI _dynamicText; 
    // public GameObject _image_goodPractice;

    [Header("Toggleable Props")]
    // public GameObject _UDF_System;
    // public GameObject _products;

    [Header("Tutorial step variables")]
    //Change these
    private bool _requirement1Met = false;
    private bool _requirement2Met = false;
    private bool _requirement3Met = false;

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
    }
    
    public void Start()
    {
        _title.text = "Evaluation";
        _text.text = "Welcome to the evaluation module. You will have to apply everything you have learned in previous modules.";
        Debug.Log("stepindex: " + _stepindex);
        _maskBehaviorScript = GameObject.Find("_Snap_mask").GetComponent<MaskBehavior>();
        _previousButton.SetActive(false);
        _sceneLoader = GameObject.Find("_sceneLoader").GetComponent<SceneLoader>();
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
        if(_stepindex == 4)
        {
            // _dynamicText.text = "Lids placed on petridishes: " + _lidsPlacedTotal + "/"+ _dishesTotalAmount;
        }
        if(_stepindex == 8)
        {
            _dynamicText.text = "Requirement 2 completed: Player has grabbed new mask";
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

                _previousButton.SetActive(true);
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

                _stepindex++;
                Debug.Log("stepindex: " + _stepindex);
                break;

            case 5: //&& _requirement2Met
                _title.text = "Great";
                _text.text = "Now pick up a lid on the counter and do the same. See if you can steer the air by tilting the lid in any direction.";
                _stepindex++;
                Debug.Log("stepindex: " + _stepindex);
                break;

            case 6:
                _title.text = "Product";
                _text.text = "The petridish will be located under the HEPA filters in first air. Your task is to place a lid over the product without contaminating it so that the product can be safely transported to the next stage of production.";
                _stepindex++;
                Debug.Log("stepindex: " + _stepindex);
                _buttonText.text = "Next";
                // _image_goodPractice.SetActive(false);
                break;

            case 7:
                _title.text = "Good practice";
                _text.text = "A good practice when placing lids is to tilt the lid towards yourself so the angle allows air to flow away from the product.";
                _stepindex++;
                Debug.Log("stepindex: " + _stepindex);
                // _image_goodPractice.SetActive(true);
                _nextButton.SetActive(true);
                _buttonText.text = "Understood";
                break;

            case 8: //&& _hasGrabbedNewMask
                _title.text = "Exercise";
                _text.text = "Place the lids on the 3 respective products, it's okay if one of them gets contaminated as this is just training. But you will be judged during the exam later on.";
                _stepindex++;
                Debug.Log("stepindex: " + _stepindex);
                // _image_goodPractice.SetActive(false);
                _buttonText.text = "Next";
                // _products.SetActive(true);
                //button should say: Understood
                // _nextButton.SetActive(false);
                break;

            case 9: // && _requirement3Met
                _title.text = "Great";
                _text.text = "You now know the basic principles of uni directional flow, and you have practiced how to place a lid on a product without contaminating it.";
                _stepindex++;
                Debug.Log("stepindex: " + _stepindex);
                //button should say: Understood
                _nextButton.SetActive(true);
                _buttonText.text = "Next";
                break;

            case 10:
                _title.text = "End of tutorial";
                _text.text = "You have completed the tutorial, you can now proceed to the next module";
                _stepindex++;
                Debug.Log("stepindex: " + _stepindex);
                //button should say: End Tutorial
                _previousButton.SetActive(false);
                _nextButton.SetActive(false);
                _endTutorialButton.SetActive(true);
                // _buttonText.text = "End Tutorial";
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
