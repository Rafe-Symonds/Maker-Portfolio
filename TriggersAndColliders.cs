using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class TriggersAndColliders : MonoBehaviour
{ }
/*


    //Attack and Buffs is triggers and colliders

    public Animator attackAnimator;

    public TimeSpan attackSpeed = new TimeSpan(0,0,0,1,0);
    public int rangedAttackInterval;
    private TimeSpan rangedAttackSpeed;
    private DateTime nextAttack = DateTime.Now;
    private TimeSpan spellOverTimeDamage = new TimeSpan(0, 0, 1);
    private DateTime nextOverTimeSpellDamage = DateTime.Now;

    private bool inMelee = false;

    
    private Projectile Projectile;

    void OnTriggerEnter2D(Collider2D collider)
    {
        
        if(collider.isTrigger == false)
        {
            return;
        }
        //Debug.Log("OnTriggerEnter2D gameObject + " + gameObject);
        LifeCycle lifeCycle = GetComponent<LifeCycle>();
        if(lifeCycle != null)
        {
            lifeCycle.Touch(collider.gameObject);
        }

        //Debug.Log(gameObject + " + " + collider);
        //Debug.Log("Enter Trigger + " + collider);
        GeneralSpell generalSpell = collider.GetComponent<GeneralSpell>();
        Blocking enemyBlocking = collider.GetComponent<Blocking>();
        if (generalSpell != null && enemyBlocking == null)
        {
            //If the troop is colliding into a spell
            //Debug.Log("General Spell trigger");
            generalSpell.spellAffect(gameObject);


        }
        else if(generalSpell != null && enemyBlocking != null)
        {
            //If the troop is colliding into a spell wall
            Blocking(collider);
            generalSpell.spellAffect(gameObject);
        }
        else if (!gameObject.tag.Equals(collider.tag) && !collider.tag.Equals(gameObject.tag + "Base") && !collider.tag.Equals("background") && !gameObject.tag.Equals(collider.tag + "Base"))
        {
            //Ranged attack: capsule collider hits enemy box collider to do damage
            //Melee collider: box collider hits enemy box collider to do damage
            //if(collider.GetType() == typeof(BoxCollider2D))
            Blocking(collider);
            Attack(collider);
            
            //Debug.Log("Attack" + gameObject);
        } 
    }
            //We treat attacking and blocking seperatly. For blocking we will use as many points as possible to block other team. If the other team
            //is blocked we save our points for the next enemy
    public void Blocking(Collider2D collider)
    {

    
            Blocking enemyBlocking = collider.GetComponent<Blocking>();
            Blocking ourBlocking = gameObject.GetComponent<Blocking>();
            if(enemyBlocking != null && ourBlocking != null && !ourBlocking.doesNotBlock)
            {
                                
                //Debug.Log("TriggerEnter  " + gameObject + "  " + collider);
                float blockingAmountNeeded = enemyBlocking.GetOutStandingBlockingAmount();
                float ourUsedBlockingAmount = ourBlocking.TotalValueBeingUsedForBlocking();
                float amountWeHaveLeftToBlockWith = ourBlocking.totalBlockingAmount - ourUsedBlockingAmount;
                float amountToUse = Math.Min(blockingAmountNeeded, amountWeHaveLeftToBlockWith);
                //Debug.Log(blockingAmountNeeded + "  " + ourUsedBlockingAmount + "  " + amountWeHaveLeftToBlockWith + "  " + amountToUse);
                //Now we have to update our dictionary of things we are blocking and the enemy's current bloking amount
                if (amountToUse > 0)
                {
                    ourBlocking.AddToEnemiesBlockedDictionary(collider.gameObject, amountToUse);
                    enemyBlocking.IncreaseCurrentBlockingAmount(amountToUse);

                    GeneralMovement generalMovement = gameObject.GetComponent<GeneralMovement>();
                    if (generalMovement != null)
                    {
                        //If we start blocking we have to stop moving 
                        generalMovement.moving = false;
                        //Debug.Log("Moving is false");
                    }


                }
                
            }
    }
        
    

    //TO DO move into spell classes 
    void OnTriggerStay2D(Collider2D collider) //need to change to the new way of spell hitting same as OnTriggerEnter
    {
        //Debug.Log("OnTriggerStay");
        GeneralSpell generalSpell = collider.GetComponent<GeneralSpell>();
        if (generalSpell != null)
        {
            generalSpell.spellAffectOverTime(gameObject);
        }
        else if (!gameObject.tag.Equals(collider.tag) && !collider.tag.Equals(gameObject.tag + "Base") && !collider.tag.Equals("background") && !gameObject.tag.Equals(collider.tag + "Base"))
        {
            //Debug.Log("OnTriggerStay Attack");
            
            Attack(collider);
            //Debug.Log("Attack" + gameObject);
        }
    }
        
        
    
    void OnTriggerExit2D(Collider2D collider)
    {
        LifeCycle lifeCycle = GetComponent<LifeCycle>();
        if (lifeCycle != null)
        {
            lifeCycle.Leave(collider.gameObject);
        }
        //Debug.Log(gameObject + " Exit Trigger " + collider);
        Blocking enemyBlocking = collider.GetComponent<Blocking>();
        Blocking ourBlocking = gameObject.GetComponent<Blocking>();
        if (enemyBlocking != null && ourBlocking != null && !gameObject.tag.Equals(collider.tag))
        {
            ourBlocking.RemoveToEnemiesBlockedDictionary(collider.gameObject);
            if(!ourBlocking.AreWeBlockingAnything())
            {
                //Debug.Log("No longer blocker anything " + gameObject + " + " + collider);
                GeneralMovement generalMovement = gameObject.GetComponent<GeneralMovement>();
                if (generalMovement != null && !ourBlocking.AreWeBlocked())
                {
                    //If we stop blocking we have to start moving
                    //Debug.Log(gameObject + "   Moving = true");
                    generalMovement.moving = true;
                    inMelee = false;
                    
                }
            }
            
        }
        GeneralSpellNew generalSpell = collider.GetComponent<GeneralSpellNew>();
        if (generalSpell != null)
        {
            //Debug.Log("Exit Trigger General Spell");
            generalSpell.spellAffectOverTimeEnd(gameObject);
        }

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals(gameObject.tag) || collision.gameObject.name.Equals("EmptyBackground") || collision.gameObject.tag.Equals(gameObject.tag + "Summoner") || collision.gameObject.tag.Equals(gameObject.tag + "Base"))
        {
            
            Debug.Log("Ignore Collider");
            Debug.Log(collision);
            Debug.Log(gameObject);
            Debug.Log(collision.gameObject);
            if(gameObject.GetComponent<Collider2D>() != null && collision.gameObject.GetComponent<Collider2D>() != null)
            {
                Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), collision.gameObject.GetComponent<Collider2D>());
                Debug.Log("After Ignore of Collision");
            }

            return;
        }
    }
    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals(gameObject.tag) || collision.gameObject.name.Equals("EmptyBackground") || collision.gameObject.tag.Equals(gameObject.tag + "Summoner") || collision.gameObject.tag.Equals(gameObject.tag + "Base"))
        {
            //Debug.Log("Ignore Collider");
            Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), collision.gameObject.GetComponent<Collider2D>());
            return;
        }
    }

    public void OnMouseDown()
    {
        EmptyBackgroundScript emptyBackgroundScript = GameObject.Find("EmptyBackground").GetComponent<EmptyBackgroundScript>();
        emptyBackgroundScript.ClickOnTroop(Camera.main.ScreenToWorldPoint(Input.mousePosition), gameObject);

    }




    //It is the creature that attacks
    private void Attack(Collider2D collider)
    {
        inMelee = true;

        if(attackAnimator != null)
        {
            //attackAnimator.SetBool("Attacking", true);
        }
        

        if (nextAttack < DateTime.Now)
        {
            //Debug.Log("Attack");

            DamageNew damage = gameObject.GetComponent<DamageNew>();
            HealthNew health = collider.gameObject.GetComponent<HealthNew>();
            if (damage == null || health == null)
            {
                return;
            }

            
            health.TakeDamage(damage);
            
            nextAttack = DateTime.Now + attackSpeed;


            //Debug.Log("nextAttack = " + nextAttack);
            //Debug.Log("dateTime =  " + DateTime.Now);


        }
    }
    public void RangedAttack(Collider2D collider)
    {
        //Debug.Log("Ranged Attack");
        if (nextAttack < DateTime.Now && inMelee == false)
        {
            Damage damage = gameObject.GetComponent<Damage>();
            Health health = collider.gameObject.GetComponent<Health>();
            if (damage == null || health == null)
            {
                return;
            }

            RangedAttack rangedAttack = transform.GetChild(0).GetComponent<RangedAttack>();
            GameObject projectilePrefab = rangedAttack.projectile;
            GameObject  newProjectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);



            health.TakeDamage(damage);
            //UnityEngine.Debug.Log("Attack");
            nextAttack = DateTime.Now + rangedAttackSpeed;
            //Debug.Log("nextAttack + " + nextAttack);
        }
        
    }
  
    // Start is called before the first frame update
    void Start()
    {
        rangedAttackSpeed = new TimeSpan(0, 0, rangedAttackInterval);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
*/