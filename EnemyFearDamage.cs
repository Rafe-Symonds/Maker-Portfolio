using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFearDamage : DamageNew
{
    bool feared = false;
    public override void specialDamageEffectOnEnemy(GameObject gameobject)
    {
        if(feared == false)
        {
            gameobject.GetComponentInChildren<AllEffectsOnTroop>().AddToList(new FearEffect(5, gameobject));
            feared = true;
        }
        
        //Destroy(gameObject);
    }
}
