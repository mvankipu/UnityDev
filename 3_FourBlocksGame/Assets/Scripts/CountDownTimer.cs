using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class CountDownTimer
/**
 Set up a count down timer with a particular time limit.
Any other class can query it to see if the timer is complete when deltaTime is sent over.
 */
{
    float timerLimit;
    float countDownTimeTracker;

    string name = "ANON";

    public CountDownTimer(float timerLimit = 2.0f, string name = "ANON")
    {
        this.name = name;
        this.timerLimit = timerLimit;

        Reset();
    }

    public void Reset()
    {
        countDownTimeTracker = timerLimit;
        //UnityEngine.Debug.Log("Reset Time: " + timerLimit.ToString());
    }

    public bool IsComplete(float deltaTime = 0.0f)
    {
        countDownTimeTracker -= deltaTime;

        if (countDownTimeTracker <= 0.0)
        {
            UnityEngine.Debug.Log("Expired" + name + " with timer value of: " + timerLimit.ToString());
            return true;
        }
        else
        {
            if(name!="ANON")
                UnityEngine.Debug.Log("Timer value of: " + timerLimit.ToString());
            return false;
        }
    }  
}
