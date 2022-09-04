using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthquakeSpellScript : SpecificSpellNew
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
        GameObject camera = GameObject.Find("Main Camera");
        StartCoroutine(camera.GetComponent<CameraShake>().Shake(1f, .05f));

        //Debug.Log("Earthquake start");
        string teamBeingEffected = "";
        if (gameObject.tag == "team1")
        {
            teamBeingEffected = "team2";
            GameObject enemyStructure = GameObject.Find("EnemyStructure");
            if(enemyStructure != null && enemyStructure.transform.childCount > 0)
            {
                enemyStructure.transform.GetChild(0).GetComponentInChildren<HealthNew>().TakeDamage(10, DamageNew.DamageType.earth);
            }
        }
        else
        {
            teamBeingEffected = "team1";
            GameObject ourStructure = GameObject.Find("OurStructure");
            if (ourStructure != null && ourStructure.transform.childCount > 0)
            {
                ourStructure.transform.GetChild(0).GetComponentInChildren<HealthNew>().TakeDamage(10, DamageNew.DamageType.earth);
            }
        }
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag(teamBeingEffected);
        foreach (GameObject troop in gameObjects)
        {
            HealthNew healthNew = troop.GetComponent<HealthNew>();
            if (healthNew != null)
            {
                //Debug.Log("Found creature in earthquake");
                healthNew.TakeDamage(10, DamageNew.DamageType.earth);
            }
        }
        Destroy(gameObject, 2f);
    }

    public override void spellAffectOverTimePolymorphic(GameObject gameobject)
    {
        
    }

    public override void spellAffectPolymorphic(GameObject gameobject)
    {
        
    }
}
