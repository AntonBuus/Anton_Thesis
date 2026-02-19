using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundAssets : MonoBehaviour
{
    private static SoundAssets instance;

    public static SoundAssets Intance
    {
        get
        {
            if (instance == null) instance = Instantiate(Resources.Load<SoundAssets>("SoundAssets"));
            return instance;
        }
    }

    public SoundAudoClip[] soundAudoClipArray;

    [System.Serializable]
    public class SoundAudoClip 
    {
        public SoundManager.Sound sound;
        public AudioClip audioClip;
    }

    

}
