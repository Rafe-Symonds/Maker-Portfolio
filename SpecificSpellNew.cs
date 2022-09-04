using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpecificSpellNew : MonoBehaviour
{
    private int spellCleanUpAfterSeconds;
    protected DateTime expiration;

    public void updateSpellCleanUpAfterSeconds(int time)
    {
        spellCleanUpAfterSeconds = time;
    }


    public abstract void specialDamageEffectOnEnemy(GameObject gameobject);

    public abstract void specialDamageEffectOnDeath(GameObject gameobject);

    public abstract void spellAffectPolymorphic(GameObject gameobject);

    public abstract void spellAffectOverTimePolymorphic(GameObject gameobject);

    public abstract void spellAffectOverTimeEndPolymorphic(GameObject gameobject);

    public abstract void spellAffectOverTimeNotNeedingTrigger();

    public virtual void onSpellCleanUpPolymorphic()
    {

        //Debug.Log("onSpellCleanUpPolymorphic");
        //Debug.Log(gameObject);
        int hotbarIndex = SaveAndLoad.Get().FindHotbarIndexForSkill(gameObject);
        if (hotbarIndex != -1 && spellCleanUpAfterSeconds != 0)
        {
            //Debug.Log("ClearHotbarGlow SpecificSpell  " + hotbarIndex);
            //GameObject hotbar = GameObject.Find("Hotbar" + hotbarIndex.ToString());
            //hotbar.GetComponent<GeneralSpawnNew>().ClearHotbarGlow();
            Destroy(gameObject, .1f);
        }
        else
        {
            if(gameObject.name != "ContactCollider" && gameObject.GetComponent<GeneralSpellNew>() != null)
            {
                Destroy(gameObject, .1f);
            }
        }
    }

    public abstract void spellAffectOnSpecificTroopPolymorphic();

    // Start is called before the first frame update
    public virtual void Awake()
    {
        expiration = TimeManagement.CurrentTime() + new TimeSpan(0, 0, 0, spellCleanUpAfterSeconds);
        //Debug.Log(spellCleanUpAfterSeconds);
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (expiration < TimeManagement.CurrentTime())
        {
            //Debug.Log("specificspellUpdate");
            onSpellCleanUpPolymorphic();
        }
    }
}
