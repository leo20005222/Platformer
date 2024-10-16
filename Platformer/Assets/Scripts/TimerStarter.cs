using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerTester : MonoBehaviour
{
    public Timer timer; //time 참조

    void Start()
    {
        timer.TimeLimit = 10f; 
        timer.StartTimer(); 
    }
}

