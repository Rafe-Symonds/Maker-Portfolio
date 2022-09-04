using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggersAndCollidersForDeathDamage : MonoBehaviour
{
    List<GameObject> objectsInRange = new List<GameObject>();
    bool alreadyAttacked = false;
    public void AttackOnDeath()
    {
        if(alreadyAttacked == false)
        {
            alreadyAttacked = true;
            foreach (GameObject gameobject in objectsInRange)
            {
                //Debug.Log(gameobject.name);
                DamageNew damage = gameObject.GetComponent<DamageNew>();
                HealthNew health = gameobject.gameObject.GetComponent<HealthNew>();
                health.TakeDamage(damage);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.isTrigger == false)
        {
            //Debug.Log("Not a trigger");
            return;
        }
        GameObject parent = gameObject.transform.parent.gameObject;
        //Debug.Log("on trigger enter for blob");
        //Debug.Log("gameObject " + gameObject + "   collider " + collider.gameObject);
        if (parent != null && collider != null && collider.transform.parent != null && !collider.CompareTag("treasureChest") && !parent.CompareTag(collider.tag) && !collider.tag.Contains("Base") && !collider.CompareTag("background") && !collider.CompareTag("Untagged") && !collider.CompareTag(parent.tag + "Base") && !collider.CompareTag("treasureChest")
                 && !parent.CompareTag(collider.transform.parent.tag + "Base") && collider.gameObject.name == "ContactCollider")
        {
            //Debug.Log("adding troop to damage on death for blob");
            objectsInRange.Add(collider.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.isTrigger == false)
        {
            //Debug.Log("Not a trigger");
            return;
        }
        GameObject parent = gameObject.transform.parent.gameObject;
        if (parent != null && collider != null && collider.transform.parent != null && !collider.CompareTag("treasureChest") && !parent.CompareTag(collider.tag) && !collider.tag.Contains("Base") && !collider.CompareTag("background") && !collider.CompareTag("Untagged") && !collider.CompareTag(parent.tag + "Base") && !collider.CompareTag("treasureChest")
                 && !parent.CompareTag(collider.transform.parent.tag + "Base") && collider.gameObject.name == "ContactCollider")
        {
            objectsInRange.Remove(collider.gameObject);
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
