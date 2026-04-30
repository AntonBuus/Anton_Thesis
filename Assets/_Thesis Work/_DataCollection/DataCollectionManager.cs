using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.InputSystem.Users;

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

    //-----------------------------------

    private List<string> timeStamps = new();
    private List<string> _timeSeconds = new();
    private List<int> _startedEvaluation = new();
    private List<int> _started1 = new();
    private List<int> _started2 = new();
    private List<float> _userSpoken = new();

    private int _pendingStartedEvaluation;
    private int _pendingStarted1;
    private int _pendingStarted2;
    private float _loudnessLogged;
    // private List<Vector3> positionData = new();

    [Header("Session Info")]
    [Tooltip("Name of the session for file naming.")]
    public string sessionName = "Session";
 
    private string csvFilePath;
    private void Start()
    {
        sessionName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        string timeStamp = System.DateTime.Now.ToString("MMM-dd-HH-mm");
        string baseFileName = $"{sessionName}_PlayerActions_{timeStamp}";

        // Use the session folder from DataCollectionManager
        csvFilePath = System.IO.Path.Combine(SessionFolderPath, baseFileName + ".csv");
    }

    void FixedUpdate()
    {
        LogActionsTaken();
    }

    private void LogActionsTaken()
    {
        var t = System.TimeSpan.FromSeconds(Time.time);
        timeStamps.Add(t.ToString(@"mm\:ss\,fff")); 
        _timeSeconds.Add(Time.time.ToString("F2")); 
        _userSpoken.Add(_loudnessLogged > 0 ? _loudnessLogged : 0);

        _startedEvaluation.Add(_pendingStartedEvaluation > 0 ? 1 : 0);
        _started1.Add(_pendingStarted1 > 0 ? 1 : 0);
        _started2.Add(_pendingStarted2 > 0 ? 1 : 0);

        // _userSpoken = 0f;
        _pendingStartedEvaluation = 0;
        _pendingStarted1 = 0;
        _pendingStarted2 = 0;

        
    }

    // ExploitAudio _exploitAudioScript;
    public void LogLoudnessAction(float loudness)
    {
        _loudnessLogged = loudness;

    }

    public void LogStartedEvaluationAction()
    {
        _pendingStartedEvaluation = 1;
    }

    public void LogStarted1Action()
    {
        _pendingStarted1 = 1;
    }

    public void LogStarted2Action()
    {
        _pendingStarted2 = 1;
    }

    // private void OnApplicationQuit()
    // {
    //     SaveData();
    // }
    private void OnDisable() //on scene end
    {
        SaveData();
    }
    public void SaveData()
    {
        SaveDataToCSV();
    }

    public void SaveDataToCSV()
    {
        var lines = new List<string>();
        lines.Add("Timestamp;TimeSeconds;UserSpoke;StartedEvaluation;Started1;Started2");

        int count = timeStamps.Count;
        for (int i = 0; i < count; i++)
        {
            lines.Add($"{timeStamps[i]};{_timeSeconds[i]};{_userSpoken[i]};{_startedEvaluation[i]};{_started1[i]};{_started2[i]}");
        }


        System.IO.File.WriteAllLines(csvFilePath, lines);
        Debug.Log($"Data saved to: {csvFilePath}");
    }
}
