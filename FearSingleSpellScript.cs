using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FearSingleSpellScript : SpecificSpellNew
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
        if (troop.CompareTag("team2"))
        {
            GameObject contactCollider = troop.transform.Find("ContactCollider").gameObject;
            if (contactCollider.GetComponent<HealthNew>().CreatureCharacteristicContains(DamageNew.DamageType.living))
            {
                contactCollider.GetComponent<AllEffectsOnTroop>().AddToList(new FearEffect(10, troop));
            }
        }
            
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
