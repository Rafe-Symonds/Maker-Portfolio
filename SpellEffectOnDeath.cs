using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellEffectOnDeath : SpecificSpellNew
{
    public GameObject objectToInstantiateOnDeath;
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
        
    }

    public override void onSpellCleanUpPolymorphic()
    {
        base.onSpellCleanUpPolymorphic();
        //Debug.Log("Arrow Clean Up");
    }

    private void OnDestroy()
    {
        GameObject effectObject = Instantiate(objectToInstantiateOnDeath, transform.position, Quaternion.identity);
        effectObject.tag = gameObject.tag;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
