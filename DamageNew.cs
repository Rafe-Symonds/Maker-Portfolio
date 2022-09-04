using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageNew : SpecificSpellNew
{
    DamageNew[] damages;
    DamageNew troopSpecialDamage;


    public class DamageEffect : BaseEffect
    {
        public float amount;
        public DamageEffect(float amount, int duration)
        {
            if(amount > 1)
            {
                name = "Strength";
            }
            else
            {
                name = "Weakness";
            }
            TimeSpan timeLeft = new TimeSpan(0, 0, duration);
            this.amount = amount;
            this.expirationTime = TimeManagement.CurrentTime() + timeLeft;
        }
        /*
        public override GameObject GetImage()
        {
            IconManager iconManager = GameObject.Find("EmptyBackground").GetComponent<IconManager>();
            return iconManager.damageIncrease;
        }
        */
    }
    public ListWithExpirations<DamageEffect> damageEffects = new ListWithExpirations<DamageEffect>();







    public float damageAmount;
    public enum DamageType {fire, water, earth, air, cold, magic, physical, poison, undead, living, disease};
    public DamageType[] damageTypes;


    public override void specialDamageEffectOnDeath(GameObject gameobject)
    {
        
    }
    public override void specialDamageEffectOnEnemy(GameObject gameobject)
    {
        
    }




    public float GetDamageAmount()
    {
        float tempDamageAmount = damageAmount;
        foreach(DamageEffect damageEffect in damageEffects.GetList())
        {
            tempDamageAmount = tempDamageAmount * damageEffect.amount;
            
        }
        //Debug.Log(tempDamageAmount);
        return tempDamageAmount;
    }

    public void AddDamageEffect(DamageEffect damageEffect)
    {
        damageEffects.AddEffect(damageEffect);
        GameObject iconController = gameObject.transform.parent.Find("IconController").gameObject;
        iconController.GetComponent<IconControllerScript>().ChangeEffect();
    }

    public override void spellAffectPolymorphic(GameObject gameobject)
    {
        //In spell so health is taken from collider

        HealthNew health;
        DamageNew damage = GetComponent<DamageNew>();
        Transform contactCollider = gameobject.transform.Find("ContactCollider");
        if(contactCollider != null)
        {
            health = contactCollider.GetComponent<HealthNew>();
        }
        else
        {
            health = gameobject.GetComponent<HealthNew>();
        }


        
        //Debug.Log(health);
        //Debug.Log(damage);
        //Debug.Log(gameobject);
        if (health != null)
        {
            if (!gameObject.tag.Equals(health.tag) && !health.tag.Equals(gameObject.tag + "Base") && !health.tag.Equals("background") && !gameObject.tag.Equals(health.tag + "Base"))
            {
                health.TakeDamage(damage);
            }
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

    public override void spellAffectOnSpecificTroopPolymorphic()
    {

    }


    public void Start()
    {
        /*
        damages = gameObject.GetComponents<DamageNew>();

        foreach (DamageNew damage in damages)
        {
            if (damage.name != "DamageNew")
            {
                troopSpecialDamage = damage;
            }
        }
        */
    }
    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (damageEffects.CleanUp())
        {
            gameObject.transform.parent.Find("IconController").GetComponent<IconControllerScript>().ChangeEffect();
            Debug.Log("readjusting the current icons");
        }
    }

    public override void spellAffectOverTimeNotNeedingTrigger()
    {
        
    }
}
