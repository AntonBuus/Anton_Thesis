using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SoundManager
{
    public enum Sound
    {
        Button,
        Oven,
        Belt,
        Fail,
        Success,
        Other,
    }
    public static void PlaySound(Sound sound)
    {
        GameObject soundGameObject = new GameObject("Sound");
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        audioSource.PlayOneShot(GetAudioClip(sound));
    }
    private static AudioClip GetAudioClip(Sound sound)
    {
        foreach (SoundAssets.SoundAudoClip soundAudoClip in SoundAssets.Intance.soundAudoClipArray)
        {
            if (soundAudoClip.sound == sound)
            {
                return soundAudoClip.audioClip;
            }
        }
        Debug.LogError("Sound " + sound + " not found!");
        return null;
    }

/*
Example:
SoundManager.PlaySound(SoundManager.Sound.xxx);
xxx = Sound type (See Sound enum)


SoundManager.PlaySound(SoundManager.Sound.Oven);
SoundManager.PlaySound(SoundManager.Sound.Success);
SoundManager.PlaySound(SoundManager.Sound.Fail);



*/
}
