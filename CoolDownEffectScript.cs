using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoolDownEffectScript : MonoBehaviour
{
    GameObject hotBar;
    Vector3[] vertices;
    void CreateVerticies()
    {
        vertices = new Vector3[]
        {
            new Vector3(hotBar.transform.position.x, hotBar.transform.position.y + hotBar.transform.localScale.y, -1),
            new Vector3(hotBar.transform.position.x + hotBar.transform.localScale.x, hotBar.transform.position.y + 
                                                                        hotBar.transform.localScale.y, -1),
            new Vector3(hotBar.transform.position.x + hotBar.transform.localScale.x, hotBar.transform.position.y, -1),
            new Vector3(hotBar.transform.position.x, hotBar.transform.position.y, -1),   
        };
    }


    void Start()
    {
        hotBar = gameObject.transform.parent.gameObject;
        CreateVerticies();

    }
    void Update()
    {
        
    }
}
