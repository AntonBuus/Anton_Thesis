using UnityEngine;

public class FootprintManager
{
    private GameObject footprint;
    private Shader footprintShader;

    public FootprintManager(GameObject go, Shader shader) {
        footprint = go;
        footprintShader = shader;
    }
    public GameObject CreateFootprint(bool isLeft) 
    {
        GameObject newFootprint = GameObject.Instantiate(footprint);
        Renderer rend = newFootprint.GetComponent<Renderer>();
        rend.material = new Material(footprintShader);

        Footprint comp = newFootprint.GetComponent<Footprint>();
        comp.isLeft = isLeft;
        
        if (isLeft) {
            newFootprint.transform.localScale = new Vector3(-newFootprint.transform.localScale.x, newFootprint.transform.localScale.y, newFootprint.transform.localScale.z);
        } 
        
        return newFootprint;
    }

    public void PositionFootstep(GameObject go, float offset, Vector3 origin, Vector3 direction) 
    {
        Footprint comp = go.GetComponent<Footprint>();
        bool isLeft = comp.isLeft;

        float xOffsetScale = isLeft ? 0.1f : -0.1f;

        go.transform.position = origin + (direction * offset);
        go.transform.rotation = Quaternion.LookRotation(-direction);
        go.transform.position +=  go.transform.right * xOffsetScale;
    }

    public void FadeFootstep(GameObject go, float saturation) 
    {
        Renderer rend = go.GetComponent<Renderer> ();
        rend.material.SetFloat("_Distance", saturation);
    }
}
