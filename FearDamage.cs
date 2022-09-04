using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FearDamage : DamageNew
{

    public override void specialDamageEffectOnEnemy(GameObject gameobject)
    {
        int randomNumber = UnityEngine.Random.Range(0, 100);
        if (randomNumber > 90)
        {
            gameobject.GetComponent<AllEffectsOnTroop>().AddToList(new FearEffect(10, gameobject.transform.parent.gameObject));
        }
    }
}
