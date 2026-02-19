using UnityEngine;
using UnityEngine.UI; // For referencing UI elements

public class HideGroupOnButtonClick : MonoBehaviour
{
    // Reference to the UI group you want to hide/show
    public GameObject groupToHide;

    // Reference to the Button component
    public Button hideButton;

    void Start()
    {
        // Check if hideButton is assigned
        if (hideButton != null)
        {
            // Add a listener to the button click event
            hideButton.onClick.AddListener(HideGroup);
        }
        else
        {
            Debug.LogError("Hide button is not assigned in the Inspector.");
        }
    }

    // Method to hide or show the group
    public void HideGroup()
    {
        if (groupToHide != null)
        {
            // Toggle the active state of the group (hide if visible, show if hidden)
            groupToHide.SetActive(!groupToHide.activeSelf);
        }
    }
}
