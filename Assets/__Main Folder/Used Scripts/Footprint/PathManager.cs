using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PathManager : MonoBehaviour
{
    public Transform target, origin;
    public GameObject footprint;
    public Shader footprintShader;
    
    private NavMeshPath path;
    private FootprintManager footprintManager;
    private List<GameObject> footprints;
    private IEnumerator outerCoroutine, innerCoroutine;
    private const float newPathDelay = 5.0f, newStepDelay = 0.7f, offset = 0.5f, leftOffsetCorrection = 0.2f;
    private float elapsed, saturation;
    private int lastCount, totalfootprintNum;
    private bool isLeft;
    
    void Start()
    {
        footprintManager = new FootprintManager(footprint, footprintShader);
        footprints = new List<GameObject>();
        path = new NavMeshPath();

        elapsed = 0.0f;
        lastCount = 0;
        isLeft = false;

        //NavMesh.CalculatePath(origin.position, target.position, NavMesh.AllAreas, path);
        //outerCoroutine = VisualizePath();
        //StartCoroutine(outerCoroutine);
    }

    void Update()
    {
        for (int i = 0; i < path.corners.Length - 1; i++)
            Debug.DrawLine(path.corners[i], path.corners[i + 1], Color.red);

        elapsed += Time.deltaTime;

        if (elapsed >= newPathDelay) {
            elapsed = 0.0f;
            NavMesh.CalculatePath(origin.position, target.position, NavMesh.AllAreas, path);

            StopAllCoroutines();
            lastCount = totalfootprintNum;
            outerCoroutine = VisualizePath();
            StartCoroutine(outerCoroutine);
        }
    }


    private IEnumerator VisualizePath()
    {
        saturation = 0.0f;
        totalfootprintNum = 0;

        for (int i = 0; i < path.corners.Length - 1; i++) {
            innerCoroutine = SpawnAlongVector(path.corners[i], path.corners[i + 1]);
            yield return StartCoroutine(innerCoroutine);
            StopCoroutine(innerCoroutine);
        }

        for (int j = 0; j < lastCount - totalfootprintNum; j++)
        {
            if (totalfootprintNum + j <= footprints.Count)
                footprints[totalfootprintNum + j].SetActive(false);
        }

        lastCount = totalfootprintNum;
        yield break;
    }

    // SpawnAlongVector was inspired by highpockets' answer to this question:
    // https://discussions.unity.com/t/how-can-i-spawn-a-series-of-objects-along-a-path/221851
    private IEnumerator SpawnAlongVector(Vector3 origin, Vector3 destination)
    {
        float distance  = Vector3.Distance(origin, destination);
        int footprintNum = (int)(distance / offset);
        Vector3 direction = (destination - origin).normalized;

        for (int j = 0; j < footprintNum; j++) {
            int indexInArray = totalfootprintNum;
            totalfootprintNum++;
            SpawnOne(indexInArray);

            Footprint comp = footprints[indexInArray].GetComponent<Footprint>();
            float currentOffset = getOffsetForIndex(j, comp.isLeft);

            comp.resetTime();
            footprintManager.PositionFootstep(footprints[indexInArray], currentOffset, origin, direction);
            footprintManager.FadeFootstep(footprints[indexInArray], saturation);

            saturation += 0.5f;

            yield return new WaitForSeconds(newStepDelay);
        }
        yield break;
    }

    private void SpawnOne(int indexInArray)
    {
        if (footprints.Count <= indexInArray) {
            footprints.Add(footprintManager.CreateFootprint(isLeft));
            isLeft = !isLeft;
        } else if (lastCount <= indexInArray) {
            footprints[indexInArray].SetActive(true);
        }
    }

    private float getOffsetForIndex(int indexInPath, bool isCurrentLeft) 
    {
        float currentOffset = offset * (indexInPath + 1);
        currentOffset = isCurrentLeft ? currentOffset - leftOffsetCorrection : currentOffset;

        return currentOffset;
    }
    //void OnDisable()
    //{
    //    StopAllCoroutines();
    //    for (int i = 0; i < footprints.Count; i++)
    //    {
    //        footprints[i].SetActive(false);
    //    }
    //}
}
