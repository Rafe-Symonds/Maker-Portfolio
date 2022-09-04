using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private Vector3 orginalPos;


    public void SetHeight()
    {
        GameObject wizard = GameObject.Find("Wizard");
        float healthPercentage = 0;
        if(wizard != null)
        {
            healthPercentage = (float) wizard.transform.Find("ContactCollider").GetComponent<HealthNew>().health / 
                                        wizard.transform.Find("ContactCollider").GetComponent<HealthNew>().GetTotalHealth();
        }

        float pivot = 1.5f - healthPercentage;
        //Debug.Log(healthPercentage);
        //Debug.Log("***************** " + height);
        //Debug.Log("------------ " + positionY);
        RectTransform mask = gameObject.transform.Find("Mask").GetComponent<RectTransform>();
        mask.pivot = new Vector2(0.5f, pivot);

        gameObject.transform.Find("Mask").Find("Fill").position = orginalPos;


    }










    // Start is called before the first frame update
    void Start()
    {
        orginalPos = gameObject.transform.Find("Mask").Find("Fill").position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        SetHeight();
    }
}
