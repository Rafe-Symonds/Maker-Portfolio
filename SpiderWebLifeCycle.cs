using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderWebLifeCycle : LifeCycle
{
    public GameObject webPrefab;
    public override void Leave(GameObject other)
    {
        LifeCycle otherLifeCycle = other.GetComponent<LifeCycle>();
        if (otherLifeCycle != null && otherLifeCycle.CheckHasProperty(Properties.Webable))
        {
            System.Random rand = new System.Random();
            int randomNumber = rand.Next(0, 99);
            if(randomNumber < 20)
            {
                Debug.Log("Instantiate Web");
                Instantiate(webPrefab, other.transform.position, Quaternion.identity);
            }
        }
    }






    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
