using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetedSpawn : MonoBehaviour
{
    public GameObject spawnPoint;
    public GameObject prefab;
    private ManaBar manabar;
    public int manaUsage;
    public bool stationary;
    EmptyBackgroundScript background;


    public void BackgroundClick(Vector3 pos)
    {
        
        if (manabar.CheckAndReduceMana(manaUsage) == true)
        {
            if (stationary == false)
            {
                GameObject instantiate = Instantiate(prefab, spawnPoint.transform.position, Quaternion.identity) as GameObject;
                instantiate.GetComponent<GeneralMovingSpellScript>().Target(pos);
            }
            else
            {
                GameObject instantiate = Instantiate(prefab, Camera.main.ScreenToWorldPoint(Input.mousePosition) , Quaternion.identity) as GameObject;
            }
           
        }
    }

    
    
    void OnMouseDown()
    {



        // background.SetCallBack(new Action<Vector3> (BackgroundClick));
    }

    
    // Start is called before the first frame update
    void Start()
    {
        manabar = GameObject.Find("ManaBarNew").GetComponent<ManaBar>();
        background = GameObject.FindObjectOfType(typeof(EmptyBackgroundScript)) as EmptyBackgroundScript;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
