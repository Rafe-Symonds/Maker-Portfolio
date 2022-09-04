using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LifeCycleNew : MonoBehaviour
{
    public enum Properties { WildernessWalk, Webable, Living};
    public Properties[] properties;


    public GameObject spirit;
    private GameObject spiritCreated = null;
    private GameObject damageNumberPrefab;
    public static int deathPosition = 100;
    protected bool dead = false;
    //private Animator runanimator;
    public void TakenDamage(int damageAmount, DamageNew.DamageType damageType)
    {
        GameObject damageNumber = Instantiate(damageNumberPrefab, transform.position, Quaternion.identity);
        damageNumber.transform.localScale = new Vector3(1, 1, 0);
        TextMeshPro text = damageNumber.GetComponent<TextMeshPro>();
        text.text = "" + damageAmount;

        //Debug.Log("Damage Amount " + damageAmount);
        if(damageType == DamageNew.DamageType.poison)
        {
            text.color = Color.green;
        }
        else if (damageType == DamageNew.DamageType.fire)
        {
            text.color = Color.red;
        }
        else if (damageType == DamageNew.DamageType.cold)
        {
            text.color = new Color(0.65f, 0.87f, 0.95f);
        }
        else if (damageType == DamageNew.DamageType.physical)
        {
            text.color = Color.gray;
        }
        else if (damageType == DamageNew.DamageType.magic)
        {
            text.color = new Color(0.58f, 0.06f, 0.8f);
        }
        else if (damageType == DamageNew.DamageType.undead)
        {
            text.color = Color.black;
        }
        else if (damageType == DamageNew.DamageType.water)
        {
            text.color = Color.blue;
        }
        else if (damageType == DamageNew.DamageType.earth)
        {
            text.color = new Color(0.29f, 0.17f, 0.10f);
        }
        else
        {
            text.color = Color.white;
        }





        float randomNumber = UnityEngine.Random.Range(-0.5f, 0.5f);
        
        Rigidbody2D rb2D = damageNumber.GetComponent<Rigidbody2D>();
        rb2D.velocity = new Vector2(randomNumber, 1);
        Destroy(damageNumber, 1f);
    }
    public void TakenDamage(int damageAmount)
    {
        GameObject damageNumber = Instantiate(damageNumberPrefab, transform.position, Quaternion.identity);
        damageNumber.transform.localScale = new Vector3(1, 1, 0);
        TextMeshPro text = damageNumber.GetComponent<TextMeshPro>();
        text.text = "" + damageAmount;
        //Debug.Log("Damage Amount " + damageAmount);
        float randomNumber = UnityEngine.Random.Range(-0.5f, 0.5f);
        Rigidbody2D rb2D = damageNumber.GetComponent<Rigidbody2D>();
        rb2D.velocity = new Vector2(randomNumber, 1);
        Destroy(damageNumber, 1f);
    }
    public virtual void Death(Animator animator)
    {
        if(dead == false)
        {
            dead = true;
            //Destroy(gameObject.transform.parent.GetComponent<Rigidbody2D>());
            //Debug.Log("Death");

            //Debug.Log("Runanimator = " + animator);

            if (animator != null)
            {
                //Debug.Log("setting dying to true");
                animator.SetBool("inMelee", false);
                animator.SetBool("moving", false);
                animator.SetBool("dying", true);
                
                
                if (spiritCreated == null)
                {
                    if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Dying"))
                    {
                        if(spirit != null)
                        {
                            spiritCreated = Instantiate(spirit, transform.position, Quaternion.identity);
                            //gameObject.transform.parent.position = new Vector3(deathPosition, 0, 0);
                            //deathPosition += 10;
                            //Debug.Log("Creature dies");
                            //Destroy(gameObject.transform.parent.gameObject, 0.1f);
                        }

                    }

                }
            }
            //Debug.Log("Die animation");
            if (gameObject.tag == "team2")
            {
                GeneralMovementNew troopGeneralMovement = gameObject.GetComponent<GeneralMovementNew>();
                GeneralMoney money = GameObject.Find("Money").GetComponent<GeneralMoney>();
                if (troopGeneralMovement != null)
                {
                    int moneyCost = troopGeneralMovement.moneyCost / 2;
                    //Debug.Log(moneyCost);
                    money.AddMoney(moneyCost);
                }

            }
            
        }   
        
        
    }


    public virtual void Touch(GameObject other)
    {
        
    }
    public virtual void Leave(GameObject other)
    {
        

    }
    public bool CheckHasProperty(Properties properties2)
    {
        for(int i = 0; i <= properties.Length - 1; i++)
        {
            if(properties[i] == properties2)
            {
                return true;
            }
        }
        return false;
    }
    public bool GetDead()
    {
        return dead;
    }

    // Start is called before the first frame update
    public virtual void Start()
    {
        damageNumberPrefab = Resources.Load("Prefabs/General/DamageNumber") as GameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

