using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

//Part of Update made with assistance of AI
public class TranportBetweenPoints : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float duration = 2.0f;

    public ClipboardManager _clipboardManager;

    public LightManager LightManager;
    private bool isMoving = false;
    private float t = 0.0f;

    private bool hasMoved = false;

    public bool item1ready
        { get; set; }

    public bool item2ready
        { get; set; }

    public bool item3ready
        { get; set; }

    private void Start()
    {
        transform.position = pointA.position;
    }
    void Update()
    {
        if (isMoving)
        {
            t += Time.deltaTime / duration;
            transform.position = Vector3.Lerp(pointA.position, pointB.position, t);

            if (t >= 1.0f)
            {
                isMoving = false;
                t = 0.0f;
            }
        }
    }

    public void StartMoving()
    {
        Debug.Log("start moving called");
        if (!isMoving && !hasMoved && item1ready && item2ready && item3ready)
        {
            _clipboardManager.Task5Completed();
            AudioManager.Instance.PlaySound("Sucess", 0.5f);
            isMoving = true;
            t = 0.0f;
            hasMoved = true;
            LightManager.ToggleLight();
        }
        else if (!isMoving && !hasMoved && !item1ready && !item2ready && !item3ready)
        {
            AudioManager.Instance.PlaySound("Fail", 0.5f);
        }
      
    }
}
