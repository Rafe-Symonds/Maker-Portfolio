using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardLifeCycle : LifeCycleNew
{
    public override void Death(Animator animator)
    {
        if(dead == false)
        {
            

            base.Death(animator);

            AfterGameScreenScript.ShowVictoryScreen_Static();
            AfterGameScreenScript afterGameScreenScript = GameObject.Find("AfterGameScreen").GetComponent<AfterGameScreenScript>();
            //Debug.Log("***** Wizard Life " + MapPlayLevelButtonScript.levelNumber);
            GameObject base2 = GameObject.Find("Base2");
            base2.GetComponent<GeneralEnemyPlayer>().attack = false;
        }
        
        
    }
}
