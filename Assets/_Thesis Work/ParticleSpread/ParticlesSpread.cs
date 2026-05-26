using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesSpread : MonoBehaviour
{
    public AudioSource _audioSource;
    public ParticleSystem _particleSystem;
    public AudioDetection _audioDetectionScript;

    public float _loudnessSensibility = 100f;
    public float _threshold = 0.1f;

    public ParticleSystem.EmissionModule _PS_emissionModule;

    void Start()
    {
        _PS_emissionModule = _particleSystem.emission;
    }
    void Update()
    {
        float loudness = _audioDetectionScript.GetLoudnessFromMicrophone() * _loudnessSensibility;
        if (loudness < _threshold)
        {
            loudness = 0f;
            DeactivateParticlesystem();
            return;

        }
        ActivateParticlesystem();
    }


    void ToggleParticlesystem()
    {
        
    }
    void ActivateParticlesystem()
    {
        if (!_particleSystem.isPlaying)
        {
            _particleSystem.Play();
            
        }
    }
    void DeactivateParticlesystem()
    {
        if (_particleSystem.isPlaying)
        {
            _particleSystem.Stop();
        }
    }
}
