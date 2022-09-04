using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealAllSpellScript : SpecificSpellNew
{
    public override void specialDamageEffectOnDeath(GameObject gameobject)
    {
        
    }

    public override void specialDamageEffectOnEnemy(GameObject gameobject)
    {
        
    }

    public override void spellAffectOnSpecificTroopPolymorphic()
    {
        
    }

    public override void spellAffectOverTimeEndPolymorphic(GameObject gameobject)
    {
        
    }

    public override void spellAffectOverTimeNotNeedingTrigger()
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("team1");
        for (int i = 0; i < gameObjects.Length; i++)
        {
            if (gameObjects[i].name == "ContactCollider")
            {
                gameObjects[i].GetComponentInParent<AllEffectsOnTroop>().AddToList(new InstantHeal(gameObjects[i].transform.parent.gameObject, 100));
            }

        }
    }

    public override void spellAffectOverTimePolymorphic(GameObject gameobject)
    {
        
    }

    public override void spellAffectPolymorphic(GameObject gameobject)
    {
        
        
    }
}
