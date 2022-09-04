using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolTipButton : MonoBehaviour
{
    private EmptyBackgroundScript emptyBackgroundScript;
    public GameObject troopOnToolTipRow;



    

    public void SelectTroop()
    {
        emptyBackgroundScript.SelectTroopFromToolTip(troopOnToolTipRow);
        ToolTipNew.HideToolTip_Static();
    }









    // Start is called before the first frame update
    void Start()
    {
        emptyBackgroundScript = GameObject.Find("EmptyBackground").GetComponent<EmptyBackgroundScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
