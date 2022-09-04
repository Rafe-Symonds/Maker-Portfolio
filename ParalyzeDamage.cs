using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParalyzeDamage : DamageNew
{
    public override void specialDamageEffectOnEnemy(GameObject gameobject)
    {
        //gameobject.GetComponentInChildren<AllEffectsOnTroop>().AddToList(new (10, gameobject));
    }
}
