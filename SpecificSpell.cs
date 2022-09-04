using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpecificSpell : MonoBehaviour
{ }
/*
    public int spellCleanUpAfterSeconds;
    protected DateTime expiration;
    

    public abstract void spellAffectPolymorphic(GameObject gameobject);

    public abstract void spellAffectOverTimePolymorphic(GameObject gameobject);

    public abstract void spellAffectOverTimeEndPolymorphic(GameObject gameobject);

    public virtual void onSpellCleanUpPolymorphic()
    {

        //Debug.Log("onSpellCleanUpPolymorphic");
        //Debug.Log(gameObject);
        int hotbarIndex = SaveAndLoad.Get().FindHotbarIndexForSkill(gameObject);
        if(hotbarIndex != -1 && spellCleanUpAfterSeconds != 0)
        {
            //Debug.Log("ClearHotbarGlow SpecificSpell  " + hotbarIndex);
            GameObject hotbar = GameObject.Find("Hotbar" + hotbarIndex.ToString());
            hotbar.GetComponent<GeneralSpawn>().ClearHotbarGlow();
            Destroy(gameObject, .1f);
        }
        
    }

    public abstract void spellAffectOnSpecificTroopPolymorphic();

    // Start is called before the first frame update
    public virtual void Awake()
    {
        expiration = DateTime.Now + new TimeSpan(0, 0, 0, spellCleanUpAfterSeconds);
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
        if(expiration < DateTime.Now)
        {
            //Debug.Log("specificspellUpdate");
            onSpellCleanUpPolymorphic();
        }
    }
}
*/