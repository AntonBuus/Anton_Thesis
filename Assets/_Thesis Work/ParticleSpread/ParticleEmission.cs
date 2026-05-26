using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// credit to Valem for the audio detection code: https://www.youtube.com/watch?v=dzD0qP8viLw
public class ParticleEmission : MonoBehaviour
{
     public AudioSource _audioSource;
    public ParticleSystem _particleSystem;
    public AudioDetection _audioDetectionScript;
   
    public float _loudnessSensibility = 100f;
    public float _threshold = 0.1f;
    public float _emissionRate = 5f;

    public ParticleSystem.EmissionModule _PS_emission;

    void Start()
    {
        _PS_emission = _particleSystem.emission;
        // _particleSystem.Play();
        // _maskBehaviorScript = GameObject.Find("_Snap_Mask").GetComponent<MaskBehavior>();
        
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
        if (_particleSystem.isStopped)
        {
            _particleSystem.Play();
            
        }
        _PS_emission.rateOverTime = _emissionRate;
        // _maskBehaviorScript.ToggleBacteria();

    }


    void ToggleParticlesystem()
    {
        
    }
    
}
