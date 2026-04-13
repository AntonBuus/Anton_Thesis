using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnableAnyGameObejct : MonoBehaviour
{
    [System.Serializable]
    public class GameObjectIntPair
    {
        public GameObject gameObject;
        public int intValue;
    }

    public List<GameObjectIntPair> _toggleableObjects = new List<GameObjectIntPair>();
    
    [Header("InputActions and associated values")]
    public InputActionProperty _leftMenuButton;
    public int _leftMenuButtonValue = 0;

    public InputActionProperty _leftXbutton;
    public int _leftXbuttonValue = 1;
    public InputActionProperty _leftYbutton;
    public int _leftYbuttonValue = 2;

    void Update()
    {
        if (_leftMenuButton.action.WasPressedThisFrame())
        {
            ToggleWithLeftMenuButton();
            Debug.Log("Left Menu button pressed!");
        }
        if (_leftXbutton.action.WasPressedThisFrame())
        {
            ToggleWithLeftXButton();
            Debug.Log("Left X button pressed!");
        }
        if (_leftYbutton.action.WasPressedThisFrame())
        {
            ToggleWithLeftYButton();
            Debug.Log("Left Y button pressed!");
        }
    }

    public void ToggleWithLeftMenuButton()
    {
        _toggleableObjects[_leftMenuButtonValue].gameObject.SetActive(!_toggleableObjects[_leftMenuButtonValue].gameObject.activeSelf);
    }
    public void ToggleWithLeftXButton()
    {
        _toggleableObjects[_leftXbuttonValue].gameObject.SetActive(!_toggleableObjects[_leftXbuttonValue].gameObject.activeSelf);
    }
    public void ToggleWithLeftYButton()
    {
        _toggleableObjects[_leftYbuttonValue].gameObject.SetActive(!_toggleableObjects[_leftYbuttonValue].gameObject.activeSelf);
    }

}
