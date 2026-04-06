using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Inspired by Valem tutorial https://youtu.be/txF4t1qynyk?si=HqltcawRaV4pzQxa
public class MirrorScript : MonoBehaviour
{
    public Transform playerTarget;
    public Transform mirrorCamera;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 localPlayer = mirrorCamera.InverseTransformPoint(playerTarget.position);
        transform.position = mirrorCamera.TransformPoint(new Vector3(localPlayer.x, localPlayer.y, -localPlayer.z));

        Vector3 lookatmirror = mirrorCamera.TransformPoint(new Vector3(-localPlayer.x, localPlayer.y, localPlayer.z));
        transform.LookAt(lookatmirror);
    }
}
