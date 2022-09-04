using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealSpellScript : SpecificSpellNew
{
    List<GameObject> gameObjectsAlreadyHit = new List<GameObject>();
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
        
    }

    public override void spellAffectOverTimePolymorphic(GameObject gameobject)
    {
        
    }

    public override void spellAffectPolymorphic(GameObject gameobject)
    {
        Debug.Log("Heal Spell triggering");
        if (!gameObjectsAlreadyHit.Contains(gameobject) && gameObject.CompareTag(gameobject.tag))
        {
            gameobject.GetComponentInChildren<AllEffectsOnTroop>().AddToList(new InstantHeal(gameobject, 8));
            gameObjectsAlreadyHit.Add(gameobject);
        }
    }
}
