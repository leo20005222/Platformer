using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerStarter : MonoBehaviour
{
    public Timer timer; //time 참조

    void Start()
    {
        timer.TimeLimit = 12f; 
        timer.StartTimer(); 
    }
}

