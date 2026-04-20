using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialStepsUDF : MonoBehaviour
{
    private int _stepindex;
    
    [Header("UI Elements")]
    public TextMeshProUGUI _title;
    public TextMeshProUGUI _text;
    public TextMeshProUGUI _buttonText;
    public GameObject _nextButton;
    public GameObject _previousButton;
    public GameObject _endTutorialButton;
    public GameObject _image_goodPractice;

    [Header("Toggleable Props")]
    public GameObject _UDF_System;
    public GameObject _products;

    [Header("Tutorial step variables")]
    //Change these
    private bool _requirement1Met = false;
    private bool _requirement2Met = false;
    private bool _requirement3Met = false;


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
        _title.text = "Welcome";
        _text.text = "This module will introduce the concept of contamination particle from speech.";
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
    public void StepNext()
    {
        if(_stepindex == -1)
        {
            _title.text = "Welcome";
            _text.text = "This module will introduce the concept of contamination particle from speech.";
            _stepindex++;
            Debug.Log("stepindex: " + _stepindex);

            _previousButton.SetActive(false);
            return;
        }
        if(_stepindex ==0)
        {
            _title.text = " Clean Air";
            _text.text = "Clean air in cleanrooms is achieved through the use of HEPA filters and uni directional flow, which helps to remove particles from the air and prevent contamination.";
            _stepindex++;
            Debug.Log("stepindex: " + _stepindex);

            _previousButton.SetActive(true);
            return;
        }
        if(_stepindex ==1)
        {
            _title.text = "First Air";
            _text.text = "This clean air is known as first air, and will be degraded to second air once it comes into contact with a person or object, which can contaminate the air with particles.";
            _stepindex++;
            Debug.Log("stepindex: " + _stepindex);
            return;
        }
        if(_stepindex ==2)
        {
            _title.text = "Second Air";
            _text.text = "Second air can manifest as turbulence and carry particles to critical parts, which can be contaminated.";
            _stepindex++;
            Debug.Log("stepindex: " + _stepindex);
            return;
        }
        if(_stepindex ==3)
        {
            _title.text = "Air visualization";
            _text.text = "We have visualized the different types of air under the HEPA filter to your left: Green air indicates first air. If you or an object comes into contact with the first air, it will be degraded to second air, which is indicated by changing the streams to yellow.";
            _stepindex++;
            Debug.Log("stepindex: " + _stepindex);
            //Show picture here
            _UDF_System.SetActive(true);


            return;
        }
        if(_stepindex ==4 ) //&& _requirement1Met
        {
            _title.text = "Exercise";
            _text.text = "To get a feel for how the air can act in a cleanroom, try to put your hand in the air stream and notice how it is moved and degraded to second air.";
            _stepindex++; 
            Debug.Log("stepindex: " + _stepindex);
            return;
        }
        if(_stepindex ==5 ) //&& _requirement2Met
        {
            _title.text = "Great";
            _text.text = "Now pick up a lid on the counter and do the same. See if you can steer the air by tilting the lid in any direction.";
            _stepindex++; 
            Debug.Log("stepindex: " + _stepindex);
            return;
        }
        if(_stepindex ==6)
        {
            _title.text = "Product";
            _text.text = "The petridish will be located under the HEPA filters in first air. Your task is to place a lid over the product without contaminating it so that the product can be safely transported to the next stage of production.";
            _stepindex++;
            Debug.Log("stepindex: " + _stepindex);
            _buttonText.text = "Next";
            _image_goodPractice.SetActive(false);

            return;
        }
        if(_stepindex ==7)
        {
            _title.text = "Good practice";
            _text.text = "A good practice when placing lids is to tilt the lid towards yourself so the angle allows air to flow away from the product.";
            _stepindex++;
            Debug.Log("stepindex: " + _stepindex);
            _image_goodPractice.SetActive(true);
            _nextButton.SetActive(true);
            _buttonText.text = "Understood";
            return;
        }
        if(_stepindex ==8) //&& _hasGrabbedNewMask 
        {
            _title.text = "Exercise";
            _text.text = "Place the lids on the 3 respective products, it's okay if one of them gets contaminated as this is just training. But you will be judged during the exam later on.";
            _stepindex++;
            Debug.Log("stepindex: " + _stepindex);
            _image_goodPractice.SetActive(false);
            _buttonText.text = "Next";
            _products.SetActive(true);
            //button should say: Understood
            // _nextButton.SetActive(false);
            return;
        }
        if(_stepindex ==9 ) // && _requirement3Met
        {
            _title.text = "Great";
            _text.text = "You now know the basic principles of uni directional flow, and you have practiced how to place a lid on a product without contaminating it.";
            _stepindex++;
            Debug.Log("stepindex: " + _stepindex);
            //button should say: Understood
            _nextButton.SetActive(true);
            _buttonText.text = "Next";
            return;
        }
        if(_stepindex ==10 )
        {
            _title.text = "End of tutorial";
            _text.text = "You have completed the tutorial, you can now proceed to the next module";
            _stepindex++;
            Debug.Log("stepindex: " + _stepindex);
            //button should say: End Tutorial
            _previousButton.SetActive(false);
            _nextButton.SetActive(false);
            _endTutorialButton.SetActive(true);
            // _buttonText.text = "End Tutorial";
            return;
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
