using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapFireworks : MonoBehaviour
{
    public GameObject finalScroll;
    // Start is called before the first frame update
    void Start()
    {
        if (SaveAndLoad.Get().GetLevelsCompleted()[9] > 0)
        {
            gameObject.SetActive(true);
            
            Invoke("ShowFinalScroll", 3f);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    public void ShowFinalScroll()
    {
        if(finalScroll != null)
        {
            finalScroll.SetActive(true);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
