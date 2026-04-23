using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogPosition : MonoBehaviour
{
    [Header("Data Collection Settings")]
    // [Tooltip("Enable to collect position data of this GameObject.")]
    [SerializeField] DataCollectionManager _dataCollectionManagerScript;

    // [Tooltip("Enable to collect rotation (Euler angles) data of this GameObject.")]
    // public bool COLLECTROTATIONDATA = false;

    [Tooltip("Enable for a single frame to mark it in the data files")]
    public bool _MarkThisFrame = false;

    
    private List<string> timeStamps = new();
    // List to store collected position data
    private List<string> _timeSeconds = new();
    private List<Vector3> positionData = new();

    // List to store collected rotation data (Euler angles in XYZ order)
    private List<Vector3> rotationData = new();

    // List to store boolean frame data for marking frames
    private List<bool> boolFrameData = new();

    // Session name and number for file naming
    [Header("Session Info")]
    [Tooltip("Name of the session for file naming.")]
    public string sessionName = "Session";
 
    // Paths where the CSV and JSON files will be stored
    private string csvFilePath;
    private string jsonFilePath;
    void Awake()
    {
        _dataCollectionManagerScript = GameObject.Find("_DataCollection_Manager").GetComponent<DataCollectionManager>();
    }
    private void Start()
    {
        // Set the CSV and JSON file paths using the convention: NAME_OF_ATTACHED GAMEOBJECT+SESSIONNAME+MMM-dd-HH-mm
        string timeStamp = System.DateTime.Now.ToString("MMM-dd-HH-mm");
        string baseFileName = $"{sessionName}{gameObject.name}_{timeStamp}";

        string dataFilePath = "C:/GitHub/Anton_Thesis/Assets/_Thesis Work/_DataCollection/LoggedData";
  
        csvFilePath = System.IO.Path.Combine(dataFilePath, baseFileName + ".csv");
        jsonFilePath = System.IO.Path.Combine(dataFilePath, baseFileName + ".json");
    }
    void FixedUpdate()
    {
        LogBaseInfo();
    }
    private void Update()
    {
        
        // if (_MarkThisFrame)
        // {
        //     Debug.Log("This frame is marked for special attention in the data files.");
            
        //     boolFrameData.Add(true);
        //     _MarkThisFrame = false; 
        // }
        // else
        // {
        //     boolFrameData.Add(false);
        // }
    }
    private void LogBaseInfo()
    {
        var t = System.TimeSpan.FromSeconds(Time.time);
        timeStamps.Add(t.ToString(@"mm\:ss\,fff")); 
        _timeSeconds.Add(Time.time.ToString("F2")); 

        if (_dataCollectionManagerScript._positionData)
        {
            // Collect the current position of this GameObject
            positionData.Add(transform.position);
        }

        if (_dataCollectionManagerScript._rotationData)
        {
            // Collect the current rotation as Euler angles (XYZ order)
            rotationData.Add(transform.eulerAngles);
        }
    }

    public void MarkThisFrame()
    {
        _MarkThisFrame = true;
    }

    private void OnApplicationQuit()
    {
        SaveData();
    }

    // Call this method to save collected data to CSV and JSON
    public void SaveData()
    {
        SaveDataToCSV();
        // SaveDataToJSON();
    }

    public void SaveDataToCSV()
    {
        if (positionData.Count == 0 && rotationData.Count == 0) return;

        var lines = new List<string>();
        
        // Create header based on what data is being collected
        if (_dataCollectionManagerScript._positionData && _dataCollectionManagerScript._rotationData)
        {
            lines.Add("Timestamp;TimeSeconds;PosX;PosY;PosZ;RotX;RotY;RotZ");
        }
        else if (_dataCollectionManagerScript._positionData)
        {
            lines.Add("Timestamp;TimeSeconds;PosX;PosY;PosZ");
        }
        else if (_dataCollectionManagerScript._rotationData)
        {
            lines.Add("Timestamp;TimeSeconds;RotX;RotY;RotZ");
        }
        // lines.Add("Note: MARKED_FRAME column indicates frames that were marked for special attention (1 = marked, empty = not marked)");

        // Write data rows
        int maxCount = Mathf.Max(positionData.Count, rotationData.Count);
        for (int i = 0; i < maxCount; i++)
        {
            string line = "";
            line += $"{timeStamps[i]};{_timeSeconds[i]}";
            if (_dataCollectionManagerScript._positionData && i < positionData.Count)
            {
                line += ";";
                line += $"{positionData[i].x};{positionData[i].y};{positionData[i].z}";
            }
            if (_dataCollectionManagerScript._rotationData)
            {
                if (_dataCollectionManagerScript._positionData) line += ";";
                if (i < rotationData.Count)
                {
                    line += $"{rotationData[i].x};{rotationData[i].y};{rotationData[i].z}";
                }
            }
            lines.Add(line);
        }

        System.IO.File.WriteAllLines(csvFilePath, lines);
        Debug.Log($"Data saved to: {csvFilePath}");
    }

    // public void SaveDataToJSON()
    // {
    //     if (positionData.Count == 0 && rotationData.Count == 0) return;

    //     var serializableList = new List<SerializableData>();
    //     int maxCount = Mathf.Max(positionData.Count, rotationData.Count);
        
    //     for (int i = 0; i < maxCount; i++)
    //     {
    //         var data = new SerializableData();
    //         data.timeStamp = timeStamps[i];
    //         data.timeSeconds = _timeSeconds[i];
    //         if (_dataCollectionManagerScript._positionData && i < positionData.Count)
    //         {
    //             data.position = new SerializableVector3(positionData[i]);
    //         }
    //         if (_dataCollectionManagerScript._rotationData && i < rotationData.Count)
    //         {
    //             data.rotation = new SerializableVector3(rotationData[i]);
    //         }
    //         // if (i < boolFrameData.Count)
    //         // {
    //         //     data.boolValue = boolFrameData[i] ? 1 : 0;
    //         // }
    //         serializableList.Add(data);
    //     }

    //     string json = JsonUtility.ToJson(new SerializableDataList(serializableList), true);
    //     System.IO.File.WriteAllText(jsonFilePath, json);
    //     Debug.Log($"Data saved to: {jsonFilePath}");
    // }

    [System.Serializable]
    public class SerializableVector3
    {
        public float x, y, z;
        public SerializableVector3(Vector3 v)
        {
            x = v.x;
            y = v.y;
            z = v.z;
        }
    }

    [System.Serializable]
    public class SerializableData
    {
        public string timeStamp;
        public string timeSeconds;
        public SerializableVector3 position;
        public SerializableVector3 rotation;
        public int boolValue;
        
    }

    [System.Serializable]
    public class SerializableDataList
    {
        public List<SerializableData> data;
        public SerializableDataList(List<SerializableData> list)
        {
            data = list;
        }
    }
}
