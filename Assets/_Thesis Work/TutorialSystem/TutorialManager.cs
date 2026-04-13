using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public GameObject[] popUps;
    private int popUpIndex;
    public GameObject spawner;

    private bool step1done = false;

    void Update()
    {
        for (int i = 0; i < popUps.Length; i++)
        {
            if (i == popUpIndex)
            {
                popUps[i].SetActive(true);
            }
            else
            {
                popUps[i].SetActive(false);
            }
        }
        
    }

    public void Step1()
    {
        if(popUpIndex ==0)
        {
            popUps[0].SetActive(false);
            popUpIndex++;
            popUps[1].SetActive(true);
        }
    }
    public void Step2()
    {
        if (popUpIndex == 1)
        {
            popUpIndex++;
        }
    }
    public void Step3()
    {
        if (popUpIndex == 2)
        {
            popUpIndex++;
        }
    }
    public void Step4()
    {
        if (popUpIndex == 3)
        {
            popUpIndex++;
        }
    }
    public void Step5()
    {
        if (popUpIndex == 4)
        {
            popUpIndex++;
            spawner.SetActive(true);
        }
    }
}
