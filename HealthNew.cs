using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthNew : MonoBehaviour
{
    public DamageNew.DamageType[] defaultProtections;

    public float[] defaultProtectionValues;
    public DamageNew.DamageType[] creatureCharacteristics;
    private int INFINITE = 360000;

    public ListWithExpirations<Protection> protections = new ListWithExpirations<Protection>();

    public int health;
    private int totalHealth;
    Sprite healthBar_100;
    Sprite healthBar_75;
    Sprite healthBar_50;
    Sprite healthBar_25;
    private bool healthBarShown = false;

    private Animator animator;

    public bool CreatureCharacteristicContains(DamageNew.DamageType target)
    {
        foreach (DamageNew.DamageType damageType in creatureCharacteristics)
        {
            if (damageType == target)
            {
                return true;
            }
        }
        return false;
    }


    public int GetTotalHealth()
    {
        return totalHealth;
    }


    public class Protection : BaseEffect
    {
        public DamageNew.DamageType[] types;
        public float[] amount;
        //Asumes types and amounts are the same length
        public Protection(DamageNew.DamageType[] types, float[] amount, int duration)
        {
            name = types[0] + "Protection";
            if (types.Length != amount.Length)
            {
                throw new Exception("types.Length != amount.Length");
            }
            TimeSpan timeLeft = new TimeSpan(0, 0, duration);
            this.types = types;
            this.amount = amount;
            this.expirationTime = TimeManagement.CurrentTime() + timeLeft;
        }

        public Protection(DamageNew.DamageType type, float amount, int duration) :
            this(new DamageNew.DamageType[] { type }, new float[] { amount }, duration) { }

        /*
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
        */
        //order so that the first one is the shortest length time wise



    }

    public class HealingOverTime : AllEffectsOnTroop.Effect
    {
        public DamageNew.DamageType[] types;
        public float amount;
        private DateTime nextHeal = TimeManagement.CurrentTime();
        private TimeSpan timeBetweenHeals = new TimeSpan(0, 0, 1);
        public HealingOverTime(DamageNew.DamageType[] types, float amount, int duration, GameObject troop) : base(duration, troop, AllEffectsOnTroop.Effect.EffectType.duplicate)
        {
            name = "Healing";
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
            */
            return heal.amount;
        }
        public override bool EffectCalledEveryFixedUpdate()
        {
            HealthNew healthScript = troop.transform.parent.Find("ContactCollider").GetComponent<HealthNew>();
            if (healthScript != null)
            {
                if (nextHeal < TimeManagement.CurrentTime())
                {


                    healthScript.health = healthScript.health + (int)(this.amount * MaxHeal(this));



                    nextHeal = TimeManagement.CurrentTime() + timeBetweenHeals;
                }
            }
            if (GetDuration() < TimeManagement.CurrentTime())
            {
                return true;
            }
            return false;
        }
        public override void StartEffect()
        {

        }
        /*
        public override GameObject GetImage()
        {
            IconManager iconManager = GameObject.Find("EmptyBackground").GetComponent<IconManager>();
            return iconManager.healing;
        }
        */
    }
    /*
    public class HealEffect : AllEffectsOnTroop.Effect
    {
        public DamageNew.DamageType[] types;
        public int amount;
        public HealEffect(DamageNew.DamageType[] types, int amount, GameObject troop) : base(1, troop, AllEffectsOnTroop.Effect.EffectType.duplicate)
        {
            name = "InstantHeal";
            this.types = types;
            this.amount = amount;
        }
        public override bool EffectCalledEveryFixedUpdate()
        {
            return true;
        }

        public override void StartEffect()
        {
            troop.GetComponentInChildren<HealthNew>().health += amount;
        }
    }
    */
    public void TakeDamage(DamageNew damage)
    {
        if (damage != null)
        {
            LifeCycleNew lifeCycle = gameObject.GetComponent<LifeCycleNew>();
            float damageAmount = damage.GetDamageAmount();
            //Each protection is additive in a protection we take the largest matching type
            foreach (Protection p in protections.GetList())
            {
                damageAmount -= damageAmount * MaxProtection(p, damage);
                
            }
            health -= Math.Max(0, (int)damageAmount);
            if (healthBarShown == true)
            {
                SetHealthBar();
            }

            if (lifeCycle != null)
            {
                if(damage.damageTypes.Length > 0)
                {
                    lifeCycle.TakenDamage(Math.Max(0, (int)damageAmount), damage.damageTypes[0]);
                }
                else
                {
                    lifeCycle.TakenDamage(Math.Max(0, (int)damageAmount));
                }
                
            }
            //UnityEngine.Debug.Log(health);
            GameObject toolTip = GameObject.Find("ToolTip");
            if(toolTip != null && toolTip.activeSelf && toolTip.GetComponent<ToolTipNew>() != null)
            {
                toolTip.GetComponent<ToolTipNew>().DamageUpdate(gameObject);
            }
            
        }
    }
    public void TakeDamage(int damage, DamageNew.DamageType damageType)
    {
        LifeCycleNew lifeCycle = gameObject.GetComponent<LifeCycleNew>();
        float damageAmount = damage;
        foreach (Protection p in protections.GetList())
        {
            damageAmount -= damageAmount * MaxProtection(p, damage, damageType);

        }
        health -= Math.Max(0, (int) damageAmount);
        if (healthBarShown == true)
        {
            SetHealthBar();
        }
        if (lifeCycle != null)
        {
            lifeCycle.TakenDamage((int) damageAmount, damageType);
        }
    }
    public void Heal(int healAmount)
    {
        health += healAmount;
        if(health > totalHealth)
        {
            health = totalHealth;
        }
    }
    private float MaxProtection(Protection protection, DamageNew damage)
    {
        float MaxReduction = 0;
        for (int d = 0; d < damage.damageTypes.Length; d++)
        {
            for (int p = 0; p < protection.types.Length; p++)
            {
                if (protection.types[p] == damage.damageTypes[d])
                {
                    MaxReduction = Math.Max(MaxReduction, protection.amount[p]);
                }
            }
        }
        return MaxReduction;
    }
    private float MaxProtection(Protection protection, int damage, DamageNew.DamageType damageType)
    {
        float MaxReduction = 0;
        
        for (int p = 0; p < protection.types.Length; p++)
        {
            if (protection.types[p] == damageType)
            {
                MaxReduction = Math.Max(MaxReduction, protection.amount[p]);
            }
        }
        
        return MaxReduction;
    }
    public void AddProtection(Protection protection)
    {
        protections.AddEffect(protection);
        Debug.Log("Adding protection effect");
        GameObject iconController = gameObject.transform.parent.Find("IconController").gameObject;
        iconController.GetComponent<IconControllerScript>().ChangeEffect();

    }
    public void ShowHealthBar()
    {
        SetHealthBar();
        healthBarShown = true;

    }
    public void HideHealthBar()
    {
        GameObject healthBar = transform.parent.Find("HealthBar").gameObject;
        SpriteRenderer healthBarSprite = healthBar.GetComponent<SpriteRenderer>();
        healthBarSprite.sprite = null;
        healthBarShown = false;
        Debug.Log("Hide HealthBar");
    }
    private void SetHealthBar()
    {
        GameObject healthBar = transform.parent.Find("HealthBar").gameObject;
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
        animator = gameObject.GetComponentInParent<Animator>();



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
    void FixedUpdate()
    {
        if (protections.CleanUp())
        {
            gameObject.transform.parent.Find("IconController").GetComponent<IconControllerScript>().ChangeEffect();
            //Debug.Log("readjusting the current icons");
        }
        
        if (health <= 0)
        {
            animator.enabled = true;
            LifeCycleNew lifeCycle = gameObject.GetComponent<LifeCycleNew>();
            if (lifeCycle != null)
            {
                lifeCycle.Death(animator);
            }
            
        }

    }
}
