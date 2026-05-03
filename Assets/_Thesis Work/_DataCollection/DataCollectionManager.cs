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
    private List<float> _userSpoken = new();

    private List<int> _speechModuleEntered = new();
    private List<int> _speechModuleParameter = new();

    private List<int> _udfModuleEntered = new();
    private List<int> _udfStartedExercise = new();
    private List<int> _udfFinishedExercise = new();

    private List<int> _evaluationEntered = new();
    private List<int> _evaluationBegun = new();
    private List<int> _evaluationEnded = new();
   
   

        
    private float _loudnessLogged;
    private int _tempEnteredSpeechModule;
    private int _tempSpeechModuleParameter;

    private int _tempEnteredUDFModule;
    private int _tempUDFStartedExercise;
    private int _tempUDFFinishedExercise;

    private int _tempEnteredEvaluation;
    private int _tempBegunEvaluation;
    private int _tempEndedEvaluation;
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
        _userSpoken.Add(_loudnessLogged);

        _speechModuleEntered.Add(_tempEnteredSpeechModule);
        _speechModuleParameter.Add(_tempSpeechModuleParameter);

        _udfModuleEntered.Add(_tempEnteredUDFModule);
        _udfStartedExercise.Add(_tempUDFStartedExercise);
        _udfFinishedExercise.Add(_tempUDFFinishedExercise);

        _evaluationEntered.Add(_tempEnteredEvaluation > 0 ? 1 : 0);
        _evaluationBegun.Add(_tempBegunEvaluation > 0 ? 1 : 0);
        _evaluationEnded.Add(_tempEndedEvaluation > 0 ? 1 : 0);
        
        

        // _userSpoken = 0f;
        
        _tempEnteredSpeechModule = 0;
        _tempSpeechModuleParameter = 0;

        _tempEnteredUDFModule = 0;
        _tempUDFStartedExercise = 0;
        _tempUDFFinishedExercise = 0;

        _tempEnteredEvaluation = 0;
        _tempBegunEvaluation = 0;
        _tempEndedEvaluation = 0;
        
    }

    // ExploitAudio _exploitAudioScript;
    public void LogLoudnessAction(float loudness)
    {
        _loudnessLogged = loudness;
        // Debug.Log($"method called: {loudness}");

    }

    
    public void LogSpeechModuleEntered()
    {
        _tempEnteredSpeechModule = 1;
    }
    public void LogSpeechParameter()
    {
        _tempSpeechModuleParameter = 1;
    }
    public void LogUDFModuleEntered()
    {
        _tempEnteredUDFModule = 1;
    }
    public void LogUDFStartedExercise()
    {
        _tempUDFStartedExercise = 1;
    }
    public void LogUDFFinishedExercise()
    {
        _tempUDFFinishedExercise = 1;
    }
    public void LogEvaluationModuleEntered()
    {
        _tempEnteredEvaluation = 1;
    }
    public void LogStartedEvaluationAction()
    {
        _tempBegunEvaluation = 1;
    }
    public void LogEndedEvaluationAction()
    {
        _tempEndedEvaluation = 1;
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
        lines.Add("Timestamp;TimeSeconds;UserSpoke;SpeechModuleEntered;SpeechModuleParameter;UDFModuleEntered;UDFModuleStarted;UDFModuleFinished;EvaluationModuleEntered;EvaluationModuleBegun;EvaluationModuleEnded");

        int count = timeStamps.Count;
        for (int i = 0; i < count; i++)
        {
            lines.Add($"{timeStamps[i]};{_timeSeconds[i]};{_userSpoken[i]};{_speechModuleEntered[i]};{_speechModuleParameter[i]};{_udfModuleEntered[i]};{_udfStartedExercise[i]};{_udfFinishedExercise[i]};{_evaluationEntered[i]};{_evaluationBegun[i]};{_evaluationEnded[i]}");
        }


        System.IO.File.WriteAllLines(csvFilePath, lines);
        Debug.Log($"Data saved to: {csvFilePath}");
    }
}
