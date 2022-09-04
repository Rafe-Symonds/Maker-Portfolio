using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDragonScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(SaveAndLoad.Get().GetLevelsCompleted()[6] > 0)
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
