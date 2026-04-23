using System.Collections.Generic;
using UnityEngine;

public class LogRotation : MonoBehaviour
{
    [Header("Data Collection Settings")]
    [SerializeField] DataCollectionManager _dataCollectionManagerScript;

    private List<string> timeStamps = new();
    private List<string> _timeSeconds = new();
    private List<Vector3> rotationData = new();

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
        string timeStamp = System.DateTime.Now.ToString("MMM-dd-HH-mm");
        string baseFileName = $"{sessionName}{gameObject.name}_{timeStamp}";

        string dataFilePath = "C:/GitHub/Anton_Thesis/Assets/_Thesis Work/_DataCollection/LoggedData";
  
        csvFilePath = System.IO.Path.Combine(dataFilePath, baseFileName + ".csv");
    }

    void FixedUpdate()
    {
        LogRotationInfo();
    }

    private void LogRotationInfo()
    {
        var t = System.TimeSpan.FromSeconds(Time.time);
        timeStamps.Add(t.ToString(@"mm\:ss\,fff")); 
        _timeSeconds.Add(Time.time.ToString("F2")); 

        if (_dataCollectionManagerScript._rotationData)
        {
            rotationData.Add(transform.eulerAngles);
        }
    }

    private void OnApplicationQuit()
    {
        SaveData();
    }

    public void SaveData()
    {
        SaveDataToCSV();
    }

    public void SaveDataToCSV()
    {
        if (rotationData.Count == 0) return;

        var lines = new List<string>();
        lines.Add("Timestamp;TimeSeconds;RotX;RotY;RotZ");

        int maxCount = Mathf.Max(rotationData.Count, Mathf.Max(timeStamps.Count, _timeSeconds.Count));
        for (int i = 0; i < maxCount; i++)
        {
            string timestamp = i < timeStamps.Count ? timeStamps[i] : string.Empty;
            string timeSeconds = i < _timeSeconds.Count ? _timeSeconds[i] : string.Empty;
            string line = $"{timestamp};{timeSeconds}";

            if (i < rotationData.Count)
            {
                line += $";{rotationData[i].x};{rotationData[i].y};{rotationData[i].z}";
            }

            lines.Add(line);
        }

        System.IO.File.WriteAllLines(csvFilePath, lines);
        Debug.Log($"Data saved to: {csvFilePath}");
    }
}
