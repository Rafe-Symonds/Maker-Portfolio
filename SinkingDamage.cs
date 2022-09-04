using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinkingDamage : DamageNew
{
    public GameObject splash;
    public override void specialDamageEffectOnEnemy(GameObject gameobject)
    {
        if (gameobject.name == "ContactCollider" && gameobject.CompareTag("team1"))
        {
            if(gameobject.GetComponent<GeneralMovementNew>().levitate == true)
            {
                return;
            }
            int num = UnityEngine.Random.Range(0, 100);
            if(num > 80)
            {
                gameobject.GetComponent<HealthNew>().TakeDamage(999, DamageType.poison);
                BoxCollider2D groundCollider = gameobject.transform.parent.Find("GroundCollider").GetComponent<BoxCollider2D>();
                Vector3 splashSpawnPoint = new Vector3(gameobject.transform.position.x, gameobject.transform.position.y - (groundCollider.bounds.extents.y / 2) + 1.7f, 0);
                Instantiate(splash, splashSpawnPoint, Quaternion.identity);
            }
        }
    }
}
