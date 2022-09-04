using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateSun : SpecificSpellNew
{
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
        Debug.Log("Top of create sun");
        string teamBeingEffected = "";
        if (gameObject.tag == "team1")
        {
            teamBeingEffected = "team2";
        }
        else
        {
            teamBeingEffected = "team1";
        }
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag(teamBeingEffected);
        foreach (GameObject gameobject in gameObjects)
        {
            HealthNew healthNew = gameobject.GetComponent<HealthNew>();
            if (healthNew != null)
            {
                Debug.Log("Found creature in create sun");
                if (healthNew.CreatureCharacteristicContains(DamageNew.DamageType.undead))
                {
                    healthNew.TakeDamage(1, DamageNew.DamageType.fire);
                }
            }
        }
    }

    public override void spellAffectOverTimePolymorphic(GameObject gameobject)
    {
        
    }

    public override void spellAffectPolymorphic(GameObject gameobject)
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 10);
        gameObject.transform.position.Set(4, 7.5f, 0);
    }
}
