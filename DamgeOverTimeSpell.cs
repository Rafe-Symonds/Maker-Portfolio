using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamgeOverTimeSpell : SpecificSpellNew
{
    List<GameObject> troopParentObjectsInArea = new List<GameObject>();
    public int damageAmount;
    public DamageNew.DamageType damageType;
    public override void specialDamageEffectOnDeath(GameObject gameobject)
    {
        
    }

    public override void specialDamageEffectOnEnemy(GameObject gameobject)
    {
        
    }

    public override void spellAffectOnSpecificTroopPolymorphic()
    {
        
    }

    public override void spellAffectOverTimeEndPolymorphic(GameObject gameobject)
    {
        
    }

    public override void spellAffectOverTimeNotNeedingTrigger()
    {
        //only getting called once per gameobject per time period - regardless of number of gameobjects in area
        //Debug.Log("spellAffectOverTimePolymorphic being called");
        foreach (GameObject parentTroop in troopParentObjectsInArea)
        {
            //Debug.Log("starting to do damage to troops for over time damage");
            if (parentTroop != null)
            {
                parentTroop.transform.Find("ContactCollider").GetComponent<HealthNew>().TakeDamage(damageAmount, damageType);
            }
        }
    }

    public override void spellAffectOverTimePolymorphic(GameObject gameobject)
    {
        
    }

    public override void spellAffectPolymorphic(GameObject gameobject)
    {
        
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        //Debug.Log("trigger enter for damage over time " + collider.gameObject);
        if (!collider.CompareTag(gameObject.tag) && !collider.CompareTag("treasureChest") && !collider.tag.Contains("Base")
                        && collider.gameObject.name == "ContactCollider")
        {
            //Debug.Log("Adding troops to list for damage over time");
            troopParentObjectsInArea.Add(collider.transform.parent.gameObject);
        }
    }
    void OnTriggerExit2D(Collider2D collider)
    {
        if (!collider.CompareTag(gameObject.tag) && !collider.CompareTag("treasureChest") && !collider.tag.Contains("Base")
                        && collider.gameObject.name == "ContactCollider")
        {
            troopParentObjectsInArea.Remove(collider.transform.parent.gameObject);
        }
    }
}
