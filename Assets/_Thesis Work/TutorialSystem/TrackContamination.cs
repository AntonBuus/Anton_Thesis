using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;

public class TrackContamination : MonoBehaviour
{
    public int _dishesTotalAmount;
    public int _contaminatedDishesAmount;
    public int _lidsPlacedTotal;
    void Start()
    {
        _dishesTotalAmount = transform.childCount;
        _contaminatedDishesAmount = 0;
        _lidsPlacedTotal = 0;
        Debug.Log("Child count: " + _dishesTotalAmount);
    }

    
    public void IncrementLidsPlaced()
    {
        _lidsPlacedTotal++;
        Debug.Log("Lids placed total: " + _lidsPlacedTotal);
    }

}
