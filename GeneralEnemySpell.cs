using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralEnemySpell : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.name == "ContactCollider" && collider.tag == "team1")
        {
            DamageNew damage = gameObject.GetComponent<DamageNew>();
            if(damage != null)
            {
                damage.specialDamageEffectOnEnemy(collider.gameObject.transform.parent.gameObject);
                if(damage.damageAmount > 0)
                {
                    HealthNew health = collider.GetComponent<HealthNew>();
                    health.TakeDamage(damage);
                    
                }
            }
            Destroy(gameObject, 1f);
            //Destroy(gameObject, 1f);
        }
    }
}
