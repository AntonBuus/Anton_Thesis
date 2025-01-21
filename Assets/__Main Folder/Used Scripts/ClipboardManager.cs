using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClipboardManager : MonoBehaviour
{
    [SerializeField] GameObject _Check1;
    [SerializeField] GameObject _Check2;
    [SerializeField] GameObject _Check3;
    [SerializeField] GameObject _Check4;
    [SerializeField] GameObject _Check5;
    
    
    public void Task1Completed()
    {
        _Check1.SetActive(true);
    }

    public void Task2Completed()
    {
        _Check2.SetActive(true);
    }

    public void Task3Completed()
    {
        _Check3.SetActive(true);
    }

    public void Task4Completed()
    {
        _Check4.SetActive(true);
    }

    public void Task5Completed()
    {
        _Check5.SetActive(true);
    }

}
