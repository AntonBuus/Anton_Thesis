using System.Collections.Generic;
using UnityEngine;

// OBS NOT IN USE CURRENTLY
public class LogParticleHitPosition : MonoBehaviour
{
    [Header("Data Collection Settings")]
    [SerializeField] DataCollectionManager _dataCollectionManagerScript;
    [SerializeField] private ParticleSystem _speechPartcileSystem1;
    [SerializeField] private ParticleSystem _speechPartcileSystem2;
    [SerializeField] private float _collisionDistance = 0.05f;
    [SerializeField] private int _maxLoggedCollisionsPerFixedUpdate = 200;

    private readonly List<string> timeStamps = new();
    private readonly List<string> _timeSeconds = new();
    private readonly List<Vector3> collisionPositionData = new();
    private readonly List<uint> _particle1Seeds = new();
    private readonly List<uint> _particle2Seeds = new();

    private ParticleSystem.Particle[] _particles1 = new ParticleSystem.Particle[0];
    private ParticleSystem.Particle[] _particles2 = new ParticleSystem.Particle[0];

    [Header("Session Info")]
    [Tooltip("Name of the session for file naming.")]
    public string sessionName = "Session";
 
    private string csvFilePath;

    private void Awake()
    {
        _speechPartcileSystem1 = this.transform.GetChild(1).GetComponent<ParticleSystem>();
        _speechPartcileSystem2 = this.transform.GetChild(2).GetComponent<ParticleSystem>();

        if (_dataCollectionManagerScript == null)
        {
            _dataCollectionManagerScript = GameObject.Find("_DataCollection_Manager")?.GetComponent<DataCollectionManager>();
        }
    }

    private void Start()
    {
        if (_dataCollectionManagerScript == null)
        {
            Debug.LogError("DataCollectionManager was not found. Collision positions cannot be logged.");
            enabled = false;
            return;
        }

        sessionName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        string timeStamp = System.DateTime.Now.ToString("MMM-dd-HH-mm");
        string baseFileName = $"{sessionName}{gameObject.name}_{timeStamp}";

        // Use the session folder from DataCollectionManager
        csvFilePath = System.IO.Path.Combine(_dataCollectionManagerScript.SessionFolderPath, baseFileName + ".csv");
    }

    private void FixedUpdate()
    {
        if (_dataCollectionManagerScript == null || !_dataCollectionManagerScript._particleCollisionPositionData)
        {
            return;
        }

        if (_speechPartcileSystem1 == null || _speechPartcileSystem2 == null || _speechPartcileSystem1 == _speechPartcileSystem2)
        {
            return;
        }

        LogPairCollisions();
    }

    private void LogPairCollisions()
    {
        int count1 = _speechPartcileSystem1.GetParticles(_particles1);
        if (_particles1.Length < count1)
        {
            _particles1 = new ParticleSystem.Particle[count1];
            count1 = _speechPartcileSystem1.GetParticles(_particles1);
        }

        int count2 = _speechPartcileSystem2.GetParticles(_particles2);
        if (_particles2.Length < count2)
        {
            _particles2 = new ParticleSystem.Particle[count2];
            count2 = _speechPartcileSystem2.GetParticles(_particles2);
        }

        if (count1 == 0 || count2 == 0)
        {
            return;
        }

        float distanceSqr = _collisionDistance * _collisionDistance;
        int loggedThisFrame = 0;

        for (int i = 0; i < count1; i++)
        {
            Vector3 pos1 = GetParticleWorldPosition(_speechPartcileSystem1, _particles1[i].position);

            for (int j = 0; j < count2; j++)
            {
                Vector3 pos2 = GetParticleWorldPosition(_speechPartcileSystem2, _particles2[j].position);

                if ((pos1 - pos2).sqrMagnitude > distanceSqr)
                {
                    continue;
                }

                var t = System.TimeSpan.FromSeconds(Time.time);
                timeStamps.Add(t.ToString(@"mm\:ss\,fff"));
                _timeSeconds.Add(Time.time.ToString("F2"));
                collisionPositionData.Add((pos1 + pos2) * 0.5f);
                _particle1Seeds.Add(_particles1[i].randomSeed);
                _particle2Seeds.Add(_particles2[j].randomSeed);

                loggedThisFrame++;
                if (loggedThisFrame >= _maxLoggedCollisionsPerFixedUpdate)
                {
                    return;
                }
            }
        }
    }

    private Vector3 GetParticleWorldPosition(ParticleSystem source, Vector3 particlePosition)
    {
        var main = source.main;
        switch (main.simulationSpace)
        {
            case ParticleSystemSimulationSpace.World:
                return particlePosition;
            case ParticleSystemSimulationSpace.Local:
                return source.transform.TransformPoint(particlePosition);
            case ParticleSystemSimulationSpace.Custom:
                if (main.customSimulationSpace != null)
                {
                    return main.customSimulationSpace.TransformPoint(particlePosition);
                }
                return source.transform.TransformPoint(particlePosition);
            default:
                return source.transform.TransformPoint(particlePosition);
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
        if (collisionPositionData.Count == 0) return;

        var lines = new List<string>();
        lines.Add("Timestamp;TimeSeconds;Particle1Seed;Particle2Seed;SpeechPosX;SpeechPosY;SpeechPosZ");

        int maxCount = Mathf.Max(collisionPositionData.Count, Mathf.Max(timeStamps.Count, _timeSeconds.Count));
        for (int i = 0; i < maxCount; i++)
        {
            string timestamp = i < timeStamps.Count ? timeStamps[i] : string.Empty;
            string timeSeconds = i < _timeSeconds.Count ? _timeSeconds[i] : string.Empty;
            string seed1 = i < _particle1Seeds.Count ? _particle1Seeds[i].ToString() : string.Empty;
            string seed2 = i < _particle2Seeds.Count ? _particle2Seeds[i].ToString() : string.Empty;
            string line = $"{timestamp};{timeSeconds};{seed1};{seed2}";

            if (i < collisionPositionData.Count)
            {
                line += $";{collisionPositionData[i].x};{collisionPositionData[i].y};{collisionPositionData[i].z}";
            }

            lines.Add(line);
        }

        System.IO.File.WriteAllLines(csvFilePath, lines);
        Debug.Log($"Data saved to: {csvFilePath}");
    }
}
