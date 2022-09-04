using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealDamage : DamageNew
{
    public override void specialDamageEffectOnEnemy(GameObject gameobject)
    {
        if(gameobject.name == "ContactCollider" && gameobject.GetComponent<AllEffectsOnTroop>() != null)
        {
            gameobject.GetComponent<AllEffectsOnTroop>().AddToList(new InstantHeal(gameobject.transform.parent.gameObject, 5));
        }
    }
}
