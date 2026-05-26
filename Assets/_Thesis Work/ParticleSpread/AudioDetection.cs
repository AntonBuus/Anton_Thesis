using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.XR.Oculus;
using UnityEngine;

public class AudioDetection : MonoBehaviour
{
    public int _sampleWindow = 64;
    private AudioClip _microphoneClip;

    public enum PreferredMic { Oculus, Laptopmic, No_mic }
    [SerializeField] public PreferredMic preferredMic = PreferredMic.Laptopmic;
    
    private int _mixIndex = 0;
    void Start()
    {
        if (preferredMic == PreferredMic.Oculus)
        {
            _mixIndex = 1;
        } 
        if (preferredMic == PreferredMic.Laptopmic)
        {
            _mixIndex = 0;
        }
        MicrophoneToAudioClip();
        Debug.Log("audiosettings sample rate: " + AudioSettings.outputSampleRate);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void MicrophoneToAudioClip()
    {
        // switch (preferredMic)
        // {
        //     case PreferredMic.Oculus:
        //         _mixIndex = 0;
        //         break;
        //     case PreferredMic.Laptopmic:
        //         _mixIndex = 1;
        //         Debug.Log("selected laptopmic.");
        //         break;
        //     case PreferredMic.No_mic:
        //         Debug.Log("No microphone selected.");
        //         return;
        // }
        string _microphoneName = Microphone.devices[_mixIndex];
        Debug.Log($"Selected microphone: {_microphoneName}");
        Debug.Log("Microphone to select from: " + Microphone.devices.Length);
        _microphoneClip = Microphone.Start(_microphoneName, true, 20, AudioSettings.outputSampleRate);
    }

    public float GetLoudnessFromMicrophone()
    {
        return GetLoudnessFromAudioClip(Microphone.GetPosition(Microphone.devices[_mixIndex]), _microphoneClip);
    }
    
    public float GetLoudnessFromAudioClip(int clipPosition, AudioClip clip)
    {
        int startPosition = Mathf.Max(0, clipPosition - _sampleWindow);
        float[] waveData = new float[_sampleWindow];
        clip.GetData(waveData, startPosition);

        float totalLoudness = 0f;
        for (int i = 0; i < _sampleWindow; i++)
        {
            totalLoudness += Mathf.Abs(waveData[i]);
        }
        return totalLoudness / _sampleWindow;
    }

}
