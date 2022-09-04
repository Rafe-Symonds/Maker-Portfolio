using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowEffect : AllEffectsOnTroop.Effect
{
    public SlowEffect(DateTime expiration, GameObject troop) : base(expiration, troop, AllEffectsOnTroop.Effect.EffectType.ignore)
    {
        name = "Slow";
    }
    public SlowEffect(int seconds, GameObject troop) : base(seconds, troop, AllEffectsOnTroop.Effect.EffectType.ignore)
    {
        name = "Slow";
    }

    public override bool EffectCalledEveryFixedUpdate()
    {
        if (expirationTime < TimeManagement.CurrentTime())
        {
            //reverse effect
            GameObject contactCollider = troop.transform.Find("ContactCollider").gameObject;
            if(contactCollider == null || contactCollider.GetComponent<GeneralMovementNew>() == null)
            {
                return true;
            }
            contactCollider.GetComponent<GeneralMovementNew>().speed *= 2;
            Animator animator = troop.GetComponent<Animator>();
            animator.speed *= 1.5f;
            return true;
        }
        return false;
    }

    public override void StartEffect()
    {
        GameObject contactCollider = troop.transform.Find("ContactCollider").gameObject;
        contactCollider.GetComponent<GeneralMovementNew>().speed *= 0.5f;
        Animator animator = troop.GetComponent<Animator>();
        animator.speed *= 0.667f;
        
    }
}
