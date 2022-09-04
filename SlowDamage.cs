using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowDamage : DamageNew
{
    public override void specialDamageEffectOnEnemy(GameObject gameobject)
    {
        if (gameobject.GetComponent<AllEffectsOnTroop>() != null)
        {
            gameobject.GetComponent<AllEffectsOnTroop>().AddToList(new SlowEffect(7, gameobject.transform.parent.gameObject));
        }    
    }
}
