using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{ }
/*
    public class DamageEffect : BaseEffect
    {
        public float amount;
        public DamageEffect(float amount, int duration)
        {
            TimeSpan timeLeft = new TimeSpan(0, 0, duration);
            this.amount = amount;
            this.expirationTime = DateTime.Now + timeLeft;
        }
        public override GameObject GetImage()
        {
            IconManager iconManager = GameObject.Find("EmptyBackground").GetComponent<IconManager>();
            return iconManager.damageIncrease;



        }

    }
    public ListWithExpirations<DamageEffect> damageEffects = new ListWithExpirations<DamageEffect>();







    public float damageAmount;
    public enum DamageType {fire, water, earth, wind, cold, magic, physical, poison, undead};
    public DamageType[] damageTypes;



    public float GetDamageAmount()
    {
        foreach(DamageEffect damageEffect in damageEffects.GetList())
        {
            damageAmount = damageAmount * damageEffect.amount;
        }
        
        return damageAmount;
    }

    public void AddDamageEffect(DamageEffect damageEffect)
    {
        damageEffects.AddEffect(damageEffect);
    }

    public override void spellAffectPolymorphic(GameObject gameobject)
    {
        //In spell so health is taken from collider
        Damage damage = GetComponent<Damage>();
        Health health = gameobject.GetComponent<Health>();
        if (health != null)
        {
            health.TakeDamage(damage);
        }

    }

    public override void spellAffectOverTimePolymorphic(GameObject gameobject)
    {
        spellAffectPolymorphic(gameobject);
    }

    public override void spellAffectOverTimeEndPolymorphic(GameObject gameobject)
    {
        
    }

    public override void onSpellCleanUpPolymorphic()
    {
        //Debug.Log("damage   onSpellCleanUpPolymorphic");
        base.onSpellCleanUpPolymorphic();
        
    }


    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        damageEffects.CleanUp();
    }

    public override void spellAffectOnSpecificTroopPolymorphic()
    {
        
    }
}
*/