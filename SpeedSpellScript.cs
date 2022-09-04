using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedSpellScript : SpecificSpellNew
{
    public float amount;
    private int durationInSeconds = 10000; //Stops in triggerExit
    private GeneralMovement.MovementAdjustment movement;
    public override void spellAffectPolymorphic(GameObject gameobject)
    {
        movement = new GeneralMovement.MovementMultiplier(amount, durationInSeconds);
        GeneralMovement generalMovement = gameobject.GetComponent<GeneralMovement>();
        generalMovement.AddAdjustment(movement);
    }

    public override void spellAffectOverTimePolymorphic(GameObject gameobject)
    {
        
    }

    public override void spellAffectOverTimeEndPolymorphic(GameObject gameobject)
    {
        GeneralMovement generalMovement = gameobject.GetComponent<GeneralMovement>();
        generalMovement.RemoveAdjustment(movement);
    }

    public override void onSpellCleanUpPolymorphic()
    {
        base.onSpellCleanUpPolymorphic();
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public override void spellAffectOnSpecificTroopPolymorphic()
    {
        
    }

    public override void specialDamageEffectOnEnemy(GameObject gameobject)
    {
        throw new System.NotImplementedException();
    }

    public override void specialDamageEffectOnDeath(GameObject gameobject)
    {
        throw new System.NotImplementedException();
    }

    public override void spellAffectOverTimeNotNeedingTrigger()
    {
        throw new System.NotImplementedException();
    }
}
