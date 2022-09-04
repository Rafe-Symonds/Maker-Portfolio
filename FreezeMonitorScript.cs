using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeMonitorScript : MonoBehaviour
{

    private DateTime lastTime = DateTime.Now; 

    // Update is called once per frame
    void FixedUpdate()
    {
       if (DateTime.Now.Millisecond - lastTime.Millisecond > 100)
       {
            Debug.Log("********* time of freezes " + DateTime.Now + "     freeze time in milliseconds = " + (DateTime.Now.Millisecond - lastTime.Millisecond));
       }
       lastTime = DateTime.Now;

    }
}
