using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataCollectionManager : MonoBehaviour
{
    [Header("What data to collect this scene")]
    public bool _positionData = false;
    public bool _rotationData = false;
    public bool _speechData = false;
    public bool _particleCollisionPositionData = false;

    [Header("Session Settings")]
    [SerializeField] private string baseDataPath = "C:/GitHub/Anton_Thesis/Assets/_Thesis Work/_DataCollection/LoggedData";
    
    public string SessionFolderPath { get; private set; }

    // Singleton instance
    public static DataCollectionManager Instance { get; private set; }

    private void Awake()
    {
        // If an instance already exists, destroy this one
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // Set this as the singleton instance
        Instance = this;
        DontDestroyOnLoad(gameObject);

        CreateSessionFolder();
    }

    private void CreateSessionFolder()
    {
        // Create timestamp and scene name
        string timestamp = System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm");
        // string sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        
        // Create session folder path
        string sessionFolderName = $"{timestamp}";
        SessionFolderPath = Path.Combine(baseDataPath, sessionFolderName);
        
        // Ensure directory exists
        if (!Directory.Exists(SessionFolderPath))
        {
            Directory.CreateDirectory(SessionFolderPath);
            Debug.Log($"Session folder created: {SessionFolderPath}");
        }
    }
}
