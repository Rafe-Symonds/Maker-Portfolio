using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepDamage : DamageNew
{
    public GameObject sheep;
    public override void specialDamageEffectOnEnemy(GameObject gameobject)
    {
        //Debug.Log("Special enemy spell " + gameobject.name + " " + gameobject.tag);
        if(gameobject.tag == "team1")
        {
            Destroy(gameobject);
            Instantiate(sheep, gameobject.transform.position, Quaternion.identity);
        }
    }
}
