using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CureDiseaseSpellScript : SpecificSpellNew
{
    List<GameObject> gameObjectsAlreadyHit = new List<GameObject>();
    public override void specialDamageEffectOnDeath(GameObject gameobject)
    {
        if (!gameObjectsAlreadyHit.Contains(gameobject))
        {
            gameObjectsAlreadyHit.Add(gameobject);

            AllEffectsOnTroop allEffectsOnTroop = gameobject.GetComponent<AllEffectsOnTroop>();
            allEffectsOnTroop.RemoveEffectType(typeof(DiseaseEffect));
        }
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
        
    }
    void Start()
    {
        //Destroy(gameObject, 1f);
    }
}
