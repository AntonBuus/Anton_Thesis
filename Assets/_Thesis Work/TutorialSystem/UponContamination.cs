using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UponContamination : MonoBehaviour
{
    private Material _productMaterial;
    TrackContamination _trackContaminationScript;
    void Start()
    {
        _productMaterial = GetComponent<Renderer>().material;
        _trackContaminationScript = GameObject.Find("TrackContamination").GetComponent<TrackContamination>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_productMaterial.color == Color.red)
        {
            Debug.Log("Contamination detected on " + gameObject.name);

        }
    }
    public void CountContamination()
    {
        if (_productMaterial.color == Color.red)
        {
            Debug.Log("Contamination detected on " + gameObject.name);
            _trackContaminationScript._contaminatedDishesAmount++;
            Debug.Log("Contaminated dishes amount: " + _trackContaminationScript._contaminatedDishesAmount);
        }
    }
}
