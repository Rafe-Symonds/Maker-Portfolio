using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkyTest : MonoBehaviour
{

    public Gradient skyColor;
    private SpriteRenderer sky;
    public float colorValue;
    public enum DayOrNight { Day, Night};
    public DayOrNight dayOrNight;
    private DateTime nextTimeMove = TimeManagement.CurrentTime();
    private TimeSpan timeBetweenColorIncrease = new TimeSpan(0, 0, 0, 0, 100);
    // Start is called before the first frame update

    public void MoveTime()
    {
        sky.color = skyColor.Evaluate(colorValue);
        if( dayOrNight == DayOrNight.Day)
        {
            if(nextTimeMove < TimeManagement.CurrentTime())
            {
                colorValue += 0.001f;
                nextTimeMove = TimeManagement.CurrentTime() + timeBetweenColorIncrease;
            }
            if(colorValue > 1f)
            {
                dayOrNight = DayOrNight.Night;
            }
            
        }
        else if( dayOrNight == DayOrNight.Night)
        {
            if (nextTimeMove < TimeManagement.CurrentTime())
            {
                colorValue -= 0.001f;
                nextTimeMove = TimeManagement.CurrentTime() + timeBetweenColorIncrease;
            }
            if (colorValue < 0f)
            {
                dayOrNight = DayOrNight.Day;
            }
        }

        

    }

    
    void Start()
    {
        sky = gameObject.GetComponent<SpriteRenderer>();

        MoveTime();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MoveTime();
    }
}
