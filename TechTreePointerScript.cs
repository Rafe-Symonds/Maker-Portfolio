using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TechTreePointerScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int numSkills = SaveAndLoad.Get().GetskillsCollected().Count;
        int numPoints = SaveAndLoad.Get().GettechTreePoints();
        if(!(numSkills <= 4 || numPoints >= 20) || SaveAndLoad.Get().GetLevelsCompleted()[9] > 0)
        {
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
