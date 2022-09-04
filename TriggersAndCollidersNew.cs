using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class TriggersAndCollidersNew : MonoBehaviour
{


    //Attack and Buffs is triggers and colliders

    

    public TimeSpan attackSpeed = new TimeSpan(0,0,0,1,0);
    public int rangedAttackInterval;
    private TimeSpan rangedAttackSpeed;
    private DateTime nextAttack = TimeManagement.CurrentTime();
    private TimeSpan spellOverTimeDamage = new TimeSpan(0, 0, 1);
    private DateTime nextOverTimeSpellDamage = TimeManagement.CurrentTime();

    public bool inMelee = false;

    
    private Projectile projectile;

    private Animator animator;
    public AudioSource attackSound;
    public AudioSource spawnSound;
    //public AudioSource spawnSound;
    
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (gameObject.name == "GroundCollider")
        {
            return;
        }
        if (collider.gameObject.name == "GroundCollider")
        {
            return;
        }
        if (gameObject.name == "RangedCollider")
        {
            return;
        }
        if (collider.gameObject.name == "RangedCollider")
        {
            return;
        }
        if (collider.gameObject.name == "SpecialCollider")
        {
            return;
        }
        if (gameObject.name == "SpecialCollider")
        {
            return;
        }
        if (gameObject.CompareTag("treasureChest"))
        {
            return;
        }
        if (collider.CompareTag("treasureChest"))
        {
            return;
        }
        Transform specialCollider = gameObject.transform.Find("SpecialCollider");
        if(specialCollider != null)
        {
            //Collider2D ourSpecialCollider = specialCollider.GetComponent<Collider2D>();
            Collider2D ourContactCollider = gameObject.transform.Find("ContactCollider").GetComponent<Collider2D>();
            if (collider.Distance(ourContactCollider).distance > 3 * ourContactCollider.bounds.size.x)
            {
                return;
            }
        }
        
        //Debug.Log(gameObject + "   " + collider);
        //Debug.Log("TriggerEnter    Collider.gameObject = " + collider.gameObject);
        if (collider.isTrigger == false)
        {
            //Debug.Log("Not a trigger");
            return;
        }
        //Debug.Log("OnTriggerEnter2D gameObject + " + gameObject);
        LifeCycleNew lifeCycle = GetComponent<LifeCycleNew>();
        if(lifeCycle != null)
        {
            lifeCycle.Touch(collider.gameObject);
        }

        //Debug.Log(gameObject + " + " + collider);
        //Debug.Log("Enter Trigger + " + collider);
        GeneralSpellNew generalSpell = collider.GetComponent<GeneralSpellNew>();
        BlockingNew enemyBlocking = collider.GetComponent<BlockingNew>();
        if (generalSpell != null && enemyBlocking == null)
        {
            //If the troop is colliding into a spell
            //Debug.Log("General Spell trigger " + gameObject);
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
            
            if (collider.gameObject.name == "ContactCollider" && attackSound.clip != null)
            {
                attackSound.PlayOneShot(attackSound.clip);
            }
            //Ranged attack: capsule collider hits enemy box collider to do damage
            //Melee collider: box collider hits enemy box collider to do damage
            //if(collider.GetType() == typeof(BoxCollider2D))
            //Debug.Log("Triggers and Colliders OnTriggerEnter  " + gameObject + "  " + collider + "      " + collider.gameObject);
            Blocking(collider);
            Attack(collider);
            if (gameObject.tag.Equals("team1"))
            {
                gameObject.transform.position.Set(gameObject.transform.position.x + 0.04f, gameObject.transform.position.y, gameObject.transform.position.z);
                //Debug.Log("Moving creature forwards for contact " + gameObject.name + " " + collider.name + " " + collider.gameObject.name);
            }
            else
            {
                gameObject.transform.position.Set(gameObject.transform.position.x - 0.04f, gameObject.transform.position.y, gameObject.transform.position.z);
            }
            //Debug.Log("Attack" + gameObject);
        } 
    }
    //We treat attacking and blocking seperatly. For blocking we will use as many points as possible to block other team. If the other team
    //is blocked we save our points for the next enemy
    
    

    //TO DO move into spell classes 
    void OnTriggerStay2D(Collider2D collider) //need to change to the new way of spell hitting same as OnTriggerEnter
    {
        if (gameObject.name == "GroundCollider")
        {
            return;
        }
        if (collider.gameObject.name == "GroundCollider")
        {
            return;
        }
        if (gameObject.name == "RangedCollider")
        {
            return;
        }
        if (collider.gameObject.name == "RangedCollider")
        {
            return;
        }
        if (collider.gameObject.name == "SpecialCollider")
        {
            return;
        }
        if (gameObject.name == "SpecialCollider")
        {
            return;
        }
        if (gameObject.CompareTag("treasureChest"))
        {
            return;
        }
        if (collider.CompareTag("treasureChest"))
        {
            return;
        }
        GeneralSpellNew generalSpell = collider.GetComponent<GeneralSpellNew>();
        //Debug.Log(generalSpell + " general spell");
        if (generalSpell != null)
        {
            generalSpell.spellAffectOverTime(gameObject);
        }
        else if (!gameObject.tag.Equals(collider.tag) && !collider.tag.Equals(gameObject.tag + "Base") && !collider.tag.Equals("background") && !gameObject.tag.Equals(collider.tag + "Base"))
        {
            //Debug.Log("OnTriggerStay Attack");
            //Debug.Log("OnTriggerStay" + gameObject);
            Attack(collider);
            //Debug.Log("Attack OnTriggerStay " + gameObject);
        }
    }
        
        
    
    void OnTriggerExit2D(Collider2D collider)
    {
        //Debug.Log("OnTriggerExit " + gameObject);
        LifeCycleNew lifeCycle = GetComponent<LifeCycleNew>();
        if (lifeCycle != null)
        {
            lifeCycle.Leave(collider.gameObject);
        }
        //Debug.Log(gameObject + " Exit Trigger " + collider);
        BlockingNew enemyBlocking = collider.GetComponent<BlockingNew>();
        BlockingNew ourBlocking = gameObject.transform.Find("ContactCollider").GetComponent<BlockingNew>();
        if (enemyBlocking != null && ourBlocking != null && !gameObject.tag.Equals(collider.tag))
        {
            //Debug.Log("OnTriggerExit " + gameObject);
            enemyBlocking.DecreaseCurrentBlockingAmount(ourBlocking.GetAmountWeAreBlockingEnemyWith(collider.gameObject));
            ourBlocking.RemoveToEnemiesBlockedDictionary(collider.gameObject);
            
            if(!ourBlocking.AreWeBlockingAnything())
            {
                //Debug.Log("No longer blocker anything " + gameObject + " + " + collider);
                GeneralMovementNew generalMovement = gameObject.transform.Find("ContactCollider").GetComponent<GeneralMovementNew>();

                if (generalMovement != null && !ourBlocking.AreWeBlocked())
                {
                    //If we stop blocking we have to start moving
                    //Debug.Log(gameObject + "   Moving = true");
                    generalMovement.moving = true;
                    inMelee = false;
                    if(animator != null)
                    {
                        animator.SetBool("inMelee", false);
                    }                   
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
        //Debug.Log(collision.gameObject.name);
        if(!collision.gameObject.name.Equals("Ground"))
        {
            //Debug.Log("gameObject = " + gameObject + "   collider = " + collision.gameObject);
            //Debug.Log(gameObject.transform.Find("GroundCollider"));
            //Debug.Log(gameObject.transform.Find("GroundCollider").GetComponent<Collider2D>());
            //Debug.Log(collision.gameObject);
            //Debug.Log(collision.gameObject.GetComponent<Collider2D>());
            Collider2D collider = collision.gameObject.GetComponent<Collider2D>();
            GameObject groundCollider = gameObject.transform.Find("GroundCollider").gameObject;
            //Debug.Log(collider);
            //Debug.Log(groundCollider);
            if (collider != null)
            {
                Physics2D.IgnoreCollision(groundCollider.GetComponent<Collider2D>(), collider);
            }
            return;
        }
        
        



        /*
        if (collision.gameObject.tag.Equals("team1") || collision.gameObject.tag.Equals("team2") || collision.gameObject.name.Equals("EmptyBackground") || collision.gameObject.tag.Equals(gameObject.tag + "Summoner") || collision.gameObject.tag.Equals(gameObject.tag + "Base"))
        {
            //Debug.Log("Ignore Collider");
            Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), collision.gameObject.GetComponent<Collider2D>());
            return;
        }
        */
    }
    void OnCollisionStay2D(Collision2D collision)
    {
        /*
        if (collision.gameObject.tag.Equals(gameObject.tag) || collision.gameObject.name.Equals("EmptyBackground") || collision.gameObject.tag.Equals(gameObject.tag + "Summoner") || collision.gameObject.tag.Equals(gameObject.tag + "Base"))
        {
            //Debug.Log("Ignore Collider");
            Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), collision.gameObject.GetComponent<Collider2D>());
            return;
        }
        */
    }

    public void OnMouseDown()
    {
        if(!gameObject.CompareTag("team1Base") && !gameObject.CompareTag("team2Base"))
        {
            EmptyBackgroundScript emptyBackgroundScript = GameObject.Find("EmptyBackground").GetComponent<EmptyBackgroundScript>();
            emptyBackgroundScript.ClickOnTroop(Camera.main.ScreenToWorldPoint(Input.mousePosition), gameObject);
        }
    }


    public void Blocking(Collider2D collider)
    {
        Transform contactCollider = gameObject.transform.Find("ContactCollider");
        GeneralMovementNew generalMovement = contactCollider.GetComponent<GeneralMovementNew>();
        GeneralMovementNew enemyGeneralMovement = collider.GetComponentInChildren<GeneralMovementNew>();
        if(generalMovement != null && enemyGeneralMovement != null)
        {
            if (generalMovement.flying == true)
            {
                return;
            }
        }
        

        if (generalMovement != null && generalMovement.frozen == false)
        {
            BlockingNew enemyBlocking = collider.GetComponent<BlockingNew>();
            BlockingNew ourBlocking = gameObject.transform.Find("ContactCollider").GetComponent<BlockingNew>();
            if (enemyBlocking != null && ourBlocking != null && !ourBlocking.doesNotBlock)
            {

                //Debug.Log("Blocking  " + gameObject + "  " + collider);
                float blockingAmountNeeded = enemyBlocking.HowMuchBlockingIHaveLeft();
                float ourUsedBlockingAmount = ourBlocking.TotalValueBeingUsedForBlocking();
                float amountWeHaveLeftToBlockWith = ourBlocking.totalBlockingAmount - ourUsedBlockingAmount;
                float amountWeShouldUseToBlock = Math.Min(blockingAmountNeeded, amountWeHaveLeftToBlockWith);
                //Debug.Log(blockingAmountNeeded + "  " + ourUsedBlockingAmount + "  " + amountWeHaveLeftToBlockWith + "  " + amountWeShouldUseToBlock);
                //Now we have to update our dictionary of things we are blocking and the enemy's current bloking amount
                if (amountWeShouldUseToBlock > 0)
                {
                    ourBlocking.AddToEnemiesBlockedDictionary(collider.gameObject, amountWeShouldUseToBlock);
                    enemyBlocking.IncreaseCurrentBlockingAmount(amountWeShouldUseToBlock);
                    //If we start blocking we have to stop moving 
                    generalMovement.moving = false;
                    //Debug.Log("Moving is false");


                }
            }
        }
    }

    //It is the creature that attacks
    private void Attack(Collider2D collider)
    {
        Transform contactCollider = gameObject.transform.Find("ContactCollider");
        GeneralMovementNew generalMovement = contactCollider.GetComponent<GeneralMovementNew>();
        if (generalMovement != null && generalMovement.frozen == false)
        {
            //Debug.Log("Attack gameObject = " + gameObject);
            if(generalMovement.moving == false)
            {
                inMelee = true;
            }
            if (generalMovement.flying == true)
            {
                inMelee = true;
            }


            if (contactCollider == null)
            {
                return;
            }




            if (nextAttack < TimeManagement.CurrentTime())
            {
                nextAttack = TimeManagement.CurrentTime() + attackSpeed;
                //Debug.Log("Attack");

                DamageNew damage = gameObject.transform.Find("ContactCollider").GetComponent<DamageNew>();

                HealthNew health = collider.gameObject.GetComponent<HealthNew>();
                if (damage == null || health == null)
                {
                    //Debug.Log("Health and Damage Check");
                    //Debug.Log(health);
                    //Debug.Log(damage);
                    return;
                }
                GeneralMovementNew troopGeneralMovementNew = gameObject.GetComponentInChildren<GeneralMovementNew>();
                bool moving = true;
                if (troopGeneralMovementNew != null)
                {
                    moving = troopGeneralMovementNew.moving;
                }

                if (animator != null && moving == false)
                {
                    animator.SetBool("inRanged", false);
                    animator.SetBool("inMelee", true);
                }
                if (animator != null && generalMovement.flying == true)
                {
                    animator.SetBool("moving", false);
                    animator.SetBool("inMelee", true);
                }

                health.TakeDamage(damage);
                damage.specialDamageEffectOnEnemy(collider.gameObject);

                

                //Debug.Log("Attack not ranged " + gameObject);
                //Debug.Log("nextAttack = " + nextAttack);
                //Debug.Log("dateTime =  " + DateTime.Now);


            }
        }
        
    }
    public void RangedAttack(GameObject gameobject)
    {
        Transform contactCollider = gameObject.transform.Find("ContactCollider");
        GeneralMovementNew generalMovement = contactCollider.GetComponent<GeneralMovementNew>();
        if(generalMovement != null && generalMovement.frozen == false)
        {
            RangedAttackNew rangedAttack = transform.Find("RangedCollider").GetComponent<RangedAttackNew>();
            GameObject projectilePrefab = rangedAttack.projectile;

            Vector3 spawnPoint = new Vector3(transform.position.x, transform.position.y +
                rangedAttack.projectileSpawnHeight * contactCollider.GetComponent<Collider2D>().bounds.size.y / 2, 0);

            GameObject newProjectile = Instantiate(projectilePrefab, spawnPoint, Quaternion.identity, transform);
            newProjectile.tag = gameObject.tag;
            newProjectile.GetComponent<Projectile>().MoveProjectile(gameobject);
            if(newProjectile.GetComponent<ParticleSystem>() != null)
            {
                newProjectile.GetComponent<ParticleSystem>().time = 5;
            }

            if (contactCollider.tag.Equals("team1"))
            {
                newProjectile.transform.rotation = Quaternion.Euler(rangedAttack.rotationX, rangedAttack.rotationY, rangedAttack.rotationZ);
            }
            else
            {
                newProjectile.transform.rotation = Quaternion.Euler(rangedAttack.rotationX, rangedAttack.rotationY, -rangedAttack.rotationZ);
                if(newProjectile.GetComponent<SpriteRenderer>() != null)
                {
                    newProjectile.GetComponent<SpriteRenderer>().flipX = !newProjectile.GetComponent<SpriteRenderer>().flipX;
                }
                
            }
            //Debug.Log("Instantiate Projectile");             
            //UnityEngine.Debug.Log("Attack");
            nextAttack = TimeManagement.CurrentTime() + rangedAttackSpeed;
            //Debug.Log("nextAttack + " + nextAttack);
        }
    }


    public void SetTeam(string teamName)
    {
        gameObject.tag = teamName;
        gameObject.transform.Find("ContactCollider").tag = teamName;
        gameObject.transform.Find("GroundCollider").tag = teamName;
    }

    // Start is called before the first frame update
    void Start()
    {
        rangedAttackSpeed = new TimeSpan(0, 0, rangedAttackInterval);
        animator = gameObject.GetComponent<Animator>();
        /*
        if (spawnSound.clip != null)
        {
            spawnSound.PlayOneShot(spawnSound.clip);
        }
        */
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        if(animator != null)
        {
            if (animator.GetBool("moving") == false && animator.GetBool("dying") == false && animator.GetBool("inMelee") == false)
            {
                //animator.SetBool("idle", true);
            }
            else
            {
                animator.SetBool("idle", false);
            }
        }
        
        
    }
}
