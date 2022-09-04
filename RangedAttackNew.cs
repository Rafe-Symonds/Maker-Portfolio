using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttackNew : MonoBehaviour
{
    public GameObject target;
    GeneralMovementNew generalMovement;
    TriggersAndCollidersNew triggersAndColliders;
    public GameObject projectile;
    public int rangedDamage;
    public int pierceAmount;
    public float projectileSpawnHeight;
    public int rotationX;
    public int rotationY;
    public int rotationZ;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (!gameObject.tag.Equals(collider.tag) && !collider.tag.Equals(gameObject.tag + "Base") && !collider.tag.Equals("background") 
                                    && !gameObject.tag.Equals(collider.tag + "Base") && !collider.tag.Equals("treasureChest"))
        {
            if(collider.GetComponent<GeneralSpellNew>() != null || collider.GetComponent<GeneralEnemySpell>() != null || collider.GetComponent<Projectile>() != null)
            {
                return;
            }
            //Debug.Log(target);
            if(CheckIfTargetIsNotObstructed(gameObject, collider.gameObject) == true)
            {
                return;
            }
            if (target == null)
            {
                target = collider.gameObject;
                //Debug.Log(target.name);
            }


            //Debug.Log("Ranged Attack OnTriggerEnter   Collider = " + collider);
            if (!gameObject.tag.Equals(collider.tag) && !collider.tag.Equals(gameObject.tag + "Base") && !collider.tag.Equals("background") 
                                    && !gameObject.tag.Equals(collider.tag + "Base") && !collider.tag.Equals("treasureChest"))
            {
                if (collider.gameObject.GetComponent<GeneralSpellNew>() != null)
                {
                    return;
                }
                if(generalMovement != null)
                {
                    generalMovement.moving = false;
                }
                
                //triggersAndColliders.RangedAttack(collider);
                Animator animator = transform.parent.GetComponent<Animator>();
                if (animator != null)
                {
                    TriggersAndCollidersNew triggersAndColliders = gameObject.transform.parent.GetComponent<TriggersAndCollidersNew>();
                    if (triggersAndColliders.attackSound.clip != null)
                    {
                        triggersAndColliders.attackSound.PlayOneShot(triggersAndColliders.attackSound.clip);
                    }
                    animator.SetBool("inRanged", true);
                }
            }
        }
    }


    private void OnTriggerStay2D(Collider2D collider)
    {
        if (!gameObject.tag.Equals(collider.tag) && !collider.tag.Equals(gameObject.tag + "Base") && !collider.tag.Equals("background")
                                    && !gameObject.tag.Equals(collider.tag + "Base") && !collider.tag.Equals("treasureChest"))
        {
            if (collider.GetComponent<GeneralSpellNew>() != null || collider.GetComponent<GeneralEnemySpell>() != null || collider.GetComponent<Projectile>() != null)
            {
                return;
            }
            if(CheckIfTargetIsNotObstructed(gameObject, collider.gameObject) == true)
            {
                target = null;
                Animator animator = transform.parent.GetComponent<Animator>();
                if (animator != null)
                {
                    animator.SetBool("inRanged", false);
                }
                return;
            }
            if (target == null)
            {
                target = collider.gameObject;
                if (!gameObject.tag.Equals(collider.tag) && !collider.tag.Equals(gameObject.tag + "Base") && !collider.tag.Equals("background")
                                    && !gameObject.tag.Equals(collider.tag + "Base") && !collider.tag.Equals("treasureChest"))
                {
                    if (collider.gameObject.GetComponent<GeneralSpellNew>() != null)
                    {
                        return;
                    }
                    if (generalMovement != null)
                    {
                        generalMovement.moving = false;
                    }

                    //triggersAndColliders.RangedAttack(collider);
                    Animator animator = transform.parent.GetComponent<Animator>();
                    if (animator != null)
                    {
                        animator.SetBool("inRanged", true);
                    }
                }
            }
            /*
            generalMovement.moving = false;
            Animator animator = transform.parent.GetComponent<Animator>();
            animator.SetBool("moving", false);
            */
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (!gameObject.tag.Equals(collider.tag) && !collider.tag.Equals(gameObject.tag + "Base") && !collider.tag.Equals("background") 
                                    && !gameObject.tag.Equals(collider.tag + "Base") && !collider.tag.Equals("treasureChest"))
        {
            if (collider.GetComponent<GeneralSpellNew>() != null || collider.GetComponent<GeneralEnemySpell>() != null || collider.GetComponent<Projectile>() != null)
            {
                return;
            }
            if (target == collider.gameObject)
            {
                target = null;
            }
            else
            {
                return;
            }
            generalMovement.moving = true;
            //Debug.Log(gameObject + "   RangedAttack  Moving = true");
            Animator animator = transform.parent.GetComponent<Animator>();
            if (animator != null)
            {
                animator.SetBool("inRanged", false);
            }
        }
    }

    public bool CheckIfTargetIsNotObstructed(GameObject troop, GameObject target)
    {
        float distance = Mathf.Sqrt(Mathf.Pow(troop.transform.position.x - target.transform.position.x, 2) + Mathf.Pow(troop.transform.position.y - target.transform.position.y, 2));
        RaycastHit2D[] hits = Physics2D.RaycastAll(troop.transform.position, target.transform.position - troop.transform.position, distance);
        for(int i = 0; i < hits.Length; i++)
        {
            if (hits[i].transform.gameObject.name == "Ground")
            {
                //Debug.Log("hits " + hits[i].transform.gameObject.name);
                return true;
            }
        }
        return false;
    }

    // Start is called before the first frame update
    void Start()
    {
        generalMovement = transform.parent.Find("ContactCollider").GetComponent<GeneralMovementNew>();
        triggersAndColliders = transform.parent.GetComponent<TriggersAndCollidersNew>();

        //moving to a random spot so the ranged collider does not start firing when spawned
        transform.position = new Vector3(UnityEngine.Random.Range(100, 1000), UnityEngine.Random.Range(100, 1000), 0);
        Invoke("MoveRangedColliderToCenter", 0.5f);
    }
    void Update()
    {
        if(target == null)
        {
            generalMovement.moving = true;
            Animator animator = transform.parent.GetComponent<Animator>();
            if (animator != null)
            {
                animator.SetBool("inRanged", false);
                animator.SetBool("moving", true);
            }
        }
        else
        {
            if(target.CompareTag(gameObject.tag) == true)
            {
                target = null;
            }
            else if(target.transform.position.y > 8)
            {
                target = null;
            }
        }
    }

    private void MoveRangedColliderToCenter()
    {
        transform.localPosition = new Vector3(0, 0, 0);
    }
}
