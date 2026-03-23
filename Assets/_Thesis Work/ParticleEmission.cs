using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEmission : MonoBehaviour
{
     public AudioSource _audioSource;
    public ParticleSystem _particleSystem;
    public AudioDetection _audioDetectionScript;

    public float _loudnessSensibility = 100f;
    public float _threshold = 0.1f;
    public float _emissionRate = 20f;

    public ParticleSystem.EmissionModule _PS_emission;

    void Start()
    {
        _PS_emission = _particleSystem.emission;
        _particleSystem.Play();
    }
    void Update()
    {
        float loudness = _audioDetectionScript.GetLoudnessFromMicrophone() * _loudnessSensibility;
        if (loudness < _threshold)
        {
            loudness = 0f;
            _PS_emission.rateOverTime = 0f;
            return;

        }
        _PS_emission.rateOverTime = _emissionRate;
    }


    void ToggleParticlesystem()
    {
        
    }
    
}
