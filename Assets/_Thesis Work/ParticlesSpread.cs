using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesSpread : MonoBehaviour
{
    public ParticleSystem _particleSystem;
    private float micThreshold = 0.1f;

    void Start()
    {
        _particleSystem = GetComponent<ParticleSystem>();
        if (_particleSystem == null)
        {
            Debug.LogError("ParticleSystem component not found on this GameObject");
        }
    }

    void Update()
    {
        float micInput = GetMicrophoneInput();
        
        if (micInput > micThreshold)
        {
            if (!_particleSystem.isPlaying)
            {
                _particleSystem.Play();
            }
        }
        else
        {
            if (_particleSystem.isPlaying)
            {
                _particleSystem.Stop();
            }
        }
    }

    float GetMicrophoneInput()
    {
        if (!Microphone.IsRecording(null))
        {
            Microphone.Start(null, true, 1, 44100);
        }

        int position = Microphone.GetPosition(null);
        if (position < 128)
            return 0f;

        AudioClip micClip = Microphone.Start(null, true, 1, 44100);
        
        if (micClip == null)
            return 0f;

        float[] samples = new float[128];
        micClip.GetData(samples, position - 128);

        float sum = 0f;
        foreach (float sample in samples)
        {
            sum += Mathf.Abs(sample);
        }

        return sum / samples.Length;
    }
}
