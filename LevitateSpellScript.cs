using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevitateSpellScript : SpecificSpellNew
{
    public override void specialDamageEffectOnDeath(GameObject gameobject)
    {
       
    }

    public override void specialDamageEffectOnEnemy(GameObject gameobject)
    {
        
    }

    public override void spellAffectOnSpecificTroopPolymorphic()
    {
        GeneralSpellNew generalSpell = gameObject.GetComponent<GeneralSpellNew>();
        GameObject troop = generalSpell.Gettroop();
        

        //Debug.Log(troop);
        troop.transform.Find("ContactCollider").GetComponent<AllEffectsOnTroop>().AddToList(new LevitateEffect(expiration, troop));
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
        
    }
}
