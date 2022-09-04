using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowSpell : SpecificSpellNew
{
    public GameObject arrow;
    bool firstSpawn;
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

    public override void spellAffectPolymorphic(GameObject gameobject)
    {

    }

    public override void spellAffectOverTimePolymorphic(GameObject gameobject)
    {

    }
    public override void onSpellCleanUpPolymorphic()
    {
        base.onSpellCleanUpPolymorphic();
        //Debug.Log("Arrow Clean Up");
    }

    public override void spellAffectOverTimeNotNeedingTrigger()
    {
        float horizontalAdjustment = UnityEngine.Random.Range(-200, 200) / 100f;
        //Debug.Log(horizontalAdjustment);
        Vector3 spawnPoint = new Vector3(gameObject.transform.position.x + horizontalAdjustment,
                                gameObject.transform.position.y, gameObject.transform.position.z);
        GameObject arrowObject = Instantiate(arrow, spawnPoint, Quaternion.identity);
        if (gameObject.CompareTag("team2"))
        {
            arrowObject.tag = "team2";
        }
        //Debug.Log("Spawning Arrows");
    }
}