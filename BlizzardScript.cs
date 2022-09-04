using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlizzardScript : SpecificSpellNew
{
    public float amount;
    private int durationInSeconds = 10000; //Stops in triggerExit
    private GeneralMovementNew.MovementAdjustment movement;

    public override void specialDamageEffectOnEnemy(GameObject gameobject)
    {

    }

    public override void spellAffectPolymorphic(GameObject gameobject)
    {
        if (gameobject.CompareTag("team2"))
        {
            GeneralMovementNew generalMovement = gameobject.GetComponentInChildren<GeneralMovementNew>();
            if (generalMovement != null && generalMovement.immuneToBlizzard == false)
            {
                
                generalMovement.AddAdjustment(movement);
            }
        }
    }

    public override void spellAffectOverTimePolymorphic(GameObject gameobject)
    {

    }

    public override void spellAffectOverTimeEndPolymorphic(GameObject gameobject)
    {
        if (gameobject.CompareTag("team2")){
            GeneralMovementNew generalMovement = gameobject.GetComponentInChildren<GeneralMovementNew>();
            if (generalMovement != null && generalMovement.immuneToBlizzard == false)
            {
                generalMovement.RemoveAdjustment(movement);
            }
        }
    }

    public override void onSpellCleanUpPolymorphic()
    {
        
        gameObject.transform.position = new Vector3(100, 0, 0);
        //Debug.Log("onSpellCleanUpPolymorphic");
        
        base.onSpellCleanUpPolymorphic();
    }


    public override void Awake()
    {
        base.Awake();
        movement = new GeneralMovementNew.MovementMultiplier(amount, durationInSeconds);
    }

    public override void spellAffectOnSpecificTroopPolymorphic()
    {
        
    }

    public override void specialDamageEffectOnDeath(GameObject gameobject)
    {
        
    }

    public override void spellAffectOverTimeNotNeedingTrigger()
    {
        
    }
}