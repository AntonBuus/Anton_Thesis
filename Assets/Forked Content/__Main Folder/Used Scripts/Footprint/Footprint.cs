using UnityEngine;

public class Footprint : MonoBehaviour
{
    public bool isLeft;
    private float elapsed = 0.0f;
    private Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer> ();
    }
    
    void FixedUpdate()
    {
        elapsed += Time.deltaTime;
        rend.material.SetFloat("_DeltaTime", elapsed);
    }

    public void resetTime()
    {
        elapsed = 0.0f;
    }
}
