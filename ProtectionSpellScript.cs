using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtectionSpellScript : SpecificSpellNew
{
    public DamageNew.DamageType type;
    public float amount;
    public int duration;
    protected List<GameObject> gameObjectsAlreadyHit = new List<GameObject>(); //may need to be moved into a common class
                                                                     //because other spells might have the same problem

    public override void spellAffectPolymorphic(GameObject gameobject)
    {
        Debug.Log("Before Protected :" + gameobject);


        if (!gameObjectsAlreadyHit.Contains(gameobject) && gameObject.CompareTag(gameobject.tag)) 
        {
            HealthNew health = gameobject.transform.Find("ContactCollider").GetComponent<HealthNew>();
            if (health != null)
            {
                Debug.Log("Protected");
                health.AddProtection(new HealthNew.Protection(type, amount, duration));
            }
            gameObjectsAlreadyHit.Add(gameobject);
        }
    }

    public override void spellAffectOverTimePolymorphic(GameObject gameobject)
    {
        
    }

    public override void spellAffectOverTimeEndPolymorphic(GameObject gameobject)
    {
        
    }

    public override void onSpellCleanUpPolymorphic()
    {
        base.onSpellCleanUpPolymorphic();
    }


    // Update is called once per frame
    void Update()
    {
        //Destroy(gameObject, 1f);
    }

    public override void spellAffectOnSpecificTroopPolymorphic()
    {
        
    }

    public override void specialDamageEffectOnEnemy(GameObject gameobject)
    {
        
    }

    public override void specialDamageEffectOnDeath(GameObject gameobject)
    {
        
    }

    public override void spellAffectOverTimeNotNeedingTrigger()
    {
        
    }
}
