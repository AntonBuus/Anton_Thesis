using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Inspired by https://youtu.be/QL29aTa7J5Q?si=1EslXKg1KK9CCqgb 
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public Sound[] _sounds;
    public AudioSource audioSource;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void PlaySound(string name, float Volume)
    {
        Sound s = Array.Find(_sounds, x => x.name == name);
        if (s == null)
        {
            Debug.Log("Sound not found");
        }
        else
        {
            audioSource.clip = s.clip;
            audioSource.PlayOneShot(audioSource.clip, Volume);
        }
    }
}
