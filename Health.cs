using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{ }
/*
    public Damage.DamageType[] defaultProtections;
    public float[] defaultProtectionValues;
    private int INFINITE = 360000;

    public ListWithExpirations<Protection> protections = new ListWithExpirations<Protection>();
    
    public int health;
    public int totalHealth;
    Sprite healthBar_100;
    Sprite healthBar_75;
    Sprite healthBar_50;
    Sprite healthBar_25;
    private bool healthBarShown = false;





    public class Protection: BaseEffect
    {
        public Damage.DamageType[] types;
        public float[] amount;
        //Asumes types and amounts are the same length
        public Protection(Damage.DamageType[] types, float[] amount, int duration)
        {
            if( types.Length != amount.Length)
            {
                throw new Exception("types.Length != amount.Length");
            }
            TimeSpan timeLeft = new TimeSpan(0, 0, duration);
            this.types = types;
            this.amount = amount;
            this.expirationTime = DateTime.Now  + timeLeft;
        }

        public Protection(Damage.DamageType type, float amount, int duration) :
            this(new Damage.DamageType[] { type }, new float[] { amount }, duration) { }

        public override GameObject GetImage()
        {
            IconManager iconManager = GameObject.Find("EmptyBackground").GetComponent<IconManager>();
            if(amount[0] > 0)
            {
                return iconManager.protection;
            }
            else
            {
                return iconManager.weakness;
            }

            
        }

        //order so that the first one is the shortest length time wise



    }

    public class HealingOverTime : GeneralEffect.Effect
    {
        public Damage.DamageType[] types;
        public float amount;
        private DateTime nextHeal = DateTime.Now;
        private TimeSpan timeBetweenHeals = new TimeSpan(0, 0, 1);
        public HealingOverTime(Damage.DamageType[] types, float amount, int duration) : base(duration)
        {
            TimeSpan timeLeft = new TimeSpan(0, 0, duration);
            this.types = types;
            this.amount = amount;
            
        }
        private float MaxHeal(HealingOverTime heal)
        {
            /*
            float MaxReduction = 0;
            for (int ps = 0; ps < protections.GetList().Count; ps++)
            {
                for (int h = 0; h < heal.types.Length; h++)
                {
                    for (int p = h; p < protections.GetList()[ps].types.Length; h++)
                    {
                        if (protections.GetList()[ps].types[p] == heal.types[h])
                        {
                            MaxReduction = Math.Max(MaxReduction, heal.amount);
                        }
                    }
                }
            }
            return MaxReduction;
            
            return heal.amount;
        }
        public override bool DoAction(GameObject thing)
        {
            Health healthScript = thing.GetComponent<Health>();
            if (healthScript != null)
            {
                if (nextHeal < DateTime.Now)
                {


                    healthScript.health = healthScript.health + (int)(this.amount * MaxHeal(this));

                    

                    nextHeal = DateTime.Now + timeBetweenHeals;
                }
            }
            if(GetDuration() < DateTime.Now)
            {
                return true;
            }
            return false;
        }

        public override GameObject GetImage()
        {
            IconManager iconManager = GameObject.Find("EmptyBackground").GetComponent<IconManager>();
            return iconManager.healing;
        }
    }
    
    public void TakeDamage(Damage damage)
    {
        LifeCycle lifeCycle = gameObject.GetComponent<LifeCycle>();
        float damageAmount = damage.GetDamageAmount();
        //Each protection is additive in a protection we take the largest matching type
        foreach (Protection p in protections.GetList())
        {
            damageAmount -= damageAmount * MaxProtection(p, damage);
            
        }
        health -= Math.Max(0, (int)damageAmount);
        if(healthBarShown == true)
        {
            SetHealthBar();
        }

        if(lifeCycle != null)
        {
            lifeCycle.TakenDamage();
        }
        

        //UnityEngine.Debug.Log(health);
    }
    private float MaxProtection(Protection protection, Damage damage)
    {
        float MaxReduction = 0;
        for (int d = 0; d < damage.damageTypes.Length; d++)
        {
            for (int p = d; p < protection.types.Length; p++)
            {
                if (protection.types[p] == damage.damageTypes[d])
                {
                    MaxReduction = Math.Max(MaxReduction, protection.amount[p]);
                }
            }
        }
        return MaxReduction;
    }
    public void AddProtection(Protection protection)
    {
        protections.AddEffect(protection);
        
    }
    public void ShowHealthBar()
    {
        SetHealthBar();
        healthBarShown = true;

    }
    public void HideHealthBar()
    {
        GameObject healthBar = gameObject.transform.Find("HealthBar").gameObject;
        SpriteRenderer healthBarSprite = healthBar.GetComponent<SpriteRenderer>();
        healthBarSprite.sprite = null;
        healthBarShown = false;
    }
    private void SetHealthBar()
    {
        GameObject healthBar = gameObject.transform.Find("HealthBar").gameObject;
        SpriteRenderer healthBarSprite = healthBar.GetComponent<SpriteRenderer>();
        if (health >= totalHealth)
        {
            healthBarSprite.sprite = healthBar_100;
        }
        else if ((100 * health) / totalHealth >= 75)
        {
            healthBarSprite.sprite = healthBar_75;
        }
        else if ((100 * health) / totalHealth >= 50)
        {
            healthBarSprite.sprite = healthBar_50;
        }
        else if ((100 * health) / totalHealth >= 25)
        {
            healthBarSprite.sprite = healthBar_25;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        healthBar_100 = Resources.Load<Sprite>("Sprites/HealthBar_100");
        healthBar_75 = Resources.Load<Sprite>("Sprites/HealthBar_75");
        healthBar_50 = Resources.Load<Sprite>("Sprites/HealthBar_50");
        healthBar_25 = Resources.Load<Sprite>("Sprites/HealthBar_25");


        totalHealth = health;

        //Pad defaultProtectionValuse so it is the same size as defaultProtections
        float[] protectionValues = new float[defaultProtections.Length];
        for (int i = 0; i < defaultProtections.Length; i++)
        {
            if (i <= defaultProtectionValues.Length - 1)
            {
                protectionValues[i] = defaultProtectionValues[i];
            }
            else
            {
                protectionValues[i] = .15f;
            }

            protections.AddEffect(new Protection(defaultProtections, protectionValues, INFINITE));
        }
    }

    // Update is called once per frame
    void Update()
    {
        protections.CleanUp();
        
        if (health <= 0)
        {
            LifeCycle lifeCycle = gameObject.GetComponent<LifeCycle>();
            if (lifeCycle != null)
            {
                lifeCycle.Death();
            }
            
        }

    }
}
*/