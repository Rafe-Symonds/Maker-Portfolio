using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClericProjectileScript : Projectile
{

    public override void OnTriggerEnter2D(Collider2D collider)
    {
        if (!gameObject.tag.Equals(collider.tag) && !collider.tag.Equals(gameObject.tag + "Base") && !collider.tag.Equals("background") && !gameObject.tag.Equals(collider.tag + "Base") && !collider.tag.Equals("treasureChest"))
        {
            
            Debug.Log("**** Cleric Projectile hitting  " + gameObject);
            DamageNew damage = gameObject.GetComponent<DamageNew>();
            HealthNew health = collider.GetComponentInChildren<HealthNew>();

            if (damage == null || health == null)
            {
                return;
            }

            

            int randomNumber = UnityEngine.Random.Range(0, 100);
            if (randomNumber > 95)
            {
                collider.GetComponentInChildren<AllEffectsOnTroop>().AddToList(new FearEffect(10, collider.transform.parent.gameObject));
            }
            else
            {
                health.TakeDamage(damage);
            }

            Destroy(gameObject, destroyDelay);
        }
        else if (collider.name == "Ground" || collider.name.Equals("Boundary"))
        {
            Destroy(gameObject);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
