using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ManaBar : MonoBehaviour
{

    public Gradient gradient;
    private Image fill;

    private TimeSpan rechargeRate = new TimeSpan(0,0,0,0,125);
    private DateTime nextRecharge = TimeManagement.CurrentTime();
    public float manaReachargeRate;
    private int flashing = 0;
    private DateTime flashChangeTime = TimeManagement.CurrentTime();
    private TimeSpan flashTime = new TimeSpan(0, 0, 0, 0, 100);


    public float maxMana;
    private float currentMana;
    private float height;      //Controls the mask.  This actually refers to the width, but I am calling it height so it makes more sense in code
    private float positionY;   //Controls the mask.
    private float pivot;
    private Vector3 orginalPos;
    public AudioSource notEnoughMana;
    //   PosY:  -67.05 -> -19.91     difference = 47.14
    //   Height: 0  -> 93.92  
    //   width decreases by 1.95871 times as fast

    public bool CheckAndReduceMana(int manaUsage)
    {
        if (currentMana < manaUsage)
        {
            if(notEnoughMana.clip != null)
            {
                notEnoughMana.PlayOneShot(notEnoughMana.clip);
            }
            fill.color = gradient.Evaluate(1f); //red
            flashing = 3;
            flashChangeTime = TimeManagement.CurrentTime() + flashTime;
            return false;
        }
        else
        {
            currentMana -= manaUsage;
            return true;
        }
    }
    
    public void StartMana()
    {
        currentMana = (maxMana / 2);
        fill.color = gradient.Evaluate(0);
    }

    public void SetMaxMana(int mana)
    {
        maxMana = mana;
    }

    public void SetHeight()
    {
        float manaPercentage = currentMana / maxMana;
        height = 93.92f * manaPercentage;
        positionY = 47.14f * manaPercentage - 67.05f;
        pivot = 1.5f - manaPercentage;
        //Debug.Log(manaPercentage);
        //Debug.Log("***************** " + height);
        //Debug.Log("------------ " + positionY);
        RectTransform mask = gameObject.transform.Find("Mask").GetComponent<RectTransform>();
        mask.pivot = new Vector2(pivot, 0.5f);
        gameObject.transform.Find("Mask").Find("Fill").position = orginalPos;
         

    }
    // Start is called before the first frame update
    void Start()
    {
        orginalPos = gameObject.transform.Find("Mask").Find("Fill").position;
        fill = gameObject.transform.Find("Mask").Find("Fill").GetComponent<Image>();
        StartMana();
        SetHeight();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        

        if( nextRecharge < TimeManagement.CurrentTime() && currentMana < maxMana)
        {
            currentMana += manaReachargeRate;
            nextRecharge = TimeManagement.CurrentTime() + rechargeRate;
            SetHeight();
        }




        


        if(flashing > 2)
        {
            if( flashChangeTime < TimeManagement.CurrentTime())
            {
                //Debug.Log("Flashing > 2");
                flashing = 2;
                fill.color = gradient.Evaluate(0f); //blue
                flashChangeTime = TimeManagement.CurrentTime() + flashTime;
            }
        }
        else if( flashing > 1)
        {
            if (flashChangeTime < TimeManagement.CurrentTime())
            {
                //Debug.Log("Flashing > 1");
                flashing = 1;
                fill.color = gradient.Evaluate(1f); //red
                flashChangeTime = TimeManagement.CurrentTime() + flashTime;
            }
        }
        else if (flashing > 0)
        {
            if (flashChangeTime < TimeManagement.CurrentTime())
            {
                //Debug.Log("Flashing > 0");
                flashing = 0;
                fill.color = gradient.Evaluate(0f); //blue
                flashChangeTime = TimeManagement.CurrentTime() + flashTime;
            }
        }
        
    }
}
