using System.Collections;
using System.Collections.Generic;
using TMPro;
// using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.UI;

public class TutorialSteps : MonoBehaviour
{
    private int _stepindex;
    
    public TextMeshProUGUI _title;
    public TextMeshProUGUI _text;
    public TextMeshProUGUI _buttonText;
    public GameObject _nextButton;
    public GameObject _previousButton;
    public GameObject _endTutorialButton;

    [Header("Tutorial step variables")]
    public bool _hasSpoken = false;
    public bool _successfullyContaminatedMask = false;
    public bool _hasGrabbedNewMask = false;


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
        _text.text = "This module will introduce the concept of contamination particle from speech";
        Debug.Log("stepindex: " + _stepindex);
        _maskBehaviorScript = GameObject.Find("_Snap_mask").GetComponent<MaskBehavior>();
        _previousButton.SetActive(false);
        _sceneLoader = GameObject.Find("_sceneLoader").GetComponent<SceneLoader>();
    }

    public void PlayerHasSpoken()
    {
        // Debug.Log("PlayerHasSpoken Called");
        if(_stepindex == 4 && !_hasSpoken)
        {
            _hasSpoken = true;
            Debug.Log("Player has spoken correctly");
            //Should i call StepNext here?
                // StepNext();
        }
    }
    public void PlayerHasGrabbedNewMask()
    {
        // Debug.Log("PlayerHasGrabbedNewMask Called");
        if(_stepindex == 8 && !_hasGrabbedNewMask)
        {
            _hasGrabbedNewMask = true;
            Debug.Log("Player has grabbed new mask correctly");
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
            _title.text = " Particles";
            _text.text = "As a rule, speech should be kept to a minimum when operating in cleanrooms.";
            _stepindex++;
            Debug.Log("stepindex: " + _stepindex);

            _previousButton.SetActive(true);
            return;
        }
        if(_stepindex ==1)
        {
            _title.text = "Particles";
            _text.text = "The reason for this is that particles are spread during speech, which can contaminate critical surfaces.";
            _stepindex++;
            Debug.Log("stepindex: " + _stepindex);
            return;
        }
        if(_stepindex ==2)
        {
            _title.text = "Particles";
            _text.text = "The amount of particles spread depends on the volume of speech, so it is best practice to keep it to a minimum.";
            _stepindex++;
            Debug.Log("stepindex: " + _stepindex);
            return;
        }
        if(_stepindex ==3)
        {
            _title.text = "Try it";
            _text.text = "Try to speak now, notice how particles are emitted from your mouth upon speech. You can also look in the mirror to your right and see the particles you spread.";
            _stepindex++;
            Debug.Log("stepindex: " + _stepindex);

            // _nextButton.SetActive(false);

            return;
        }
        if(_stepindex ==4 && _hasSpoken)
        {
            _title.text = "Well done";
            _text.text = "Now look at the table, notice there is a mask that you can grab, try to grab it and put it on.";
            _stepindex++; 
            Debug.Log("stepindex: " + _stepindex);
            return;
        }
        if(_stepindex ==5 && _maskBehaviorScript.isWearingMask)
        {
            _title.text = "Masks";
           _text.text = "Masks can help reduce the amount of particles emitted from the mouth, which is why it is required to wear one when operating in cleanrooms. Notice how there are now fewer bacteria when you speak?";
            _stepindex++;
            Debug.Log("stepindex: " + _stepindex);
            _buttonText.text = "Next";

            return;
        }
        if(_stepindex ==6 && _successfullyContaminatedMask)
        {
            _title.text = "Mask Contamination";
            _text.text = "Speaking will still contaminate the mask, making it less effective, so speaking should still be kept to a minimum even when wearing a mask";
            _stepindex++;
            Debug.Log("stepindex: " + _stepindex);

            _nextButton.SetActive(true);
            _buttonText.text = "Understood";
            return;
        }
        if(_stepindex ==7) //&& _hasGrabbedNewMask 
        {
            _title.text = "New Mask";
           _text.text = "Grab a new mask from your mask bag, you can find it by pressing X on your left controller, pull out a new one with your right hand and put that on.";
            _stepindex++;
            Debug.Log("stepindex: " + _stepindex);
            //button should say: Understood
            // _nextButton.SetActive(false);
            return;
        }
        if(_stepindex ==8 && _hasGrabbedNewMask)
        {
            _title.text = "Great";
            _text.text = "You now know how contamination can spread even from speaking, and you know how to change your mask. When on the production line, make sure to change your mask if you notice that it is contaminated. Change the mask if it is wet, as it is an indicator of contamination.";
            _stepindex++;
            Debug.Log("stepindex: " + _stepindex);
            //button should say: Understood
            _nextButton.SetActive(true);
            _buttonText.text = "Next";
            return;
        }
        if(_stepindex ==9 )
        {
            _title.text = "End of tutorial";
            _text.text = "You have completed the tutorial, you can now proceed to the next module.";
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
