using System.Collections.Generic;
// using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class LogPosition : MonoBehaviour
{
    [Header("Data Collection Settings")]
    [SerializeField] DataCollectionManager _dataCollectionManagerScript;

    private List<string> timeStamps = new();
    private List<string> _timeSeconds = new();
    private List<Vector3> positionData = new();

    [Header("Session Info")]
    [Tooltip("Name of the session for file naming.")]
    public string sessionName = "Session";
 
    private string csvFilePath;

    void Awake()
    {
        _dataCollectionManagerScript = GameObject.Find("_DataCollection_Manager").GetComponent<DataCollectionManager>();
    }

    private void Start()
    {
        sessionName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        string timeStamp = System.DateTime.Now.ToString("MMM-dd-HH-mm");
        string baseFileName = $"{sessionName}{gameObject.name}_{timeStamp}";

        // Use the session folder from DataCollectionManager
        csvFilePath = System.IO.Path.Combine(_dataCollectionManagerScript.SessionFolderPath, baseFileName + ".csv");
    }

    void FixedUpdate()
    {
        LogPositionInfo();
    }

    private void LogPositionInfo()
    {
        var t = System.TimeSpan.FromSeconds(Time.time);
        timeStamps.Add(t.ToString(@"mm\:ss\,fff")); 
        _timeSeconds.Add(Time.time.ToString("F2")); 

        if (_dataCollectionManagerScript._positionData)
        {
            positionData.Add(transform.parent.position);
        }
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
        if (positionData.Count == 0) return;

        var lines = new List<string>();
        lines.Add("Timestamp;TimeSeconds;PosX;PosY;PosZ");

        int maxCount = Mathf.Max(positionData.Count, Mathf.Max(timeStamps.Count, _timeSeconds.Count));
        for (int i = 0; i < maxCount; i++)
        {
            string timestamp = i < timeStamps.Count ? timeStamps[i] : string.Empty;
            string timeSeconds = i < _timeSeconds.Count ? _timeSeconds[i] : string.Empty;
            string line = $"{timestamp};{timeSeconds}";

            if (i < positionData.Count)
            {
                line += $";{positionData[i].x};{positionData[i].y};{positionData[i].z}";
            }

            lines.Add(line);
        }

        System.IO.File.WriteAllLines(csvFilePath, lines);
        Debug.Log($"Data saved to: {csvFilePath}");
    }
}
