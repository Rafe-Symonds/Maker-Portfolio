using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpecialTarget : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag.Equals("team1") && collider.name.Equals("ContactCollider"))
        {
            GameObject enemyBase = GameObject.Find("Base2");
            enemyBase.GetComponent<GeneralEnemyPlayer>().specialTarget = collider.gameObject;
        }
    }
    void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.tag.Equals("team1") && collider.name.Equals("ContactCollider"))
        {
            GameObject enemyBase = GameObject.Find("Base2");
            enemyBase.GetComponent<GeneralEnemyPlayer>().specialTarget = collider.gameObject;
        }
    }
}
