using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevitateEffect : AllEffectsOnTroop.Effect
{
    public LevitateEffect(DateTime expiration, GameObject troop) : base(expiration, troop, AllEffectsOnTroop.Effect.EffectType.replace)
    {
        name = "Levitate";
    }
    public override bool EffectCalledEveryFixedUpdate()
    {
        if (GetDuration() < TimeManagement.CurrentTime())
        {
            troop.GetComponentInChildren<GeneralMovementNew>().levitate = false;

            troop.transform.Find("IconController").GetComponent<IconControllerScript>().DisplayEffects();
            return true;
        }
        return false;
    }

    public override void StartEffect()
    {
        troop.GetComponentInChildren<GeneralMovementNew>().levitate = true;
    }

    
}
