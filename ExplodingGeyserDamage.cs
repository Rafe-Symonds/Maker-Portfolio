using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodingGeyserDamage : DamageNew
{
    public override void specialDamageEffectOnEnemy(GameObject gameobject)
    {
        int num = UnityEngine.Random.Range(0, 100);
        if (num > 65)
        {
            if (gameobject.name == "ContactCollider" && gameobject.CompareTag("team1"))
            {
                gameobject.GetComponent<HealthNew>().TakeDamage(10, DamageType.fire);
                Animator animator = gameObject.GetComponentInParent<Animator>();
                animator.SetBool("special", true);
            }
        }
            
    }
}
