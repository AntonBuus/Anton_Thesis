using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;

public class SceneManager : MonoBehaviour
{
    [Header("=== Scene Settings ===")]

    public string sceneToLoad;
    
    public float delayBeforeSceneLoad = 2f;
    // public AudioSource timeTravelAudio;
    // public ParticleSystem timeTravelEffect;


    public void LoadDesiredScene(string sceneName)
    {
        StartCoroutine(LoadSceneAfterDelay(sceneName));
    }

    private IEnumerator LoadSceneAfterDelay(string sceneName)
    {
        yield return new WaitForSeconds(delayBeforeSceneLoad);
        UnitySceneManager.LoadScene(sceneName);
    }
}
