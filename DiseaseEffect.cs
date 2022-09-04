using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiseaseEffect : AllEffectsOnTroop.Effect
{
    DateTime nextDamage = TimeManagement.CurrentTime();
    public DiseaseEffect(DateTime expiration, GameObject troop) : base(expiration, troop, AllEffectsOnTroop.Effect.EffectType.replace) 
    {
        name = "Disease";
    }
    public DiseaseEffect(int seconds, GameObject troop) : base(seconds, troop, AllEffectsOnTroop.Effect.EffectType.replace)
    {
        name = "Disease";
    }

    public override bool EffectCalledEveryFixedUpdate()
    {
        if (expirationTime < TimeManagement.CurrentTime())
        {
            return true;
        }
        if (nextDamage < TimeManagement.CurrentTime())
        {
            nextDamage += new TimeSpan(0, 0, 5);
            troop.GetComponentInChildren<HealthNew>().TakeDamage(5, DamageNew.DamageType.disease);
        }
        return false;
    }

    public override void StartEffect()
    {
        
    }

    
}
