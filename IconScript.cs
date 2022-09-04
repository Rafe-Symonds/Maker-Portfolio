using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IconScript : MonoBehaviour
{
    TextMeshPro timerObject;
    TextMeshPro nameObject;
    public int timer;
    TimeSpan oneSecondTimer = new TimeSpan(0, 0, 1);
    DateTime time;
    public bool stop = false;


    // Start is called before the first frame update
    void Start()
    {
        nameObject = gameObject.transform.Find("Canvas").Find("Name").GetComponent<TextMeshPro>();
        nameObject.enabled = false;
        nameObject.text = gameObject.name.Substring(0, gameObject.name.Length - 7); // this substring is to remove the "(Clone)" off of the end
        if (nameObject.text.Contains("Protection"))
        {
            int indexOfProtection = nameObject.text.IndexOf("Protection");
            nameObject.text = nameObject.text.Substring(0, nameObject.text.Length - indexOfProtection - 2) + " Protection";
        }
        nameObject.text = nameObject.text.Substring(0, 1).ToUpper() + nameObject.text.Substring(1);
        timerObject = gameObject.transform.Find("Canvas").Find("Timer").GetComponent<TextMeshPro>();
        timerObject.GetComponent<MeshRenderer>().sortingOrder = 10;
        time = TimeManagement.CurrentTime();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(gameObject != null && stop == false)
        {
            if (time < TimeManagement.CurrentTime())
            {
                timer--;
                timerObject.text = timer.ToString();
                time = TimeManagement.CurrentTime() + oneSecondTimer;
            }

            if (timer <= 0)
            {
                timerObject.text = "";
                //Destroy(gameObject);
                //Debug.Log("Destroying icon through timer");
            }
            if (timer > 3000)
            {
                timerObject.text = "8";

                GameObject timer = gameObject.transform.Find("Canvas").Find("Timer").gameObject;
                timer.GetComponent<RectTransform>().SetPositionAndRotation(new Vector3(gameObject.transform.position.x + 0.01f,
                                            gameObject.transform.position.y - 0.2f, 0), Quaternion.Euler(0, 0, 90));
                timerObject.fontSize = 1f;

            }
        }
    }
    void OnMouseOver()
    {
        nameObject.enabled = true;
    }
    void OnMouseExit()
    {
        nameObject.enabled = false;
    }
    public void SetTimer(double timeLeft)
    {
        timer = (int)(timeLeft + 0.5);
        //Debug.Log("timer = " + timer);
        
    }
}
