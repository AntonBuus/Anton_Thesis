// Import necessary namespaces
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Main class for handling microphone input registration
public class RegisterMicInput : MonoBehaviour
{
    // Stores the audio clip from microphone input
    private AudioClip micInput;
    // Audio source component for playing microphone input
    private AudioSource audioSource;
    // Sample rate constant set to CD quality
    private const int sampleRate = 44100;
    // Maximum recording duration in seconds
    private const int recordingLengthSeconds = 10;
    // Index of the currently selected microphone device
    [SerializeField] private int selectedDeviceIndex = 0;

    // Called when the script instance is being loaded
    void Start()
    {
        // Initialize audio source if not already set
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();

        // Log all available microphone devices
        LogAvailableDevices();
        // Log current audio level
        LogAudioLevel();
    }

    // Called once per frame
    void Update()
    {
        // Log registered audio input status during gameplay
        if (Microphone.IsRecording(Microphone.devices[selectedDeviceIndex]))
        {
            Debug.Log($"Recording from: {Microphone.devices[selectedDeviceIndex]}");
        }
    }

    // Logs the current audio source volume level
    void LogAudioLevel()
    {
        // Check if audio source exists
        if (audioSource != null)
        {
            // Output the current volume to debug log
            Debug.Log($"Audio level: {audioSource.volume}");
        }
        else
        {
            // Warn if audio source is not initialized
            Debug.LogWarning("AudioSource not initialized");
        }
    }

    // Logs all available microphone devices to console
    void LogAvailableDevices()
    {
        // Check if any microphone devices are available
        if (Microphone.devices.Length == 0)
        {
            // Report error if no devices found
            Debug.LogError("No microphone devices found!");
            return;
        }

        // Begin logging available devices
        Debug.Log("Available microphone devices:");
        // Iterate through all available microphone devices
        for (int i = 0; i < Microphone.devices.Length; i++)
        {
            // Output each device name with its index
            Debug.Log($"[{i}] {Microphone.devices[i]}");
        }
        // Log which device is currently selected
        Debug.Log($"Using device: {Microphone.devices[selectedDeviceIndex]}");
    }

    // Selects a microphone device by index
    public void SelectMicrophoneDevice(int deviceIndex)
    {
        // Validate that the device index is within valid range
        if (deviceIndex < 0 || deviceIndex >= Microphone.devices.Length)
        {
            // Report error for invalid device index
            Debug.LogError($"Invalid device index: {deviceIndex}");
            return;
        }

        // Stop any existing microphone input
        StopMicInput();
        // Update the selected device index
        selectedDeviceIndex = deviceIndex;
        // Start recording from the new device
        StartMicInput();
    }

    // Starts microphone input recording
    void StartMicInput()
    {
        // Check if any microphone devices are available
        if (Microphone.devices.Length == 0)
        {
            // Report error if no devices found
            Debug.LogError("No microphone devices found!");
            return;
        }

        // Get the name of the selected microphone device
        string deviceName = Microphone.devices[selectedDeviceIndex];
        // Start recording from the selected device with specified parameters
        micInput = Microphone.Start(deviceName, true, recordingLengthSeconds, sampleRate);
        
        // Configure and play audio source if it exists
        if (audioSource != null)
        {
            // Set the recorded audio clip to the audio source
            audioSource.clip = micInput;
            // Enable looping so recording continuously plays
            audioSource.loop = true;
            // Start playback of the audio source
            audioSource.Play();
        }

        // Confirm that microphone input has been registered
        Debug.Log($"Microphone input registered from: {deviceName}");
    }

    // Stops microphone input recording
    void StopMicInput()
    {
        // End microphone recording
        Microphone.End(null);
        // Stop audio source if it exists
        if (audioSource != null)
            audioSource.Stop();
    }

    // Called when the script component is disabled
    void OnDisable()
    {
        // Ensure microphone input is stopped when script is disabled
        StopMicInput();
    }
}
