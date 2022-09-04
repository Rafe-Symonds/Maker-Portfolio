using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToadDamage : DamageNew
{
    public GameObject toad;
    public int percentChanceToTurnToToad;
    public override void specialDamageEffectOnEnemy(GameObject gameobject)
    {
        //Debug.Log("Special enemy spell " + gameobject.name + " " + gameobject.tag);
        int randomNum = UnityEngine.Random.Range(0, 100);
        if (randomNum > (100 - percentChanceToTurnToToad) && !gameobject.CompareTag(gameObject.tag) && gameobject.name == "ContactCollider" && !gameobject.tag.Contains("Base"))
        {
            Destroy(gameobject.transform.parent.gameObject);
            Instantiate(toad, gameobject.transform.position, Quaternion.identity);
        }
    }
}