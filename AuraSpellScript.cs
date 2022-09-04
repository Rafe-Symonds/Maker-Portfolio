using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuraSpellScript : SpecificSpellNew
{
    public override void onSpellCleanUpPolymorphic()
    {
        base.onSpellCleanUpPolymorphic();
    }

    public override void spellAffectOnSpecificTroopPolymorphic()
    {
        
    }

    public override void spellAffectOverTimeEndPolymorphic(GameObject gameobject)
    {
        
    }

    public override void spellAffectOverTimePolymorphic(GameObject gameobject)
    {
        
    }


    public class AuraEffect : AllEffectsOnTroop.Effect
    {
        public AuraEffect(DateTime experation, GameObject troop) : base(experation, troop, AllEffectsOnTroop.Effect.EffectType.replace)
        {
            name = "Aura";
        }
        public override bool EffectCalledEveryFixedUpdate()
        {
            if (GetDuration() < TimeManagement.CurrentTime())
            {
                Debug.Log("Hide HealthBar");
                troop.GetComponent<HealthNew>().HideHealthBar();
                return true;
            }
            return false;
        }
        public override void StartEffect()
        {
            troop.transform.Find("ContactCollider").GetComponent<HealthNew>().ShowHealthBar();
        }
        /*
        public override GameObject GetImage()
        {
            IconManager iconManager = GameObject.Find("EmptyBackground").GetComponent<IconManager>();
            return iconManager.growth;
        }
        */
    }
    public override void spellAffectPolymorphic(GameObject gameobject)
    {
        Debug.Log("spellAffectPolymorphic");
        
        gameobject.transform.Find("ContactCollider").GetComponent<AllEffectsOnTroop>().AddToList(new AuraEffect(expiration, gameobject));
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, 1f);
    }

    public override void specialDamageEffectOnEnemy(GameObject gameobject)
    {
        throw new NotImplementedException();
    }

    public override void specialDamageEffectOnDeath(GameObject gameobject)
    {
        throw new NotImplementedException();
    }

    public override void spellAffectOverTimeNotNeedingTrigger()
    {
        throw new NotImplementedException();
    }
}
