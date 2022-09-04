using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManagement : MonoBehaviour
{
    static TimeSpan timePaused = new TimeSpan(0,0,0);
    static DateTime pauseTime;
    static bool paused = false;

    public static void StartPause()
    {
        pauseTime = DateTime.Now;
        paused = true;
    }
    public static void StopPause()
    {
        timePaused += DateTime.Now - pauseTime;
        paused = false;
    }
    public static DateTime CurrentTime()
    {
        if(paused == true)
        {
            return pauseTime;
        }
        else
        {
            if(timePaused.Milliseconds > 0)
            {
                try
                {
                    return DateTime.Now - timePaused;
                }
                catch(Exception e)
                {
                    Debug.Log("**************  The added or subtracted value results in an un-representable DateTime.");
                    return DateTime.Now;
                }
                
            }
            else
            {
                return DateTime.Now;
            }
        }
    }
    public static void SetPauseTimeToZero()
    {
        timePaused = new TimeSpan(0, 0, 0);
    }
}
