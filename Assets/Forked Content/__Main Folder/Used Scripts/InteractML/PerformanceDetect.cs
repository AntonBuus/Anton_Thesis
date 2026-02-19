using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InteractML;

//Borrowed from Anton's ML hand-in
public class PerformanceDetect : MonoBehaviour
{
    [PullFromIMLController]
    public int _userPerformance;

    private int _expertValue = 1;
    private int _beginnerValue = 0;
    public GameObject expertCheckSymbol;

    private void Start()
    {
        _userPerformance = 1;
    }
    private void Update()
    {
        if (_userPerformance == _expertValue)
        {
            expertCheckSymbol.SetActive(false);
        }
        else if (_userPerformance == _beginnerValue)
        {
            expertCheckSymbol.SetActive(true);
        }

    }
}
