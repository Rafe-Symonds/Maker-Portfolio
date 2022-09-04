using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharmDamage : DamageNew
{
    bool charmed = false;
    public override void specialDamageEffectOnEnemy(GameObject gameobject)
    {
        if(charmed == false)
        {
            gameobject.GetComponentInChildren<AllEffectsOnTroop>().AddToList(new CharmEffect(5, gameobject));
            Destroy(gameObject);
            charmed = true;
        }
        
    }
}
